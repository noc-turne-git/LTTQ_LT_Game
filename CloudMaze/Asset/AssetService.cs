using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CloudMaze.Asset
{
    public class AssetService
    {
        //Cache ảnh
        private static readonly ConcurrentDictionary<string, ImageSource> ImageCache = new(StringComparer.OrdinalIgnoreCase);
        //Hàm tạo ảnh;
        private static readonly IReadOnlyDictionary<string, Func<ImageSource>> ProceduralGenerators = new Dictionary<string, Func<ImageSource>>(StringComparer.OrdinalIgnoreCase)
        {
            ["Scenes/Maze"] = ProceduralArtFactory.CreateMaze,
            //["Enemy/VirusOrange"]
            ["Enemy/VirusYellow"] = ProceduralArtFactory.CreateVirus,
            //["Enemy/Virus1Green"]*/
        };

        private static readonly string AssetRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");

        public static ImageSource GetImage(string relativePath)
        {
            if (ImageCache.TryGetValue(relativePath, out var cached))
            {
                return cached;
            }

            ImageSource image;
            if (ProceduralGenerators.TryGetValue(relativePath, out var generator))
            {
                image = generator();
            }
            else
            {
                image = LoadFromFile(relativePath);
            }

            image.Freeze();
            ImageCache[relativePath] = image;
            return image;
        }

        private static ImageSource LoadFromFile(string relativePath)
        {
            var path = Path.Combine(AssetRoot, relativePath.Replace('/', Path.DirectorySeparatorChar));
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Asset not found: {relativePath}", path);
            }

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.Absolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap; // ImageSource mà trả về là bitmap??
        }
    }
}
