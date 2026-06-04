using Server.Models;

namespace Server.Services;

/// <summary>
/// Shared, thread-safe whiteboard state (singleton). Tracks the board history, the
/// set of connected clients (with a hard cap), and a rolling admin notification log.
/// </summary>
public class WhiteboardState
{
    private readonly object _gate = new();
    private readonly List<BoardOp> _ops = new();
    private readonly Dictionary<string, ClientInfo> _clients = new();
    private readonly List<AdminEvent> _events = new();
    private const int MaxEvents = 100;

    // ---------- connected clients (hard cap) ----------

    /// <summary>
    /// Atomically tries to take a slot. Returns ok=false (without adding) when the
    /// room already holds <paramref name="max"/> clients.
    /// </summary>
    public (bool ok, int count) TryAddClient(string connectionId, string name, int max)
    {
        lock (_gate)
        {
            if (_clients.Count >= max)
                return (false, _clients.Count);

            _clients[connectionId] = new ClientInfo
            {
                ConnectionId = connectionId,
                Name = name,
                JoinedAt = DateTimeOffset.Now.ToString("HH:mm:ss"),
            };
            return (true, _clients.Count);
        }
    }

    /// <summary>Releases a slot. removed=false if this id never held one (e.g. a rejected attempt).</summary>
    public (bool removed, int count, string name) RemoveClient(string connectionId)
    {
        lock (_gate)
        {
            if (_clients.TryGetValue(connectionId, out var info))
            {
                _clients.Remove(connectionId);
                return (true, _clients.Count, info.Name);
            }
            return (false, _clients.Count, string.Empty);
        }
    }

    public int ClientCount { get { lock (_gate) { return _clients.Count; } } }

    // ---------- admin notification log (ring buffer) ----------

    public void AddEvent(string level, string message)
    {
        lock (_gate)
        {
            _events.Add(new AdminEvent
            {
                Time = DateTimeOffset.Now.ToString("HH:mm:ss"),
                Level = level,
                Message = message,
            });
            if (_events.Count > MaxEvents)
                _events.RemoveAt(0);
        }
    }

    /// <summary>The snapshot the admin dashboard polls for.</summary>
    public AdminState GetAdminState(int max)
    {
        lock (_gate)
        {
            var events = _events.AsEnumerable().Reverse().ToArray(); // newest first
            return new AdminState
            {
                Count = _clients.Count,
                Max = max,
                Clients = _clients.Values.ToArray(),
                Events = events,
            };
        }
    }

    // ---------- board operations (history replay) ----------

    public void AddOp(BoardOp op) { lock (_gate) { _ops.Add(op); } }
    public void ClearOps() { lock (_gate) { _ops.Clear(); } }
    public BoardOp[] Snapshot() { lock (_gate) { return _ops.ToArray(); } }
}
