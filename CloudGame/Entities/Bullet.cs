using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using CloudGame.Asset;

namespace CloudGame.Entities
{
    internal class Bullet
    {

        public double X;
        public double Y;
        public double Speed = 500; // pixel/second
        public double Width = 20;
        public double Height = 20;
        public ImageSource Image;

        public Bullet(double x, double y)
        {
            X = x;
            Y = y;
            Image = AssetService.GetImage("Asset/Bullet.png"); ;
        }

        public void Update(double deltaSeconds)
        {
            Y += Speed * deltaSeconds;
        }

        public Rect GetBounds()
        {
            return new Rect(X, Y, Width, Height);
        }
    }
}
