using Wisej.Web;

namespace AddressManager
{
    internal static class App
    {
        [STAThread]
        static void Main()
        {
            Application.MainForm = new MainForm();
            Application.Run();
        }
    }
}