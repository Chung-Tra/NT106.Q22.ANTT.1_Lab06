namespace Client;

/// <summary>
/// The login / connection screen. The user types the server address (localhost, an
/// IP:PORT, or a full URL) and a display name. <see cref="HubUrl"/> holds the full
/// SignalR hub URL once the user clicks Connect.
/// </summary>
public partial class ConnectForm : Form
{
    public string HubUrl { get; private set; } = string.Empty;
    public string DisplayName { get; private set; } = "User";

    public ConnectForm()
    {
        InitializeComponent();
        btnConnect.Click += BtnConnect_Click;
    }

    private void BtnConnect_Click(object? sender, EventArgs e)
    {
        string raw = txtServer.Text.Trim();
        if (raw.Length == 0)
        {
            MessageBox.Show(
                "Please enter the server address, e.g. localhost or 192.168.1.10:5000.",
                "Missing server", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            HubUrl = BuildHubUrl(raw);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Invalid server address: " + ex.Message,
                "Invalid address", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        DisplayName = string.IsNullOrWhiteSpace(txtName.Text) ? "User" : txtName.Text.Trim();
        DialogResult = DialogResult.OK;
        Close();
    }

    /// <summary>
    /// Turns whatever the user typed into a full SignalR hub URL.
    /// Accepts: "localhost", "localhost:5000", "192.168.1.10:5000",
    ///          "http://host:5000", "http://host:5000/whiteboard".
    /// Defaults: scheme = http, port = 5000, path = /whiteboard.
    /// </summary>
    public static string BuildHubUrl(string input)
    {
        const int defaultPort = 5000;
        const string hubPath = "/whiteboard";

        string scheme = "http://";
        string rest = input.Trim();

        int schemeIdx = rest.IndexOf("://", StringComparison.OrdinalIgnoreCase);
        if (schemeIdx >= 0)
        {
            scheme = rest.Substring(0, schemeIdx + 3);
            rest = rest.Substring(schemeIdx + 3);
        }

        rest = rest.TrimEnd('/');
        if (rest.Length == 0)
            throw new ArgumentException("the host part is empty");

        string authority;
        string path;
        int slash = rest.IndexOf('/');
        if (slash >= 0)
        {
            authority = rest.Substring(0, slash);
            path = rest.Substring(slash); // starts with '/'
        }
        else
        {
            authority = rest;
            path = string.Empty;
        }

        // Add the default port when the user gave only a host (skip IPv6 literals "[..]").
        if (!authority.StartsWith('[') && !authority.Contains(':'))
            authority += ":" + defaultPort;

        if (string.IsNullOrEmpty(path) || path == "/")
            path = hubPath;

        string url = scheme + authority + path;
        _ = new Uri(url); // validate; throws if malformed
        return url;
    }
}
