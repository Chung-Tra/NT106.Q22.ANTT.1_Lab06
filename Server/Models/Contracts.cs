namespace Server.Models;

/// <summary>
/// One drawn line segment (from point 1 to point 2) with its pen settings.
/// Coordinates are in the fixed logical canvas space that every client shares,
/// so a stroke drawn on one client lands in the same place on all others.
/// </summary>
public class StrokeDto
{
    public int X1 { get; set; }
    public int Y1 { get; set; }
    public int X2 { get; set; }
    public int Y2 { get; set; }

    /// <summary>Pen thickness (1..5).</summary>
    public int Width { get; set; }

    /// <summary>Pen colour stored as ARGB (System.Drawing.Color.ToArgb()).</summary>
    public int ColorArgb { get; set; }
}

/// <summary>
/// An image inserted onto the board. The bytes are a PNG encoded as Base64 so the
/// picture travels through the hub to every client (including late joiners).
/// </summary>
public class ImageDto
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public string DataBase64 { get; set; } = string.Empty;
}

/// <summary>
/// A single operation in the board history. The server keeps the ordered list of
/// operations so a newly connected client can rebuild the exact current board.
/// </summary>
public class BoardOp
{
    /// <summary>"stroke" or "image".</summary>
    public string Type { get; set; } = string.Empty;
    public StrokeDto? Stroke { get; set; }
    public ImageDto? Image { get; set; }
}

// ----- Admin dashboard data -----

/// <summary>A currently connected drawing client, shown on the admin dashboard.</summary>
public class ClientInfo
{
    public string ConnectionId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string JoinedAt { get; set; } = string.Empty;
}

/// <summary>One line in the admin notification log.</summary>
public class AdminEvent
{
    public string Time { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty; // info | warn | error
    public string Message { get; set; } = string.Empty;
}

/// <summary>Snapshot returned to the admin dashboard each time it polls.</summary>
public class AdminState
{
    public int Count { get; set; }
    public int Max { get; set; }
    public ClientInfo[] Clients { get; set; } = System.Array.Empty<ClientInfo>();
    public AdminEvent[] Events { get; set; } = System.Array.Empty<AdminEvent>();
}
