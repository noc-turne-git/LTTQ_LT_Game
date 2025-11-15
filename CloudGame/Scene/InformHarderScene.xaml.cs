using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
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
            WindowState = WindowState.Maximized;
            gameScene = _gameScene; // truyền đối tượng gameScene để tiếp tục ván đấu

            // Tạo UI
            Grid grid = new Grid();

            int index = rnd.Next(1, CountImage + 1);

            Image img = new Image();
            img.Stretch = Stretch.Uniform;                      // giữ tỉ lệ ảnh
            img.VerticalAlignment = VerticalAlignment.Center;   // ảnh nằm ở trên
            img.Margin = new Thickness(0, 0, 0, 150);           // chừaở dưới
            BitmapImage bmp;
            switch (index)
            {
                case 1:
                    bmp = new BitmapImage(new Uri("Asset/StartHarder/pic1.png", UriKind.Relative));
                    break;
                case 2:
                    bmp = new BitmapImage(new Uri("Asset/StartHarder/pic2.png", UriKind.Relative));
                    break;
                case 3:
                    bmp = new BitmapImage(new Uri("Asset/StartHarder/pic3.png", UriKind.Relative));
                    break;
                case 4:
                default:
                    bmp = new BitmapImage(new Uri("Asset/StartHarder/pic4.png", UriKind.Relative));
                    break;
            }

            if (bmp == null)
            {
                Console.WriteLine("Lỗi load ảnh InformHarderScene");
                return;
            }

            img.Source = bmp;
            grid.Children.Add(img);
            TextBox textBox = new TextBox();
            textBox.Text = "Game sẽ tăng độ khó";

            Button BtnContinue = new Button();
            BtnContinue.Content = "Game sẽ tăng độ khó";
            BtnContinue.Padding = new Thickness(20, 10, 20, 10);
            BtnContinue.FontSize = 50;
            BtnContinue.FontWeight = FontWeights.Bold;
            BtnContinue.Foreground = Brushes.White;
            BtnContinue.Background = (Brush)new BrushConverter().ConvertFromString("#5C3DFF");
            BtnContinue.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#927AFF");
            BtnContinue.BorderThickness = new Thickness(2);
            BtnContinue.Cursor = Cursors.Hand;
            BtnContinue.HorizontalAlignment = HorizontalAlignment.Center;
            BtnContinue.VerticalAlignment = VerticalAlignment.Bottom;
            BtnContinue.Margin = new Thickness(0, 0, 0, 50);
            BtnContinue.Click += BtnContinue_Click;


            // DropShadowEffect
            BtnContinue.Effect = new DropShadowEffect()
            {
                Color = (Color)ColorConverter.ConvertFromString("#7C5CFF"),
                BlurRadius = 25,
                ShadowDepth = 0
            };

            // Thêm vào grid
            grid.Children.Add(BtnContinue);
            this.Content = grid;
        }

        private void BtnContinue_Click(object sender, RoutedEventArgs e)
        {
            gameScene.Show();
            this.Close();
            gameScene.gameHost.SetHarder();
        }
    }
}


