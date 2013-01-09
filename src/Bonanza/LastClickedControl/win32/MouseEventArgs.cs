using System;

namespace LastClickedControl.win32
{
    public class MouseEventArgs:EventArgs
    {
        private readonly Point _point;
        public MouseEventArgs(Point point)
        {
            _point = point;
        }

        public Point Point
        {
            get { return _point; }
        }
    }
}