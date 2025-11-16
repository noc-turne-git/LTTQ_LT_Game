using System.Windows;
using System.Windows.Media.Imaging;

namespace CloudGame.Scene
{
    /// <summary>
    /// Interaction logic for InformHarderScene.xaml
    /// </summary>
    public partial class InformHarderScene : Window
    {
        private Random rnd = new Random();
        private static int CountImage = 4;
        private GameScene gameScene;
        public InformHarderScene(GameScene _gameScene)
        {
            InitializeComponent();  // QUAN TRỌNG
            gameScene = _gameScene;            
            LoadRandomImage();
        }

        private void LoadRandomImage()
        {
            //int index = rnd.Next(1, CountImage + 1);
            int index = 4;
            string absolutePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                $"Asset/StartHarder/pic{index}.png");
            RandomImage.Source = new BitmapImage(new Uri(absolutePath));
        }

        private void BtnContinue_Click(object sender, RoutedEventArgs e)
        {
            gameScene.Show();
            this.Close();
            gameScene.gameHost.SetHarder();
        }
    }

       
    
}


