using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Cursor = System.Windows.Input.Cursor;
using Cursors = System.Windows.Input.Cursors;
using MessageBox = System.Windows.MessageBox;

namespace marcury_wpf.SubWindow
{
    /// <summary>
    /// Interaction logic for OverlayWindow.xaml
    /// </summary>
    public partial class OverlayWindow : Window
    {
        private Cursor _originalCursor;
        private const int WS_EX_TRANSPARENT = 0x00000020;
        private const int WS_EX_TOOLWINDOW = 0x00000080;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_EXSTYLE = -20;
        public OverlayWindow()
        {
            InitializeComponent();
            this.Width = SystemParameters.VirtualScreenWidth;
            this.Height = SystemParameters.VirtualScreenHeight;
            this.Left = SystemParameters.VirtualScreenLeft;
            this.Top = SystemParameters.VirtualScreenTop;
            this.Loaded += OverlayWindow_Loaded;
        }
        
        private void OverlayWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Lấy handle của OverlayWindow
            var hwnd = new WindowInteropHelper(this).Handle;

            // Đặt thuộc tính WS_EX_TRANSPARENT để click xuyên qua
            int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, exStyle | WS_EX_TRANSPARENT | WS_EX_TOOLWINDOW);
            // Save the original mouse pointer
            _originalCursor = Mouse.OverrideCursor;

            // Change the mouse cursor to a plus sign
            Mouse.OverrideCursor = Cursors.Cross;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Khôi phục con trỏ chuột về mặc định
            Mouse.OverrideCursor = null;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(System.Drawing.Point Point);

        private void OverlayWindow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Lấy vị trí chuột hiện tại trong màn hình
            var point = System.Windows.Forms.Control.MousePosition;

            // Lấy handle của cửa sổ hoặc control tại vị trí đó
            IntPtr hWnd = WindowFromPoint(new System.Drawing.Point(point.X, point.Y));

            if (hWnd != IntPtr.Zero) {
                MessageBox.Show($"Handle của control: {hWnd}");
            }
        }
    }
}