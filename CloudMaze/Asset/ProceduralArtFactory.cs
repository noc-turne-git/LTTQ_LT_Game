using CloudMaze.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace CloudMaze.Asset
{
    internal class ProceduralArtFactory
    {
        private static double cell = 512 / Maze.Rows;
        private static ImageSource Render(int width, int height, Action<DrawingContext> renderer)
        {
            var visual = new DrawingVisual();
            //RenderOptions.SetEdgeMode(visual, EdgeMode.Aliased); 
            using (var dc = visual.RenderOpen())
            {
                renderer(dc);
            }

            var bitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            bitmap.Freeze();
            return bitmap;
        }

        public static ImageSource CreateVirus() => Render((int)cell, (int)cell, dc =>
        {
            double center = cell / 2.0 * 0.5;
            double radius = cell / 2.0 * 0.5;

            // Thân virus (vàng)
            dc.DrawEllipse(Brushes.Yellow, new Pen(Brushes.Orange, 2), new Point(center, center), radius, radius);

            // Sừng trái
            PointCollection leftHorn = new PointCollection
            {
                new Point(center - 5, center - radius / 2),
                new Point(center - 5, center - radius - 1),
                new Point(center, center - radius / 2)
            };
            //dc.DrawGeometry(Brushes.Red, null, new PolygonGeometry(leftHorn));
            // Tạo một StreamGeometry mới
            StreamGeometry horn = new StreamGeometry();

            // Mở GeometryContext để vẽ hình
            using (StreamGeometryContext ctx = horn.Open())
            {
                ctx.BeginFigure(new Point(center - radius * 0.6, center - radius * 0.6), true, true);
                ctx.LineTo(new Point(center - radius * 0.3, center - radius * 1.2), true, true);
                ctx.LineTo(new Point(center - radius * 0.0, center - radius * 0.6), true, true);
            }
            horn.Freeze();
            dc.DrawGeometry(Brushes.Red, null, horn);

            // Sừng phải
            PointCollection rightHorn = new PointCollection
            {
                new Point(center + radius / 2, center - radius / 2),
                new Point(center + radius / 2 + 10, center - radius - 10),
                new Point(center + radius, center - radius / 2)
            };
            //dc.DrawGeometry(Brushes.Red, null, new PolygonGeometry(rightHorn));
            // Tạo một StreamGeometry mới
            horn = new StreamGeometry();

            // Mở GeometryContext để vẽ hình
            using (StreamGeometryContext ctx = horn.Open())
            {
                ctx.BeginFigure(new Point(center + radius * 0.6, center - radius * 0.6), true, true);
                ctx.LineTo(new Point(center + radius * 0.3, center - radius * 1.2), true, true);
                ctx.LineTo(new Point(center + radius * 0.0, center - radius * 0.6), true, true);
            }

            // Freeze để tối ưu hiệu năng
            horn.Freeze();

            // Vẽ geometry lên DrawingContext
            dc.DrawGeometry(Brushes.Red, null, horn);

            // === Mắt trái ===
            dc.DrawEllipse(Brushes.White, null, new Point(center - radius * 0.3, center - radius * 0.1), radius * 0.2, radius * 0.2);
            dc.DrawEllipse(Brushes.Black, null, new Point(center - radius * 0.3, center - radius * 0.1), radius * 0.1, radius * 0.1);

            // === Mắt phải ===
            dc.DrawEllipse(Brushes.White, null, new Point(center + radius * 0.3, center - radius * 0.1), radius * 0.2, radius * 0.2);
            dc.DrawEllipse(Brushes.Black, null, new Point(center + radius * 0.3, center - radius * 0.1), radius * 0.1, radius * 0.1);
        });

        public static ImageSource CreateMaze() => Render(512, 512, dc =>
        {
            //Maze.GenerateMaze();
            int rows = Maze.Rows;
            int cols = Maze.Cols;
            

            SolidColorBrush pathBrush = new SolidColorBrush(Colors.LightSkyBlue);
            Pen wallPen = new Pen(Brushes.DarkBlue, 10);

            Rect rect = new Rect(0, 0, 512, 512);
            dc.DrawRectangle(pathBrush, null, rect);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (Maze.Grid[r, c] == 1)
                    {
                        double x = c * cell;
                        double y = r * cell;

                        // vẽ các cạnh tường
                        if (r == 0 || Maze.Grid[r - 1, c] == 0) // trên
                            dc.DrawLine(wallPen, new Point(x, y), new Point(x + cell, y));
                        if (r == rows - 1 || Maze.Grid[r + 1, c] == 0) // dưới
                            dc.DrawLine(wallPen, new Point(x, y + cell), new Point(x + cell, y + cell));
                        if (c == 0 || Maze.Grid[r, c - 1] == 0) // trái
                            dc.DrawLine(wallPen, new Point(x, y), new Point(x, y + cell));
                        if (c == cols - 1 || Maze.Grid[r, c + 1] == 0) // phải
                            dc.DrawLine(wallPen, new Point(x + cell, y), new Point(x + cell, y + cell));
                    }
                }
            }
        });
    };
}
