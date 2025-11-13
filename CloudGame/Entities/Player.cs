using CloudGame.Asset;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CloudGame.Entities
{
    internal class Player
    {
        public ImageSource image { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Speed { get; set; } = 200; // pixel / second
        public double Width = 200;
        public double Height = 130;
        //  private Canvas canvas;

        public Player(double hostWidth)
        {
            image = AssetService.GetImage("Asset/Cloud.png");

            // Đặt giữa màn hình
            X = (hostWidth - Width) / 2;
            Y = 10 ;
        }

        public void Update(double deltaSeconds, double maxWidth, double maxHeight)
        {
            double dx = 0, dy = 0;

            if (Keyboard.IsKeyDown(Key.Left)) dx = -1;
            if (Keyboard.IsKeyDown(Key.Right)) dx = 1;

            // chuẩn hóa vector
            double len = Math.Sqrt(dx * dx + dy * dy);
            if (len != 0) { dx /= len; dy /= len; }

            X += dx * Speed * deltaSeconds;
            Y += dy * Speed * deltaSeconds;

            // giới hạn
            if (X < 0) X = 0;
            if (Y < 0) Y = 0;
            if (X + Width > maxWidth) X = maxWidth - Width;
            if (Y + Height > maxHeight) Y = maxHeight - Height;
        }

        public Rect GetBounds()
        {
            return new Rect(X, Y, Width, Height);
        }
    }
}
