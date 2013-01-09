using System;
using System.Runtime.InteropServices;
using System.Text;

namespace LastClickedControl.win32
{
    public class Control
    {
        #region win32
        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(int xPoint, int yPoint);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, StringBuilder lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        const int WM_GETTEXT = 0x000D;
        const int WM_GETTEXTLENGTH = 0x000E;

        #endregion
        private static string GetWindowText(IntPtr hWnd)
        {
            var titleLength = SendMessage(hWnd, WM_GETTEXTLENGTH, (IntPtr)0, (IntPtr)0).ToInt32() + 1;

            var stringBuilder = new StringBuilder(titleLength);
            SendMessage(hWnd, WM_GETTEXT, (IntPtr)titleLength, stringBuilder);
            return stringBuilder.ToString();
        }
        private Control(string title)
        {
            Title = title;
        }

        public static Control FindByLocation(Point location)
        {
            var window = WindowFromPoint(location.x, location.y);
            return new Control(GetWindowText(window));
        }

        public string Title { get; set; }
    }
}