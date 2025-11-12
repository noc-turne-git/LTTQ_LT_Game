using System;

namespace CloudMaze.Entities
{
    public static class Maze
    {
        public static int[,] Grid { get; private set; }
        public const int Rows = 17;
        public const int Cols = 17;
        /*public static int[,] Grid = new int[Rows, Cols];

        private static Random rnd = new Random();

        // Khởi tạo và sinh mê cung ngẫu nhiên

        static void PrintGrid(int[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                Console.Write("[");
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(grid[i, j]);
                    if (j < cols - 1)
                        Console.Write(","); // dấu phẩy giữa các phần tử
                }
                Console.WriteLine("]");
            }
        }
        public static void GenerateMaze()
        {
            // 1 = tường, 0 = đường
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Cols; c++)
                    Grid[r, c] = 1; // bắt đầu toàn tường

            CarvePassage(0, 1); // bắt đầu từ ô (1,1)
            PrintGrid(Grid);
        }
        private static void CarvePassage(int r, int c)
        {
            Grid[r, c] = 0; // đánh dấu là đường

            // 4 hướng: lên, xuống, trái, phải
            var directions = new (int dr, int dc)[] { (-2, 0), (2, 0), (0, -2), (0, 2) };
            directions = directions.OrderBy(_ => rnd.Next()).ToArray(); // trộn ngẫu nhiên

            foreach (var (dr, dc) in directions)
            {
                int nr = r + dr;
                int nc = c + dc;

                // kiểm tra ranh giới
                if (nr > 0 && nr < Rows - 1 && nc > 0 && nc < Cols - 1 && Grid[nr, nc] == 1)
                {
                    // bỏ tường giữa
                    Grid[r + dr / 2, c + dc / 2] = 0;
                    CarvePassage(nr, nc);
                }
            }
        }
        */


        static Maze()
        {
            Grid = new int[,]
            {
                {1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,1},
                {1,0,0,0,1,1,1,0,1,0,1,1,1,1,1,0,1},
                {1,0,1,0,0,0,1,0,1,0,1,0,0,0,0,0,1},
                {1,0,1,1,1,1,1,0,0,0,1,0,1,1,1,0,1},
                {1,0,1,0,0,0,0,0,1,0,0,0,1,0,1,0,1},
                {1,0,1,0,1,1,1,1,1,1,1,0,1,0,1,0,1},
                {1,0,0,0,1,0,0,0,0,0,0,0,0,0,1,0,1},
                {1,1,1,1,1,0,1,1,1,1,1,0,1,0,1,1,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1},
                {1,0,1,0,1,1,1,1,1,0,1,1,1,1,1,0,1},
                {1,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,1},
                {1,0,1,1,1,1,1,0,0,0,0,1,1,1,1,1,1},
                {1,0,0,0,0,0,1,0,1,0,0,0,0,0,0,0,1},
                {1,1,1,1,1,1,1,1,1,0,1,1,1,0,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1}
            };


        }
    }
}
