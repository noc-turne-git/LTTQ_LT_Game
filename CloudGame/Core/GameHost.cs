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
        private readonly Stopwatch _stopwatch = new();
        private GameTime gameTime = new GameTime();
        private DispatcherTimer timer;

        private List<Enemy> enemies = new List<Enemy>(); // danh sách quái vật trong game
        private Player player;
        private ImageSource backgroundImage;             // ảnh nền
        private bool harderShown = false;                // đã hiện Window InformHaderScene chưa
        private bool paused = false;                     // lưu trạng thái tạm dừng game
        private GameScene gameScene;                     // cần mở Window InformHaderScene khi đạt thời gian nhưng Window cần tham chiếu tới GameScene
        public GameHost(GameScene _gameScene)
        {
            Focusable = true;
            SnapsToDevicePixels = true;
            _stopwatch.Start();
            gameScene = _gameScene;

            backgroundImage = Asset.AssetService.GetImage("Asset/Scene.png");
            Loaded += (s, e) =>
            {
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
                InvalidateVisual(); // gọi OnRender
        }

        // đây là lớp kế thừa FrameworkElement (--> UIElement)
        // 1/ nên dc này là vẽ trực tiếp lên UI
        // 2/ khi UI lần đầu đc vẽ / InvalidateVisual() thì OnRender sẽ đc gọi
        protected override void OnRender(DrawingContext dc) 
        {
            base.OnRender(dc);
            if (paused)
                return;
            Console.WriteLine("OnRender called");
            var now = _stopwatch.Elapsed;                          //Lấy thời điểm hiện tại kể từ khi game bắt đầu chạy.
            gameTime.DeltaTime = now - gameTime.LastFrame;         //Tính thời gian trôi qua giữa hai khung hình (frame time).
            gameTime.TotalTime = now;
            double deltaSeconds = gameTime.DeltaTime.TotalSeconds;
            dc.DrawImage(backgroundImage, new Rect(0, 0, this.ActualWidth, this.ActualHeight));
            //Console.WriteLine("Create background");
            if (player != null)
            {
                player.Update(deltaSeconds, ActualWidth, ActualHeight);
                dc.DrawImage(player.image, new Rect(player.X, player.Y, player.Width, player.Height));
            } 
                

            //Sau 1 thoi gian --> xuất hiện 1 quái vật
            Enemy newEnemy = Enemy.CreateEnemy(gameTime, (int)this.ActualWidth, (int) this.ActualHeight);
            if (newEnemy != null) enemies.Add(newEnemy);

            foreach (var en in enemies)
            {
                dc.DrawImage(en.Image, new Rect(en.X, en.Y, Enemy.Width, Enemy.Height));
                en.UpdatePos(deltaSeconds, (int)this.ActualWidth, (int)this.ActualHeight);
            }
            gameTime.LastFrame = now;                              //Ghi lại thời điểm hiện tại để frame sau tính tiếp. 

            // Kiểm tra nếu vượt qua 1 thời gian thì tăng độ khó
            if (!harderShown && gameTime.TotalTime.TotalMinutes >= 0.5)
            {
                OpenInformHaderScene();
            }
            // InvalidateVisual(); // gọi lại render, ko cần vi dung Timer_Tick
        }
        public void PauseGame()
        {
            paused = true;
        }
        public void ResumeGame()
        {
            paused = false;
            gameTime.LastFrame = _stopwatch.Elapsed;  // tránh deltaTime (= now - Last) lớn khi resume (vị trí quái dùng deltaTime để di chuyển)
        }
        public void SetHarder()
        {
            Enemy.speed = Enemy.speed + 100;                            // tăng tốc độ di chuyển của quái vật
            Enemy.enemySpawnInterval = TimeSpan.FromSeconds(0.5);       // tăng tốc độ xuất hiện của quái vật
            ResumeGame();
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
