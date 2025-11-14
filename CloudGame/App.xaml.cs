using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace CloudGame
{
    public partial class App : Application
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Tạo console để debug
            AllocConsole();
            Console.WriteLine("Game started!");
        }
    }
}
