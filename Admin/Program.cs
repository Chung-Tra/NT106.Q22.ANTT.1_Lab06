namespace Admin;

internal static class Program
{
    /// <summary>The main entry point for the admin application.</summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new AdminForm());
    }
}
