using CloudGame.Entities;
using CloudGame.Scene;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;


namespace CloudGame.Core
{
    public class GameHost : FrameworkElement
    {
        private readonly Stopwatch stopwatch = new();
        private GameTime gameTime = new GameTime();
        private DispatcherTimer timer;
        private ImageSource backgroundImage;
        private TimeSpan lastEnemySpawn = TimeSpan.Zero;
        private TimeSpan enemySpawnInterval = TimeSpan.FromSeconds(1.3);

        private List<Enemy> enemies = new List<Enemy>(); // danh sách quái vật trong game
        private Player player;
         private InputService inputService;
        private GameScene gameScene;

        private bool harderShown = false;
        private bool defeatedShown = false;// đã hiện Window InformHaderScene chưa
        private bool paused = false;                     // lưu trạng thái tạm dừng game
                           // cần mở Window InformHaderScene khi đạt thời gian nhưng Window cần tham chiếu tới GameScene
        public GameHost(GameScene gameScene)
        {
            Focusable = true;
            SnapsToDevicePixels = true;
            stopwatch.Start();
            inputService = new InputService();           

            KeyDown += (s, e) => inputService.KeyDown(e.Key);
            KeyUp += (s, e) => inputService.KeyUp(e.Key);
            backgroundImage = Asset.AssetService.GetImage("Asset/Scene.png");
            this.gameScene = gameScene;
            Loaded += (s, e) =>
            {
                Focus();
                player = new Player(this.ActualWidth);
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(16); // ~60 FPS
                timer.Tick += Timer_Tick;
                timer.Start();       
            };
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!paused)
                InvalidateVisual();  // gọi OnRender
        }
        private void TrySpawnEnemy()
        {
            if (gameTime.TotalTime == TimeSpan.Zero || gameTime.TotalTime - lastEnemySpawn >= enemySpawnInterval)
            {
                lastEnemySpawn = gameTime.TotalTime;

                Enemy e = Enemy.CreateEnemy((int)ActualWidth, (int)ActualHeight);
                enemies.Add(e);

                Console.WriteLine("Spawn Enemy at " + gameTime.TotalTime.TotalSeconds + " sec");
            }
        }
        protected override void OnRender(DrawingContext dc) 
        {
            base.OnRender(dc);
            if (paused) return;

            var now = stopwatch.Elapsed;                          //Lấy thời điểm hiện tại kể từ khi game bắt đầu chạy.
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
            TrySpawnEnemy();

            List<Enemy> removeEnemies = new List<Enemy>();
            List<Bullet> removeBullets = new List<Bullet>();
            foreach (var en in enemies)
            {
                dc.DrawImage(en.Image, new Rect(en.X, en.Y, en.Width, en.Height));

                //thanh mau HP
                if (en.CurrentHP == en.MaxHP)
                    dc.DrawRectangle(Brushes.Red, null, new Rect(en.HPX, en.HPY, en.HPWidth, en.HPHeight));
                else
                {
                    double hpPercent = (double)en.CurrentHP / en.MaxHP;
                    dc.DrawRectangle(Brushes.LimeGreen, null, new Rect(en.HPX,en.HPY, en.HPWidth * hpPercent, en.HPHeight));
                }    

                en.UpdatePos(deltaSeconds, (int)this.ActualWidth, (int)this.ActualHeight);    
                
                //BULLETS
                foreach (var b in player.Bullets)
                {
                    if (b.GetBounds().IntersectsWith(en.GetBounds()))
                    {
                        en.CurrentHP -= 1;
                        removeBullets.Add(b);
                        if (en.CurrentHP <=0)
                            removeEnemies.Add(en);
                        
                    }
                }    
            }

            foreach (var en in removeEnemies)
                enemies.Remove(en);
            foreach (var b in removeBullets)
                player.Bullets.Remove(b);          

            if (!harderShown && gameTime.TotalTime.TotalMinutes >= 0.1)
            {
                OpenInformHaderScene();
            }

            if (!defeatedShown && IsDie())
            {
                OpenDefeatedScene();
                
            }
                gameTime.LastFrame = now;
        }

        public bool IsDie ()
        {
            foreach(var en in  enemies)
            {
                if (en.GetBounds().IntersectsWith(player.GetBounds()) || en.Y == 0)
                {
                    return true;
                }
                
            }
            return false;
        }
        public void PauseGame()
        {
            paused = true;
        }
        public void ResumeGame()
        {
            paused = false;
            gameTime.LastFrame = stopwatch.Elapsed;  // tránh deltaTime (= now - Last) lớn khi resume (vị trí quái dùng deltaTime để di chuyển)
        }
        public void SetHarder()
        {        
            Enemy.Speed = Enemy.Speed + 50;                            // tăng tốc độ di chuyển của quái vật
            enemySpawnInterval = TimeSpan.FromSeconds(0.2);       // tăng tốc độ xuất hiện của quái vật
            ResumeGame();
        }

        private void OpenDefeatedScene()
        {
            defeatedShown = true;  // đánh dấu đã mở
            PauseGame();           // tạm dừng game trước khi mở window

            // Mở window trên UI thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                DefeatedScene defeatedScene = new DefeatedScene();
                gameScene.Hide();
                defeatedScene.Show();
            });
        }
        private void OpenInformHaderScene()
        {
            harderShown = true; // đảm bảo chỉ mở 1 lần
            PauseGame();
            // Mở window mới trên UI thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                InformHarderScene harderWindow = new InformHarderScene(gameScene);
                if (harderWindow != null)
                {
                    Console.WriteLine("Inform Harder window is opened.");
                }
                harderWindow.Show();
                gameScene.Hide();
            });
        }
    }
}
