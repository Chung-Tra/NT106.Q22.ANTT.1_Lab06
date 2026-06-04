using System.Net.Http;

namespace Admin;

/// <summary>
/// Desktop admin dashboard. Polls the server's /admin/state feed every 1.5s (plain
/// HTTP - it does NOT use a hub connection, so it never takes one of the 5 client
/// slots) and shows the live count, the clients in the room, and the notification log.
/// </summary>
public partial class AdminForm : Form
{
    private readonly HttpClient _http = new() { Timeout = TimeSpan.FromSeconds(5) };
    private readonly System.Windows.Forms.Timer _timer = new() { Interval = 1500 };
    private string _baseUrl = string.Empty;
    private bool _busy;

    public AdminForm()
    {
        InitializeComponent();
        ConfigureGrids();

        btnConnect.Click += (_, _) => DoConnect();
        txtServer.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; DoConnect(); }
        };
        _timer.Tick += async (_, _) => await RefreshAsync();
        FormClosing += (_, _) => { _timer.Stop(); _http.Dispose(); };
    }

    /// <summary>Lets tests (or callers) connect programmatically.</summary>
    public void ConnectTo(string server)
    {
        txtServer.Text = server;
        DoConnect();
    }

    private void DoConnect()
    {
        try
        {
            _baseUrl = AdminApi.NormalizeBaseUrl(txtServer.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Invalid server address: " + ex.Message, "Admin",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        lblConn.Text = "Connecting to " + _baseUrl + " ...";
        lblConn.ForeColor = Color.Gray;
        _timer.Start();
        _ = RefreshAsync(); // refresh immediately, don't wait for the first tick
    }

    private async Task RefreshAsync()
    {
        if (_busy || string.IsNullOrEmpty(_baseUrl)) return;
        _busy = true;
        try
        {
            AdminState state = await AdminApi.FetchAsync(_http, _baseUrl);
            Render(state);
            lblConn.Text = "Connected to " + _baseUrl;
            lblConn.ForeColor = Color.ForestGreen;
        }
        catch (Exception ex)
        {
            lblConn.Text = "Offline (" + ex.GetType().Name + ") - retrying...";
            lblConn.ForeColor = Color.Firebrick;
        }
        finally
        {
            _busy = false;
        }
    }

    private void Render(AdminState s)
    {
        lblCount.Text = $"{s.Count} / {s.Max}";
        bool full = s.Max > 0 && s.Count >= s.Max;
        lblBadge.Text = full ? "FULL - new connections blocked" : "OPEN";
        lblBadge.BackColor = full ? Color.Firebrick : Color.ForestGreen;
        lblBadge.ForeColor = Color.White;

        dgvClients.SuspendLayout();
        dgvClients.Rows.Clear();
        for (int i = 0; i < s.Clients.Length; i++)
        {
            ClientInfo c = s.Clients[i];
            dgvClients.Rows.Add((i + 1).ToString(), c.Name, c.JoinedAt, c.ConnectionId);
        }
        dgvClients.ResumeLayout();

        dgvEvents.SuspendLayout();
        dgvEvents.Rows.Clear();
        foreach (AdminEvent e in s.Events) // server returns newest-first
        {
            int idx = dgvEvents.Rows.Add(e.Time, e.Level.ToUpperInvariant(), e.Message);
            dgvEvents.Rows[idx].DefaultCellStyle.ForeColor = e.Level switch
            {
                "error" => Color.Firebrick,
                "warn" => Color.DarkOrange,
                _ => Color.Black,
            };
        }
        dgvEvents.ResumeLayout();
    }

    private void ConfigureGrids()
    {
        dgvClients.Columns.Clear();
        dgvClients.Columns.Add("num", "#");
        dgvClients.Columns.Add("name", "Name");
        dgvClients.Columns.Add("joined", "Joined");
        dgvClients.Columns.Add("conn", "Connection ID");
        dgvClients.Columns[0].Width = 40;
        dgvClients.Columns[1].Width = 170;
        dgvClients.Columns[2].Width = 90;
        dgvClients.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        dgvEvents.Columns.Clear();
        dgvEvents.Columns.Add("time", "Time");
        dgvEvents.Columns.Add("level", "Level");
        dgvEvents.Columns.Add("msg", "Message");
        dgvEvents.Columns[0].Width = 90;
        dgvEvents.Columns[1].Width = 70;
        dgvEvents.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    }
}
