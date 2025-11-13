using CloudGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CloudGame.Entities
{
    internal class Enemy
    {
        public ImageSource Image { get; set; }
        
        public double X { get; set; } // tọa độ trên màn hình
        public double Y { get; set; } // tọa độ trên màn hình
        public int Width { get; set; }
        public int Height { get; set; }
        private double speed = 100; // thay đổi 50 pixels per second
        private static Random rnd = new Random();

        private static TimeSpan lastEnemySpawn = TimeSpan.Zero; // thời điểm spawn quái vật cuối cùng được tạo

        private int dirCurrent = rnd.Next(1,5); // hướng di chuyển hiện tại

        public Enemy(ImageSource image, double PosX, double PosY)
        {
            Image = image;
            X = PosX;
            Y = PosY;
            Width = 50;
            Height = 50;
        }

        public static Enemy CreateEnemy(GameTime gameTime, int UIwidth , int UIheight)
        {
            double totalSeconds = gameTime.TotalTime.TotalSeconds;
            if (gameTime.TotalTime == TimeSpan.Zero
                || gameTime.TotalTime - lastEnemySpawn >= gameTime.IntervalEnemySpawn)
            {
                lastEnemySpawn = gameTime.TotalTime;
                Console.WriteLine("Spawn Enemy at " + gameTime.TotalTime.TotalSeconds + " seconds");
                // Tạo enemy mới ở vị trí ngẫu nhiên
                int posX = Enemy.rnd.Next(0, (int)(UIwidth - 20)); // chiều rộng enemy là 20
                int posY = Enemy.rnd.Next(0, (int)(UIheight - 20)); // chiều cao enemy là 20

                int type = Enemy.rnd.Next(1, 3); //tạo số >= min và < max.

                ImageSource enemyImage;
                switch (type)
                {
                    case 1:
                        enemyImage = Asset.AssetService.GetImage("Asset/GreenEnemy.png");
                        break;
                    case 2:
                    default:
                        enemyImage = Asset.AssetService.GetImage("Asset/YellowEnemy.png");
                        break;
                }
                Enemy newEnemy = new Enemy(enemyImage, posX, posY);
                return newEnemy;
            }
            return null;
        }

        public void UpdatePos(double deltaSeconds, double UIwidth, double UIheight)
        {
            // Chọn hướng mới: 60% giữ hướng cũ, 40% random
            int dir;
            if (rnd.NextDouble() < 0.8)
                dir = dirCurrent;
            else
                dir = rnd.Next(1, 5);

            // Cập nhật vị trí theo hướng
            switch (dir)
            {
                case 1: X += speed * deltaSeconds; break; // phải
                case 2: X -= speed * deltaSeconds; break; // trái
                case 3: Y += speed * deltaSeconds; break; // xuống
                case 4: Y -= speed * deltaSeconds; break; // lên
            }

            // Kiểm tra chạm biên
            bool hitBorder = false;
            if (X < 0) { X = 0; hitBorder = true; }
            if (X > UIwidth - Width) { X = UIwidth - Width; hitBorder = true; }
            if (Y < 0) { Y = 0; hitBorder = true; }
            if (Y > UIheight - Height) { Y = UIheight - Height; hitBorder = true; }

            // Nếu chạm biên, chọn hướng mới ngẫu nhiên
            if (hitBorder)
            {
                dirCurrent = rnd.Next(1, 5); // thay hướng mới
            }
            else
            {
                dirCurrent = dir; // nếu không chạm biên, giữ hướng hiện tại
            }
        }

    }
}
