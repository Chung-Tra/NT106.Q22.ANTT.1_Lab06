using Microsoft.AspNetCore.SignalR;
using Server.Models;
using Server.Services;

namespace Server.Hubs;

/// <summary>
/// The realtime whiteboard hub. Enforces a hard cap on connected clients: when the
/// room is full a new connection is rejected, and only once an existing client leaves
/// can a waiting client take the freed slot.
/// </summary>
public class WhiteboardHub : Hub
{
    private readonly WhiteboardState _state;
    private readonly IEmailSender _email;
    private readonly IConfiguration _config;
    private readonly ILogger<WhiteboardHub> _logger;

    public WhiteboardHub(
        WhiteboardState state,
        IEmailSender email,
        IConfiguration config,
        ILogger<WhiteboardHub> logger)
    {
        _state = state;
        _email = email;
        _config = config;
        _logger = logger;
    }

    private int MaxClients =>
        _config.GetValue<int?>("Whiteboard:MaxClients")
        ?? _config.GetValue<int?>("Whiteboard:ClientAlertThreshold") // legacy key name
        ?? 5;

    public override async Task OnConnectedAsync()
    {
        int max = MaxClients;

        // Display name passed by the client as ?name=... on the hub URL.
        string name = Context.GetHttpContext()?.Request.Query["name"].ToString() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(name))
            name = "Client-" + Context.ConnectionId[..Math.Min(4, Context.ConnectionId.Length)];

        var (ok, count) = _state.TryAddClient(Context.ConnectionId, name, max);

        if (!ok)
        {
            // Room is full -> reject this connection.
            _state.AddEvent("error", $"REJECTED \"{name}\": room is full ({max}/{max}).");
            _logger.LogWarning("Rejected {Id} (\"{Name}\"): room full ({Max}/{Max}).", Context.ConnectionId, name, max, max);

            // Tell the client why, then drop the connection.
            await Clients.Caller.SendAsync("ConnectionRejected",
                $"Room is full ({max}/{max}). Waiting for a free slot...");
            Context.Abort();
            return;
        }

        _state.AddEvent("info", $"\"{name}\" joined ({count}/{max}).");
        _logger.LogInformation("Client \"{Name}\" connected {Id}. {Count}/{Max}.", name, Context.ConnectionId, count, max);

        // Replay the current board so the new client matches everyone else.
        await Clients.Caller.SendAsync("LoadState", _state.Snapshot());
        // Tell everyone the up-to-date connected-client count.
        await Clients.All.SendAsync("ClientCount", count);

        if (count == max)
        {
            _state.AddEvent("warn", $"LIMIT REACHED: {count}/{max}. Further connections will be blocked.");
            _logger.LogWarning("Connected-client limit reached {Count}/{Max} - alerting admin.", count, max);
            _ = _email.SendAdminAlertAsync(count, max); // optional email (skipped if SMTP not configured)
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var (removed, count, name) = _state.RemoveClient(Context.ConnectionId);
        if (removed)
        {
            int max = MaxClients;
            _state.AddEvent("info", $"\"{name}\" left ({count}/{max}). A slot is now free.");
            _logger.LogInformation("Client \"{Name}\" disconnected {Id}. {Count}/{Max}.", name, Context.ConnectionId, count, max);
            await Clients.All.SendAsync("ClientCount", count);
        }
        await base.OnDisconnectedAsync(exception);
    }

    /// <summary>Relay one drawn segment to the other clients and store it in history.</summary>
    public async Task BroadcastStroke(StrokeDto stroke)
    {
        _state.AddOp(new BoardOp { Type = "stroke", Stroke = stroke });
        await Clients.Others.SendAsync("ReceiveStroke", stroke);
    }

    /// <summary>Relay an inserted image to the other clients and store it in history.</summary>
    public async Task BroadcastImage(ImageDto image)
    {
        _state.AddOp(new BoardOp { Type = "image", Image = image });
        await Clients.Others.SendAsync("ReceiveImage", image);
    }

    /// <summary>Wipe the board for everyone and reset the stored history.</summary>
    public async Task ClearBoard()
    {
        _state.ClearOps();
        await Clients.Others.SendAsync("ReceiveClear");
    }

    /// <summary>
    /// End the session for everyone. Each client (including the sender) saves the current
    /// board to an image file and closes itself.
    /// </summary>
    public async Task EndSession()
    {
        _state.AddEvent("warn", "Session ended by a client. All boards saved & closed.");
        _logger.LogInformation("End requested by {Id}.", Context.ConnectionId);
        await Clients.All.SendAsync("ReceiveEnd");
    }
}
