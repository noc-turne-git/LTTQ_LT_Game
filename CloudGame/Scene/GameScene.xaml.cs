using CloudGame.Asset;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using CloudGame.Core;

namespace CloudGame.Scene
{
    /// <summary>
    /// Interaction logic for GameScene.xaml
    /// </summary>
    public partial class GameScene : Window
    {
        public GameScene()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            var gameHost = new GameHost(); // tạo UI ảo
            this.Content = gameHost; // gán UI ảo vào Content của Window
        }

        
    }
}
