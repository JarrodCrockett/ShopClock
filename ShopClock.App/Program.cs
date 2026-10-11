using ShopClock.Core;

namespace ShopClock.App
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ShopClock",
                "punches.csv");
            var store = new PunchStore(path);
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm(store));
        }
    }
}