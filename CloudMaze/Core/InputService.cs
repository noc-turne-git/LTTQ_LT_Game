using System.Windows.Input;
using System.Collections.Generic;

namespace MyGame.Core
{
    public static class InputService
    {
        private static readonly HashSet<Key> _pressedKeys = new();

        public static void KeyDown(Key key) => _pressedKeys.Add(key);
        public static void KeyUp(Key key) => _pressedKeys.Remove(key);
        public static bool IsKeyPressed(Key key) => _pressedKeys.Contains(key);
    }
}
