namespace EconomyMonitor.Wpf;

/// <summary>
/// Application entry point class.
/// </summary>
internal sealed class Program
{
    private Program() { }

    /// <summary>
    /// Application entry point.
    /// </summary>
    [STAThread]
    public static void Main()
    {
        App app = new();
        app.InitializeComponent();
        app.Run();
    }
}
