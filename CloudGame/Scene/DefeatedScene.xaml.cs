using System.Windows;
using System.Windows.Media.Imaging;

namespace CloudGame.Scene
{
    /// <summary>
    /// Interaction logic for DefeatedScene.xaml
    /// </summary>
    public partial class DefeatedScene : Window
    {
        private Random rnd = new Random();
        private static int CountImage = 4;
        private GameMenu gameMenu;
        public DefeatedScene(GameMenu _gameMenu)
        {
            InitializeComponent();
            gameMenu = _gameMenu;
            LoadImage();
        }

        private void LoadImage()
        {
            int index = rnd.Next(1, CountImage + 1);
            string absolutePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                               $"Asset/Defeated/pic{index}.png");
            DefeatedImage.Source = new BitmapImage(new Uri(absolutePath));
           
        }
        private void ReturnMenu_Click(object sender, RoutedEventArgs e)
        {
            gameMenu.Show();
            this.Close();
        }
    }
}

