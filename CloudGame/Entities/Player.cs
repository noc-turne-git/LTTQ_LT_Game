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
        public double Speed { get; set; } = 250; // pixel / second
        public double Width = 220;
        public double Height = 110;

        public List<Bullet> Bullets = new(); 
        private bool canShoot = true; 


        public Player(double hostWidth)
        {
            image = AssetService.GetImage("Asset/Cloud.png");
            X = (hostWidth - Width) / 2;
            Y = 10 ;
        }

        public void Update(double deltaSeconds, double maxWidth, double maxHeight, HashSet<Key> keys)
        {
            double dx = 0, dy = 0;

            if (keys.Contains(Key.Left)) dx -= 1;
            if (keys.Contains(Key.Right)) dx += 1;

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


            if (keys.Contains(Key.Space))
            {
                if (canShoot)
                {
                    Shoot();
                    canShoot = false; // khóa bắn, chờ nhả Space
                }
            }
            else
            {
                canShoot = true; // nhả Space -> có thể bắn tiếp
            }

            for (int i = Bullets.Count - 1; i >= 0; i--)
            {
                Bullets[i].Update(deltaSeconds);
                if (Bullets[i].Y + Bullets[i].Height < 0)
                {
                    Bullets.RemoveAt(i); // loại đạn ra ngoài màn hình
                }
            }
        }
        private void Shoot()
        {
            // tạo đạn ở chính giữa player
            double bulletX = X + Width / 2 - 10; // bullet width 20
            double bulletY = Y + Height / 2 + 10;
            Bullets.Add(new Bullet(bulletX, bulletY));

        }
        public Rect GetBounds()
        {
            return new Rect(X, Y, Width, Height);
        }
    }
}
