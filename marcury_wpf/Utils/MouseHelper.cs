using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace marcury_wpf.Utils
{
    internal class MouseHelper
    {

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        public static Point GetMousePosition()
        {
            GetCursorPos(out POINT point);
            return new Point(point.X, point.Y);
        }
    }
}
