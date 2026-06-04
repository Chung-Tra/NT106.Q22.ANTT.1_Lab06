using Server.Hubs;
using Server.Services;

var builder = WebApplication.CreateBuilder(args);

// --- SignalR (the realtime "hub") ---
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = builder.Environment.IsDevelopment();
    // Allow fairly large inbound messages so clients can insert images from the Internet.
    options.MaximumReceiveMessageSize = 16 * 1024 * 1024; // 16 MB
});

// Permissive CORS - harmless for the WinForms client, handy if a browser client is added later.
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy => policy
        .SetIsOriginAllowed(_ => true)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()));

// Shared board state + admin email alert.
builder.Services.AddSingleton<WhiteboardState>();
builder.Services.AddSingleton<IEmailSender, SmtpEmailSender>();

var app = builder.Build();

app.UseCors();

// Simple landing page so you can confirm the server is up from a browser.
app.MapGet("/", () => Results.Text(
    "Whiteboard SignalR server is running.\n" +
    "Hub endpoint:    /whiteboard\n" +
    "Admin data feed: /admin/state  (consumed by the Admin desktop app)\n" +
    "Connect a client with this machine's address, e.g. localhost:5000 or 192.168.x.x:5000"));

// --- Admin data feed (JSON) consumed by the Admin WinForms app ---
app.MapGet("/admin/state", (WhiteboardState state, IConfiguration cfg) =>
    Results.Json(state.GetAdminState(MaxClients(cfg))));

// The hub the clients connect to.
app.MapHub<WhiteboardHub>("/whiteboard");

// When the server has started, print the exact addresses clients should use.
app.Lifetime.ApplicationStarted.Register(() => PrintConnectionInfo(builder.Configuration));

app.Run();

// Hard cap on connected clients (accepts the new key, falls back to the legacy one).
static int MaxClients(IConfiguration config) =>
    config.GetValue<int?>("Whiteboard:MaxClients")
    ?? config.GetValue<int?>("Whiteboard:ClientAlertThreshold")
    ?? 5;

// Prints "localhost:PORT" and every LAN IPv4 address of this machine, so you can
// see exactly what to type on the client's Connect screen.
static void PrintConnectionInfo(IConfiguration config)
{
    string url = config["Kestrel:Endpoints:Http:Url"] ?? "http://0.0.0.0:5000";
    int port = Uri.TryCreate(url, UriKind.Absolute, out var u) ? u.Port : 5000;
    int max = MaxClients(config);

    var lanIps = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
        .Where(ni => ni.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up
                     && ni.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Loopback)
        .SelectMany(ni => ni.GetIPProperties().UnicastAddresses)
        .Select(a => a.Address)
        .Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
                     && !System.Net.IPAddress.IsLoopback(ip)
                     && !ip.ToString().StartsWith("169.254.")) // skip auto-config (APIPA)
        .Select(ip => ip.ToString())
        .Distinct()
        .ToList();

    string bar = new string('=', 60);
    Console.WriteLine();
    Console.WriteLine(bar);
    Console.WriteLine("  WHITEBOARD SERVER IS RUNNING");
    Console.WriteLine(bar);
    Console.WriteLine($"  Port: {port}   |   Hub path: /whiteboard   |   Max clients: {max}");
    Console.WriteLine("  Enter ONE of these on the client's Connect screen:");
    Console.WriteLine($"     - Same machine :  localhost:{port}");
    if (lanIps.Count == 0)
        Console.WriteLine("     - LAN          :  (no IPv4 LAN adapter detected)");
    else
        foreach (string ip in lanIps)
            Console.WriteLine($"     - LAN          :  {ip}:{port}");
    Console.WriteLine();
    Console.WriteLine("  Admin app: run the 'Admin' project and connect to one of the");
    Console.WriteLine("  addresses above (the app reads /admin/state).");
    Console.WriteLine(bar);
    Console.WriteLine();
}
