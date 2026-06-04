namespace Admin;

// These mirror the JSON returned by the server's GET /admin/state endpoint.
// (Property names are matched case-insensitively, so server camelCase maps fine.)

public class AdminState
{
    public int Count { get; set; }
    public int Max { get; set; }
    public ClientInfo[] Clients { get; set; } = Array.Empty<ClientInfo>();
    public AdminEvent[] Events { get; set; } = Array.Empty<AdminEvent>();
}

public class ClientInfo
{
    public string ConnectionId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string JoinedAt { get; set; } = string.Empty;
}

public class AdminEvent
{
    public string Time { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty; // info | warn | error
    public string Message { get; set; } = string.Empty;
}
