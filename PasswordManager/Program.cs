using PasswordManager.WinFormsUI;

namespace PasswordManager;

/// <summary>
/// The main entry point for the application.
/// Handles pre-launch cleanup and opens the login window.
/// </summary>
static class Program
{
    /// <summary>
    /// Application startup method.
    /// Deletes all temporary .csv files from previous sessions.
    /// Initializes application settings.
    /// Launches the login form.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // clean up temporary CSV files left from unexpected shutdowns
        foreach (var file in Directory.GetFiles(".", "*.csv"))
        {
            try { File.Delete(file); } catch {}
        }
        
        ApplicationConfiguration.Initialize();
        Application.Run(new LoginForm());
    }
}