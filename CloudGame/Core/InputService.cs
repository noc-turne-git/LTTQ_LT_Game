
using System.Windows.Input;

namespace CloudGame.Core
{
    internal class InputService
    {
        private readonly HashSet<Key> pressedKeys = new();

        // Gọi khi KeyDown
        public void KeyDown(Key key)
        {
            pressedKeys.Add(key);
        }

        // Gọi khi KeyUp
        public void KeyUp(Key key)
        {
            pressedKeys.Remove(key);
        }

        // Kiểm tra một phím có đang nhấn không
        public bool IsKeyPressed(Key key)
        {
            return pressedKeys.Contains(key);
        }

        // Trả về tất cả phím đang nhấn (nếu muốn)
        public HashSet<Key> GetPressedKeys()
        {
            return new HashSet<Key>(pressedKeys);
        }
    }
}
