using System.Runtime.InteropServices;

namespace LastClickedControl.win32
{
    [StructLayout(LayoutKind.Sequential)]
    internal class MouseHookStruct
    {
        public Point pt;
        public int hwnd;
        public int wHitTestCode;
        public int dwExtraInfo;
    }
}