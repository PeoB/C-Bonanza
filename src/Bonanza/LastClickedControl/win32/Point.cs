using System.Runtime.InteropServices;

namespace LastClickedControl.win32
{
    [StructLayout(LayoutKind.Sequential)]
    public class Point
    {
        public int x;
        public int y;
    }
}