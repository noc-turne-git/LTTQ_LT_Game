using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CloudGame.Scene
{
    /// <summary>
    /// Interaction logic for DefeatedScene.xaml
    /// </summary>
    public partial class DefeatedScene : Window
    {
        private Random rnd = new Random();
        private static int CountImage = 4;
        public DefeatedScene()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            int index = rnd.Next(1, CountImage + 1);
            ImageSource image;
            switch (index)
            {
                case 1:
                    image = new BitmapImage(new Uri("Asset/Defeated/pic1.png", UriKind.Relative));
                    break;
                case 2:
                    image = new BitmapImage(new Uri("Asset/Defeated/pic2.png", UriKind.Relative));
                    break;
                case 3:
                    image = new BitmapImage(new Uri("Asset/Defeated/pic3.png", UriKind.Relative));
                    break;
                case 4:
                default:
                    image = new BitmapImage(new Uri("Asset/Defeated/pic4.png", UriKind.Relative));
                    break;
            }
            this.Content = image;
        }
    }
}

