using CloudGame.Core;
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
        public static int Width { get; set; } = 50;
        public static int Height { get; set; } = 50;
        public static double speed = 50;                                        // Tốc độ di chuyển (pixel/giây)

        private static Random rnd = new Random();

        private static TimeSpan lastEnemySpawn = TimeSpan.Zero;                  // thời điểm spawn quái vật cuối cùng được tạo
        public static TimeSpan enemySpawnInterval = TimeSpan.FromSeconds(1);    // khoảng thời gian giữa các lần spawn quái vật

        private int dirCurrent = rnd.Next(1,5);                                  // hướng di chuyển hiện tại

        public Enemy(ImageSource image, double PosX, double PosY)
        {
            Image = image;
            X = PosX;
            Y = PosY;
        }

        public static Enemy CreateEnemy(GameTime gameTime, int UIwidth , int UIheight)
        {
            double totalSeconds = gameTime.TotalTime.TotalSeconds;
            if (gameTime.LastFrame == TimeSpan.Zero
                || gameTime.TotalTime - lastEnemySpawn >= enemySpawnInterval)
            {
                lastEnemySpawn = gameTime.TotalTime;
                Console.WriteLine("Spawn Enemy at " + gameTime.TotalTime.TotalSeconds + " seconds");
                // Tạo enemy mới ở vị trí dưới cùng
                int posX = Enemy.rnd.Next(0, (int)(UIwidth));
                int posY = UIheight; // 

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
            //Console.WriteLine("Enemy UpdatePos called");
            // Chọn hướng
            int dir;
            if (rnd.NextDouble() < 0.5)
                dir = MOVE_UP;
            else if (rnd.NextDouble() < 0.8)
                dir = dirCurrent; // giữ nguyên hướng
            else
                dir = rnd.Next(1, 4); // chọn hướng mới ngẫu nhiên

            // Cập nhật vị trí theo hướng
            switch (dir)
            {
                case MOVE_RIGHT: X += speed * deltaSeconds; break; // phải
                case MOVE_LEFT: X -= speed * deltaSeconds; break; // trái
                case MOVE_UP: Y -= speed * deltaSeconds; break; // lên
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
