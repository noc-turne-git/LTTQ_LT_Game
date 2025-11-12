using System.Configuration;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows;

namespace CloudMaze
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        protected override void OnStartup(System.Windows.StartupEventArgs e)
        {
            base.OnStartup(e);

            AllocConsole(); // Mở console
            Console.WriteLine("Console đã mở!");

          /*  var window = new CloudGame.Scene.GameScene();
            window.Show();*/
        }
    }

}
