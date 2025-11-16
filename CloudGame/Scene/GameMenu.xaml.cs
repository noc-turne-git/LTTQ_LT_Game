using System.Windows;

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
            GameScene gameScene = new GameScene(this);
            gameScene.Show();
            this.Hide();
        }
    }
}
