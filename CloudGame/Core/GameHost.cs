using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using CloudGame.Entities;

namespace CloudGame.Core
{
    public class GameHost : FrameworkElement
    {
        private readonly Stopwatch _stopwatch = new();
        private GameTime gameTime = new GameTime();
        private DispatcherTimer timer;     
        private ImageSource backgroundImage; // ảnh nền
        private InputService inputService;

        private List<Enemy> enemies = new List<Enemy>(); // danh sách quái vật trong game
        private Player player;

        public GameHost()
        {
            Focusable = true;
            SnapsToDevicePixels = true;
            _stopwatch.Start();

            inputService = new InputService();
            KeyDown += (s, e) => inputService.KeyDown(e.Key);
            KeyUp += (s, e) => inputService.KeyUp(e.Key);
            backgroundImage = Asset.AssetService.GetImage("Asset/Scene.png");

            Loaded += (s, e) =>
            {
                player = new Player(this.ActualWidth);
                Focus();
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(16); // ~60 FPS
                timer.Tick += Timer_Tick;
                timer.Start();
            };
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            InvalidateVisual(); // gọi OnRender
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
            double deltaSeconds = gameTime.DeltaTime.TotalSeconds;
            dc.DrawImage(backgroundImage, new Rect(0, 0, this.ActualWidth, this.ActualHeight));

            //PLAYER
            if (player != null)
            {
                player.Update(deltaSeconds, ActualWidth, ActualHeight, inputService.GetPressedKeys());
                dc.DrawImage(player.image, new Rect(player.X, player.Y, player.Width, player.Height));

                foreach (var b in player.Bullets)
                {
                    dc.DrawImage(b.Image, new Rect(b.X, b.Y, b.Width, b.Height));
                }
            } 
                
            //ENEMY
            Enemy newEnemy = Enemy.CreateEnemy(gameTime, (int)this.ActualWidth, (int) this.ActualHeight);
            if (newEnemy != null) enemies.Add(newEnemy);

            List<Enemy> removeEnemies = new List<Enemy>();
            List<Bullet> removeBullets = new List<Bullet>();
            foreach (var en in enemies)
            {
                dc.DrawImage(en.Image, new Rect(en.X, en.Y, en.Width, en.Height));
                if (en.CurrentHP == en.MaxHP)
                    dc.DrawRectangle(Brushes.Red, null, new Rect(en.HPX, en.HPY, en.HPWidth, en.HPHeight));//thanh mau HP
                else
                {
                    double hpPercent = (double)en.CurrentHP / en.MaxHP;
                    dc.DrawRectangle(Brushes.LimeGreen, null, new Rect(en.HPX,en.HPY, en.HPWidth * hpPercent, en.HPHeight));
                }    

                en.UpdatePos(deltaSeconds, (int)this.ActualWidth, (int)this.ActualHeight);               
                foreach (var b in player.Bullets)
                {
                    if (b.GetBounds().IntersectsWith(en.GetBounds()))
                    {
                        en.CurrentHP -= 1;
                        removeBullets.Add(b);
                        if (en.CurrentHP <=0)
                        //MessageBox.Show("defeat");
                            removeEnemies.Add(en);
                        
                    }
                }    
            }
            foreach (var en in removeEnemies)
                enemies.Remove(en);
            foreach (var b in removeBullets)
                player.Bullets.Remove(b);

            gameTime.LastFrame = now;                              //Ghi lại thời điểm hiện tại để frame sau tính tiếp. 

        }
    }
}
