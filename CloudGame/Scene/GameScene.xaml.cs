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
        private GameMenu gameMenu;
        public GameScene(GameMenu menu)
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;

            gameMenu = menu;
            gameHost = new GameHost(this, menu);
            this.Content = gameHost;
    
        }
    }
}
