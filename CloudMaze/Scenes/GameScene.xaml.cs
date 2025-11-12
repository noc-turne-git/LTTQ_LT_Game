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
using CloudMaze.Asset;

namespace CloudMaze.Scenes
{
    /// <summary>
    /// Interaction logic for GameScene.xaml
    /// </summary>
    public partial class GameScene : Window
    {
        public GameScene()
        {
            InitializeComponent();
            var mazeImage = AssetService.GetImage("Enemy/VirusYellow");
            var imgControl = new Image
            {
                Source = mazeImage,
                Width = 100,
                Height = 100
            };
            this.Content = imgControl;
        }
        void DownloadAsset ()
        {
            this.Foreground = new SolidColorBrush(Color.FromRgb(176, 226, 255));
            var mazeImage = AssetService.GetImage("Scenes/Maze");
            var imgControl = new Image
            {
                Source = mazeImage,
                Width = 512,
                Height = 512
            };
            this.Content = imgControl;
        }
    }
}
