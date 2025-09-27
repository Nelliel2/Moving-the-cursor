using System;
using System.Runtime.InteropServices;

namespace MovingCursor.Core
{

    internal class NativeMessageBox
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(nint hWnd, string text, string caption, uint type);

        public static void ShowError(string message)
        {
            MessageBox(nint.Zero, message, "Ошибка", 0x00000010);
        }
    }
}
