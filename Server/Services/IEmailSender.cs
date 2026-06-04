namespace Server.Services;

/// <summary>Sends the administrator alert when the connected-client limit is reached.</summary>
public interface IEmailSender
{
    Task SendAdminAlertAsync(int currentCount, int threshold);
}
