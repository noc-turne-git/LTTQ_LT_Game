using CloudMaze.Asset;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CloudMaze.Entities
{
    internal class Player
    {
        public Image Sprite { get; private set; }
        public double X {  get; private set; }
        public double Y { get; private set; } 
        public double Speed { get; set; } = 5;
        private Canvas canvas;

        public Player (Canvas in_canvas, string imagePath)
        {
            canvas = in_canvas;
            Source = AssetService.GetImage(imagePath)
            X = 0; Y = 0;
            Canvas.SetLeft(Sprite, X);
            Canvas.SetTop(Sprite, Y);
            canvas.Children.Add(Sprite);
        }

        public void Move (float x, float y)
        {
            X += x * Speed;
            Y += y * Speed;
            UpdatePosition();
        }

        public void UpdatePosition()
        {
            Canvas.SetLeft(Sprite, X);
            Canvas.SetTop(Sprite, Y);
        }
        public Rect GetBounds()
        {
            return new Rect(X, Y, Sprite.Width, Sprite.Height);
        }
    }
}
