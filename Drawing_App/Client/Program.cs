namespace Client;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    ///  Shows the connect screen first; only opens the whiteboard if the user connects.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        using var connect = new ConnectForm();
        if (connect.ShowDialog() != DialogResult.OK)
            return;

        Application.Run(new WhiteboardForm(connect.HubUrl, connect.DisplayName));
    }
}
