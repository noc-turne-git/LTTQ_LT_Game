using System.Windows;
using CloudGame.Core;

namespace CloudGame.Scene
{
    /// <summary>
    /// Interaction logic for GameScene.xaml
    /// </summary>
    public partial class GameScene : Window
    {
        public GameHost gameHost { get;}
        public GameScene()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;

            gameHost = new GameHost(this);
            this.Content = gameHost;
    
        }
    }
}
