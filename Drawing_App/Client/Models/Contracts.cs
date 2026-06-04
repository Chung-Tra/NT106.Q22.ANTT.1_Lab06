namespace Client.Models;

/// <summary>
/// One drawn line segment with its pen settings. Must match the server's StrokeDto
/// (same property names) so SignalR can serialise it across the wire.
/// </summary>
public class StrokeDto
{
    public int X1 { get; set; }
    public int Y1 { get; set; }
    public int X2 { get; set; }
    public int Y2 { get; set; }
    public int Width { get; set; }
    public int ColorArgb { get; set; }
}

/// <summary>An image inserted onto the board (PNG bytes as Base64).</summary>
public class ImageDto
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public string DataBase64 { get; set; } = string.Empty;
}

/// <summary>One entry of the board history replayed to clients that join late.</summary>
public class BoardOp
{
    public string Type { get; set; } = string.Empty; // "stroke" or "image"
    public StrokeDto? Stroke { get; set; }
    public ImageDto? Image { get; set; }
}
