using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net.Http;
using Client.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client;

/// <summary>
/// The shared whiteboard. Drawing is done on a backing <see cref="Bitmap"/> so the
/// picture persists and can be saved on End. Every local action is also sent through
/// the SignalR hub, and actions received from other clients are drawn on the same
/// bitmap (marshalled back onto the UI thread).
/// </summary>
public partial class WhiteboardForm : Form
{
    private readonly string _hubUrl;
    private readonly string _displayName;

    private HubConnection? _connection;

    private Bitmap _bitmap = null!;
    private Graphics _g = null!;

    private bool _isDrawing;
    private Point _lastPoint;

    private Color _currentColor = Color.Black;
    private int _currentWidth = 4;

    private bool _ending;          // guards against closing twice
    private bool _savePromptOpen;  // a Save dialog is currently open

    private volatile bool _accepted;   // server accepted us (sent the board state)
    private volatile bool _rejected;   // server rejected us (room full)
    private bool _loadedOnce;          // applied the server board once (skip on reconnect)
    private string _rejectMessage = "Room is full.";

    public WhiteboardForm(string hubUrl, string displayName)
    {
        _hubUrl = hubUrl;
        _displayName = displayName;

        InitializeComponent();

        // Backing bitmap is exactly the size of the canvas, so the mouse position maps
        // 1:1 onto bitmap pixels. The canvas is the same fixed size on every client,
        // which keeps stroke coordinates aligned across all of them.
        _bitmap = new Bitmap(picCanvas.Width, picCanvas.Height);
        _g = Graphics.FromImage(_bitmap);
        _g.SmoothingMode = SmoothingMode.AntiAlias;
        _g.Clear(Color.White);
        picCanvas.Image = _bitmap;

        pnlColorPreview.BackColor = _currentColor;

        // Wire up events in code (keeps the Designer file simple).
        picCanvas.MouseDown += PicCanvas_MouseDown;
        picCanvas.MouseMove += PicCanvas_MouseMove;
        picCanvas.MouseUp += PicCanvas_MouseUp;

        btnEnd.Click += BtnEnd_Click;
        btnColor.Click += BtnColor_Click;
        btnClear.Click += BtnClear_Click;
        btnInsertImage.Click += BtnInsertImage_Click;

        foreach (var rb in new[] { rb1, rb2, rb3, rb4, rb5 })
            rb.CheckedChanged += Width_CheckedChanged;

        Load += WhiteboardForm_Load;
        FormClosing += WhiteboardForm_FormClosing;
    }

    // ---------- SignalR connection ----------

    private void WhiteboardForm_Load(object? sender, EventArgs e)
    {
        Text = $"Whiteboard - {_displayName} @ {_hubUrl}";
        lblStatus.Text = "Status: connecting...";

        // Pass the display name to the server (?name=...) so it appears on the admin dashboard.
        string url = _hubUrl + (_hubUrl.Contains('?') ? "&" : "?") + "name=" + Uri.EscapeDataString(_displayName);

        _connection = new HubConnectionBuilder()
            .WithUrl(url)
            .Build();

        // Incoming messages from the hub (run on a background thread -> marshal to UI).
        _connection.On<StrokeDto>("ReceiveStroke", s =>
            RunOnUi(() => DrawSegment(s.X1, s.Y1, s.X2, s.Y2, s.Width, Color.FromArgb(s.ColorArgb))));

        _connection.On<ImageDto>("ReceiveImage", img =>
            RunOnUi(() => DrawImage(img)));

        _connection.On("ReceiveClear", () =>
            RunOnUi(ClearCanvas));

        _connection.On("ReceiveEnd", () =>
            RunOnUi(SaveAndClose));

        _connection.On<int>("ClientCount", n =>
            RunOnUi(() =>
            {
                lblClients.Text = $"Connected clients: {n}";
                lblClients.Refresh(); // ép vẽ lại ngay, không chờ focus/idle (hết cảnh "phải bấm vào mới hiện")
            }));

        // The server sends LoadState only to clients it ACCEPTED.
        // Apply it only on the FIRST connect; on a later reconnect we KEEP the local board
        // so anything drawn while OFFLINE is not wiped out by the server's state.
        _connection.On<BoardOp[]>("LoadState", ops =>
        {
            _accepted = true;
            if (_loadedOnce) return;
            _loadedOnce = true;
            RunOnUi(() => LoadState(ops));
        });

        // The server sends this when the room is full and refuses our connection.
        _connection.On<string>("ConnectionRejected", msg =>
        {
            _rejected = true;
            _rejectMessage = msg;
        });

        // Start the connect/keep-alive loop (handles "room full" by waiting for a slot).
        _ = RunConnectionAsync();
    }

    /// <summary>
    /// Connects and keeps trying. If the server is full it rejects us, and we wait then
    /// retry until a slot frees up (someone leaves). Also reconnects if the link drops.
    /// </summary>
    private async Task RunConnectionAsync()
    {
        if (_connection is null) return;

        while (!_ending && !IsDisposed)
        {
            _accepted = false;
            _rejected = false;
            RunOnUi(() => lblStatus.Text = "Status: connecting...");

            try
            {
                await _connection.StartAsync();
            }
            catch (Exception)
            {
                // Server not reachable -> stay OFFLINE (local drawing still works), retry soon.
                RunOnUi(() => lblStatus.Text = "Status: OFFLINE - vẫn vẽ cục bộ được (chưa kết nối server, thử lại sau 3s)");
                await Task.Delay(3000);
                continue;
            }

            // Connected at the transport level. The server either accepts us (sends
            // LoadState) or rejects us (sends ConnectionRejected) within a moment.
            for (int i = 0; i < 20 && !_accepted && !_rejected; i++)
                await Task.Delay(100);

            if (_rejected)
            {
                string msg = _rejectMessage;
                RunOnUi(() => lblStatus.Text = "Status: " + msg);
                try { await _connection.StopAsync(); } catch { /* ignore */ }
                await Task.Delay(3000); // wait for someone to leave, then try again
                continue;
            }

            // Accepted - we are in the room.
            RunOnUi(() => lblStatus.Text = $"Status: ONLINE - đã kết nối {_hubUrl}");

            // Wait until the connection closes, then loop around to reconnect.
            var closed = new TaskCompletionSource();
            Task OnClosed(Exception? _) { closed.TrySetResult(); return Task.CompletedTask; }
            _connection.Closed += OnClosed;
            await closed.Task;
            _connection.Closed -= OnClosed;

            if (_ending || IsDisposed) break;
            RunOnUi(() => lblStatus.Text = "Status: OFFLINE - vẫn vẽ cục bộ được (mất kết nối, đang kết nối lại...)");
            await Task.Delay(1500);
        }
    }

    private void WhiteboardForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        // Stop the connect loop and clean up the connection on exit.
        _ending = true;
        try { _ = _connection?.DisposeAsync(); } catch { /* ignore */ }
    }

    /// <summary>Run an action on the UI thread (SignalR callbacks arrive on a pool thread).</summary>
    private void RunOnUi(Action action)
    {
        if (IsDisposed || Disposing) return;
        try
        {
            if (InvokeRequired) BeginInvoke(action);
            else action();
        }
        catch (ObjectDisposedException) { /* form closed while a message was in flight */ }
        catch (InvalidOperationException) { /* handle not created yet */ }
    }

    // ---------- Drawing primitives (always called on the UI thread) ----------

    private void DrawSegment(int x1, int y1, int x2, int y2, int width, Color color)
    {
        using var pen = new Pen(color, width)
        {
            StartCap = LineCap.Round,
            EndCap = LineCap.Round,
            LineJoin = LineJoin.Round,
        };
        _g.DrawLine(pen, x1, y1, x2, y2);
        picCanvas.Invalidate();
    }

    private void DrawImage(ImageDto dto)
    {
        try
        {
            byte[] bytes = Convert.FromBase64String(dto.DataBase64);
            using var ms = new MemoryStream(bytes);
            using var img = Image.FromStream(ms);
            _g.DrawImage(img, new Rectangle(dto.X, dto.Y, dto.Width, dto.Height));
            picCanvas.Invalidate();
        }
        catch
        {
            // Ignore a malformed image rather than tearing the board down.
        }
    }

    private void ClearCanvas()
    {
        _g.Clear(Color.White);
        picCanvas.Invalidate();
    }

    private void LoadState(BoardOp[] ops)
    {
        // LoadState is the authoritative current board: clear, then replay everything.
        ClearCanvas();
        foreach (var op in ops)
        {
            if (op.Type == "stroke" && op.Stroke is { } s)
                DrawSegment(s.X1, s.Y1, s.X2, s.Y2, s.Width, Color.FromArgb(s.ColorArgb));
            else if (op.Type == "image" && op.Image is { } img)
                DrawImage(img);
        }
    }

    // ---------- Mouse drawing ----------

    private void PicCanvas_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            _isDrawing = true;
            _lastPoint = e.Location;
        }
    }

    private void PicCanvas_MouseMove(object? sender, MouseEventArgs e)
    {
        if (!_isDrawing) return;

        Point p = e.Location;
        DrawSegment(_lastPoint.X, _lastPoint.Y, p.X, p.Y, _currentWidth, _currentColor);

        // Share this segment with the other clients (fire-and-forget).
        if (_connection?.State == HubConnectionState.Connected)
        {
            var dto = new StrokeDto
            {
                X1 = _lastPoint.X,
                Y1 = _lastPoint.Y,
                X2 = p.X,
                Y2 = p.Y,
                Width = _currentWidth,
                ColorArgb = _currentColor.ToArgb(),
            };
            _ = SafeSendAsync("BroadcastStroke", dto);
        }

        _lastPoint = p;
    }

    private void PicCanvas_MouseUp(object? sender, MouseEventArgs e) => _isDrawing = false;

    // ---------- Toolbar ----------

    private void BtnColor_Click(object? sender, EventArgs e)
    {
        using var dlg = new ColorDialog { Color = _currentColor, FullOpen = true };
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            _currentColor = dlg.Color;
            pnlColorPreview.BackColor = _currentColor;
        }
    }

    private void Width_CheckedChanged(object? sender, EventArgs e)
    {
        if (sender is RadioButton { Checked: true } rb && int.TryParse(rb.Text, out int w))
            _currentWidth = w;
    }

    private void BtnClear_Click(object? sender, EventArgs e)
    {
        ClearCanvas();
        _ = SafeSendAsync("ClearBoard");
    }

    private async void BtnInsertImage_Click(object? sender, EventArgs e)
    {
        using var dlg = new InsertImageForm();
        if (dlg.ShowDialog(this) != DialogResult.OK) return;

        try
        {
            using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(20) };
            http.DefaultRequestHeaders.UserAgent.ParseAdd("WhiteboardClient/1.0");
            byte[] data = await http.GetByteArrayAsync(dlg.ImageUrl);

            using var ms = new MemoryStream(data);
            using var original = Image.FromStream(ms);

            // Decide the target size: use the values the user typed (0 = keep original),
            // then clamp so the image always fits on the canvas.
            int targetW = dlg.ImgWidth > 0 ? dlg.ImgWidth : original.Width;
            int targetH = dlg.ImgHeight > 0 ? dlg.ImgHeight : original.Height;
            targetW = Math.Clamp(targetW, 1, _bitmap.Width);
            targetH = Math.Clamp(targetH, 1, _bitmap.Height);

            // Render to a normalised PNG so every client gets the same bytes.
            using var resized = new Bitmap(targetW, targetH);
            using (var g = Graphics.FromImage(resized))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(original, 0, 0, targetW, targetH);
            }

            using var outMs = new MemoryStream();
            resized.Save(outMs, ImageFormat.Png);
            string base64 = Convert.ToBase64String(outMs.ToArray());

            int x = Math.Max(0, (_bitmap.Width - targetW) / 2);
            int y = Math.Max(0, (_bitmap.Height - targetH) / 2);

            var dto = new ImageDto { X = x, Y = y, Width = targetW, Height = targetH, DataBase64 = base64 };

            DrawImage(dto);                          // show locally
            await SafeSendAsync("BroadcastImage", dto); // share with the others
        }
        catch (Exception ex)
        {
            MessageBox.Show("Could not load that image:\n" + ex.Message,
                "Insert image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private async void BtnEnd_Click(object? sender, EventArgs e)
    {
        // Ask the server to end the session for everyone. Each client (including this one)
        // receives "ReceiveEnd", saves its board and closes. If we are offline, just end here.
        if (_connection?.State == HubConnectionState.Connected)
        {
            try
            {
                await _connection.SendAsync("EndSession");
                return;
            }
            catch
            {
                // fall through to local save
            }
        }
        SaveAndClose();
    }

    // ---------- End / save ----------

    private void SaveAndClose()
    {
        if (_ending || _savePromptOpen) return;

        // Let the user choose WHERE to save the board (PNG or JPG), then close.
        _savePromptOpen = true;
        try
        {
            using var dlg = new SaveFileDialog
            {
                Title = "Lưu ảnh Whiteboard",
                FileName = $"Whiteboard_{Sanitize(_displayName)}_{DateTime.Now:yyyyMMdd_HHmmss}.png",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                Filter = "PNG image (*.png)|*.png|JPEG image (*.jpg)|*.jpg",
                DefaultExt = "png",
                AddExtension = true,
                OverwritePrompt = true,
            };

            // Cancel -> do NOT close; let the user keep drawing.
            if (dlg.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                SaveBoardImageTo(dlg.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Không lưu được ảnh:\n" + ex.Message,
                    "Lưu thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // saving failed -> stay open so the user can retry
            }
        }
        finally
        {
            _savePromptOpen = false;
        }

        // Saved successfully -> close the form -> Application.Run returns -> program exits.
        _ending = true;
        BeginInvoke(new Action(Close));
    }

    /// <summary>
    /// Saves the current board to the path the user chose. Saves a COPY of the bitmap (the
    /// live one is held by the PictureBox and an open Graphics, which can make GDI+ Save
    /// throw). The image format follows the extension (.jpg/.jpeg -> JPEG, otherwise PNG).
    /// </summary>
    private void SaveBoardImageTo(string path)
    {
        _g.Flush();
        ImageFormat format = Path.GetExtension(path).ToLowerInvariant() switch
        {
            ".jpg" or ".jpeg" => ImageFormat.Jpeg,
            _ => ImageFormat.Png,
        };

        string? dir = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(dir))
            Directory.CreateDirectory(dir);

        using var copy = new Bitmap(_bitmap); // clone -> no GDI+ lock on the live bitmap
        copy.Save(path, format);
    }

    private static string Sanitize(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
            name = name.Replace(c, '_');
        name = name.Trim();
        return name.Length == 0 ? "client" : name;
    }

    // ---------- Helpers ----------

    private async Task SafeSendAsync(string method, params object?[] args)
    {
        if (_connection?.State != HubConnectionState.Connected) return;
        try
        {
            await _connection.SendCoreAsync(method, args);
        }
        catch
        {
            // A dropped message during a transient disconnect is acceptable for the lab.
        }
    }
}
