using System.Runtime.InteropServices;

namespace MovingCursor.Core
{
    internal class MonitorBounds
    {
        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);
        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;

        public static int Left = 0;
        public static int Top = 0;
        public static int Right = GetSystemMetrics(SM_CXSCREEN);
        public static int Bottom = GetSystemMetrics(SM_CYSCREEN);
    }
}
