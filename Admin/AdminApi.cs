using System.Net.Http.Json;
using System.Text.Json;

namespace Admin;

/// <summary>Talks to the server's /admin/state JSON feed.</summary>
public static class AdminApi
{
    private static readonly JsonSerializerOptions JsonOpts = new() { PropertyNameCaseInsensitive = true };

    /// <summary>Fetches the current admin snapshot from "{baseUrl}/admin/state".</summary>
    public static async Task<AdminState> FetchAsync(HttpClient http, string baseUrl, CancellationToken ct = default)
    {
        string url = baseUrl.TrimEnd('/') + "/admin/state";
        AdminState? state = await http.GetFromJsonAsync<AdminState>(url, JsonOpts, ct);
        return state ?? new AdminState();
    }

    /// <summary>
    /// Turns "localhost", "localhost:5000", "192.168.1.10:5000" or a full URL into a
    /// base URL like "http://192.168.1.10:5000" (default scheme http, default port 5000).
    /// </summary>
    public static string NormalizeBaseUrl(string input)
    {
        const int defaultPort = 5000;

        string scheme = "http://";
        string rest = input.Trim();

        int schemeIdx = rest.IndexOf("://", StringComparison.OrdinalIgnoreCase);
        if (schemeIdx >= 0)
        {
            scheme = rest[..(schemeIdx + 3)];
            rest = rest[(schemeIdx + 3)..];
        }

        rest = rest.TrimEnd('/');

        int slash = rest.IndexOf('/');
        string authority = slash >= 0 ? rest[..slash] : rest;
        if (authority.Length == 0)
            throw new ArgumentException("the host part is empty");

        if (!authority.StartsWith('[') && !authority.Contains(':'))
            authority += ":" + defaultPort;

        string url = scheme + authority;
        _ = new Uri(url); // validate
        return url;
    }
}
