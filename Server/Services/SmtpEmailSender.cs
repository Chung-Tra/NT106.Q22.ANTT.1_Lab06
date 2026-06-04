using System.Net;
using System.Net.Mail;

namespace Server.Services;

/// <summary>
/// SMTP implementation of <see cref="IEmailSender"/>. Reads its configuration from the
/// "Smtp" section of appsettings.json. If SMTP is not configured the alert is skipped
/// (logged as a warning) instead of crashing the server.
///
/// Note: System.Net.Mail.SmtpClient is the simplest built-in option and is fine for a
/// lab. For production, Microsoft recommends MailKit.
/// </summary>
public class SmtpEmailSender : IEmailSender
{
    private readonly IConfiguration _config;
    private readonly ILogger<SmtpEmailSender> _logger;

    public SmtpEmailSender(IConfiguration config, ILogger<SmtpEmailSender> logger)
    {
        _config = config;
        _logger = logger;
    }

    public async Task SendAdminAlertAsync(int currentCount, int threshold)
    {
        IConfigurationSection smtp = _config.GetSection("Smtp");
        string host = smtp["Host"] ?? string.Empty;
        string admin = smtp["AdminEmail"] ?? string.Empty;

        if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(admin))
        {
            _logger.LogWarning(
                "SMTP not configured (Smtp:Host / Smtp:AdminEmail are empty). Skipping admin alert. " +
                "Connected clients = {Count} (threshold {Threshold}).",
                currentCount, threshold);
            return;
        }

        try
        {
            int port = int.TryParse(smtp["Port"], out int p) ? p : 587;
            bool enableSsl = !bool.TryParse(smtp["EnableSsl"], out bool ssl) || ssl; // default true
            string user = smtp["User"] ?? string.Empty;
            string password = smtp["Password"] ?? string.Empty;
            string from = string.IsNullOrWhiteSpace(smtp["From"]) ? user : smtp["From"]!;

            using var message = new MailMessage(from, admin)
            {
                Subject = $"[Whiteboard] Client limit reached: {currentCount}/{threshold}",
                Body =
                    $"The whiteboard server now has {currentCount} connected client(s), " +
                    $"which reached the configured limit of {threshold}.\r\n" +
                    $"Time: {DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss zzz}",
            };

            using var client = new SmtpClient(host, port)
            {
                EnableSsl = enableSsl,
                Credentials = string.IsNullOrWhiteSpace(user)
                    ? CredentialCache.DefaultNetworkCredentials
                    : new NetworkCredential(user, password),
            };

            await client.SendMailAsync(message);
            _logger.LogInformation("Admin alert email sent to {Admin}.", admin);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send admin alert email.");
        }
    }
}
