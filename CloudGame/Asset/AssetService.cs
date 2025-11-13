using System;
using System.Collections.Concurrent;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CloudGame.Asset
{
    public class AssetService
    {
        // Cache ảnh đã load
        private static readonly ConcurrentDictionary<string, ImageSource> ImageCache = new(StringComparer.OrdinalIgnoreCase);

        public static ImageSource GetImage(string relativePath)
        {
            // Nếu đã có trong cache thì trả về
            if (ImageCache.TryGetValue(relativePath, out var cached))
                return cached;

            // Nếu chưa có thì load từ file
            var image = LoadFromFile(relativePath);
            if (image != null)
            {
                image.Freeze();
                ImageCache[relativePath] = image;
            }

            return image;
        }

        private static ImageSource LoadFromFile(string relativePath)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);

            if (!File.Exists(path))
            {
                MessageBox.Show($"File không tồn tại: {path}");
                return null;
            }

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.Absolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.EndInit();
            bitmap.Freeze();

            return bitmap;
        }
    }
}
