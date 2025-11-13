using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using CloudGame.Entities;

namespace CloudGame.Core
{
    public class GameHost : FrameworkElement
    {
        private readonly Stopwatch _stopwatch = new();
        private GameTime gameTime = new GameTime();
        
        private List<Enemy> enemies = new List<Enemy>(); // danh sách quái vật trong game
        private ImageSource backgroundImage; // ảnh nền

        public GameHost()
        {
            Focusable = true;
            SnapsToDevicePixels = true;
            _stopwatch.Start();

            backgroundImage = Asset.AssetService.GetImage("Asset/Scene.png");
        }

        // đây là lớp kế thừa FrameworkElement (--> UIElement)
        // 1/ nên dc này là vẽ trực tiếp lên UI
        // 2/ khi UI lần đầu đc vẽ / InvalidateVisual() thì OnRender sẽ đc gọi
        protected override void OnRender(DrawingContext dc) 
        {
            base.OnRender(dc);

            var now = _stopwatch.Elapsed;                          //Lấy thời điểm hiện tại kể từ khi game bắt đầu chạy.
            gameTime.DeltaTime = now - gameTime.LastFrame;         //Tính thời gian trôi qua giữa hai khung hình (frame time).
            gameTime.TotalTime = now;                               

            dc.DrawImage(backgroundImage, new Rect(0, 0, this.ActualWidth, this.ActualHeight));
            //Console.WriteLine("Create background");
            // TODO: Update game logic here

            //Sau 1 thoi gian --> xuất hiện 1 quái vật
            Enemy newEnemy = Enemy.CreateEnemy(gameTime, (int)this.ActualWidth, (int) this.ActualHeight);
            //Console.WriteLine($"New Enemy Created");
            if (newEnemy != null) enemies.Add(newEnemy);

            foreach (var en in enemies)
            {
                dc.DrawImage(en.Image, new Rect(en.X, en.Y, en.Width, en.Height));
                
                //Console.WriteLine($"Enemy Pos: {en.X}, {en.Y}");
                //Console.WriteLine($"DeltaTime: {gameTime.DeltaTime.TotalSeconds}s, TotalTime: {gameTime.TotalSeconds.TotalSeconds}s");

                double deltaSeconds = gameTime.DeltaTime.TotalSeconds;
                en.UpdatePos(deltaSeconds, (int)this.ActualWidth, (int)this.ActualHeight);
            }
            gameTime.LastFrame = now;                              //Ghi lại thời điểm hiện tại để frame sau tính tiếp. 

            // TODO: Draw game objects here

            InvalidateVisual(); // gọi lại render, ko cần Timer_Tick
        }
    }
}
