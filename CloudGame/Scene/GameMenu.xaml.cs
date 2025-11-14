using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CloudGame.Scene
{
    /// <summary>
    /// Interaction logic for GameMenu.xaml
    /// </summary>
    public partial class GameMenu : Window
    {
        public GameMenu()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
        }
        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            GameScene gameScene = new GameScene();
            gameScene.Show();
            this.Close();
        }
    }
}
