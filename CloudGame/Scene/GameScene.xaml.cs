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
            this.Loaded += (s, e) =>
            {
                try
                {
                    DownLoadAssets();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi load ảnh: " + ex.Message);
                }
            };
            //DownLoadAssets();
        }

        void DownLoadAssets() {
            var image = Asset.AssetService.GetImage("Asset/Scene.png");
            Image imgControl = new Image();
            imgControl.Source = image;
            imgControl.Stretch = Stretch.Fill; // hoặc Uniform
            imgControl.Width = this.Width;
            imgControl.Height = this.Height;
            this.Content = imgControl;
        } 
    }
}
