using CloudGame.Core;
using System.Windows;
using System.Windows.Media;

namespace CloudGame.Entities
{
    internal class Enemy
    {
        const int MOVE_UP = 1;
        const int MOVE_LEFT = 2;
        const int MOVE_RIGHT = 3;
        public ImageSource Image { get; set; }
        
        public double X { get; set; } // tọa độ trên màn hình
        public double Y { get; set; } // tọa độ trên màn hình
        public int Width { get; set; }
        public int Height { get; set; }
        public int MaxHP {  get; set; }
        public int CurrentHP {  get; set; }
        public static double Speed = 300;                                    

        // Vị trí thanh máu
        public int HPWidth { get; set; }
        public int HPHeight {  get; set; }
        public double HPX { get; set; }
        public double HPY { get; set; }

        private static Random rnd = new Random(); 
        private int dirCurrent = rnd.Next(1,4); // hướng di chuyển hiện tại

        public Enemy()
        {          
            Width = 70;
            Height = 70;
            HPWidth = Width;
            HPHeight = 5;
        }

        public static Enemy CreateEnemy(int UIwidth , int UIheight)
        {          
                // Tạo enemy mới ở vị trí ngẫu nhiên
            Enemy newEnemy = new Enemy();
            newEnemy.X = Enemy.rnd.Next(0, (int)(UIwidth - newEnemy.Width)); // chiều rộng enemy là 20
            newEnemy.Y = UIheight - newEnemy.Height; // chiều cao enemy là 20
            newEnemy.HPX = newEnemy.X;
            newEnemy.HPY = newEnemy.Y - 8;

            int type = Enemy.rnd.Next(1, 3);                
            switch (type)
            {
                case 1:
                    newEnemy.Image = Asset.AssetService.GetImage("Asset/GreenEnemy.png");
                    newEnemy.MaxHP = 2;
                    break;
                case 2:
                default:
                    newEnemy.Image = Asset.AssetService.GetImage("Asset/YellowEnemy.png");
                    newEnemy.MaxHP = 4;
                    break;
            }
            newEnemy.CurrentHP = newEnemy.MaxHP;
            return newEnemy;
        }

        public void UpdatePos(double deltaSeconds, double UIwidth, double UIheight)
        {
            int dir;
            double rnd_dir = rnd.NextDouble();
            if (rnd_dir < 0.7)
                dir = MOVE_UP;
            else
                dir = rnd.Next(1, 3);
            switch (dir)
            {
                case MOVE_UP: Y-= Speed * deltaSeconds; break;
                case MOVE_LEFT: X -= Speed * deltaSeconds; break; // trái
                case MOVE_RIGHT: X += Speed * deltaSeconds; break; // phải
            }

            if (rnd.NextDouble() < 0.8)
                dir = dirCurrent;
            else
                dir = rnd.Next(1, 4);       

            // Kiểm tra chạm biên
            bool hitBorder = false;
            if (X < 50) { X = 50; hitBorder = true; }
            if (X > UIwidth - Width - 50) { X = UIwidth - Width - 50; hitBorder = true; }
            if (Y < 0) { Y = 0; hitBorder = true; }
            if (Y > UIheight - Height) { Y = UIheight - Height; hitBorder = true; }

            // Nếu chạm biên, chọn hướng mới ngẫu nhiên
            if (hitBorder)
            {
                dirCurrent = rnd.Next(1, 4); // thay hướng mới
            }
            else
            {
                dirCurrent = dir; // nếu không chạm biên, giữ hướng hiện tại
            }
            HPX = X;
            HPY = Y - 8;
        }

        public Rect GetBounds()
        {
            return new Rect(X, Y, Width, Height);
        }
    }
}
