using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Cursor = System.Windows.Input.Cursor;
using Cursors = System.Windows.Input.Cursors;

namespace marcury_wpf.SubWindow
{
    /// <summary>
    /// Interaction logic for OverlayWindow.xaml
    /// </summary>
    public partial class OverlayWindow : Window
    {
        private Cursor _originalCursor;
        public OverlayWindow()
        {
            InitializeComponent();
            this.Width = SystemParameters.VirtualScreenWidth;
            this.Height = SystemParameters.VirtualScreenHeight;
            this.Left = SystemParameters.VirtualScreenLeft;
            this.Top = SystemParameters.VirtualScreenTop;
            this.Loaded += OverlayWindow_Loaded;
            this.Loaded += OverlayWindow_Loaded;
            this.Closed += OverlayWindow_Closed;
        }
        
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private void OverlayWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Save the original mouse pointer
            _originalCursor = Mouse.OverrideCursor;

            // Change the mouse cursor to a plus sign
            Mouse.OverrideCursor = Cursors.Cross;
        }

        private void OverlayWindow_Closed(object sender, System.EventArgs e)
        {
            // Restore the original mouse pointer
            Mouse.OverrideCursor = _originalCursor;
        }
    }
}