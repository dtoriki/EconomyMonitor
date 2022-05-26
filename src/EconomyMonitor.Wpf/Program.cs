namespace EconomyMonitor.Wpf;

internal sealed class Program
{
    private Program() { }

    [STAThread]
    public static void Main()
    {
        App app = new();
        app.InitializeComponent();
        app.Run();
    }
}
