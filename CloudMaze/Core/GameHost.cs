using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace CloudMaze.Core
{
    public class GameHost : FrameworkElement
    {
        private readonly Stopwatch _stopwatch = new();
        private TimeSpan _lastFrame;

        public GameHost()
        {
            Focusable = true;
            SnapsToDevicePixels = true;
            _stopwatch.Start();
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var now = _stopwatch.Elapsed; //Lấy thời điểm hiện tại kể từ khi game bắt đầu chạy.
            var delta = now - _lastFrame; //Tính thời gian trôi qua giữa hai khung hình (frame time).
            _lastFrame = now; //Ghi lại thời điểm hiện tại để frame sau tính tiếp.

            // TODO: Update game logic here

            // TODO: Draw game objects here
            InvalidateVisual(); // gọi lại render
        }
    }
}
