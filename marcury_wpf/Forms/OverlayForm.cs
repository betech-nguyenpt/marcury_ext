using System.Runtime.InteropServices;

namespace marcury_wpf.Forms
{
    public partial class OverlayForm : Form
    {
        public event Action<IntPtr>? OnWindowHandleDetected; // Event when handle is found

        /// <summary>
        /// Contructor OverlayForm
        /// </summary>
        public OverlayForm()
        {
            InitializeComponent();

            // Set a very light background color and lower opacity to clearly see the forms below
            this.BackColor = Color.FromArgb(200, 200, 200); // Very light gray
            this.Opacity = 0.15;                            // 15% opacity, makes the form more transparent

            // Set the border style and display state of the form
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true; // Set overlay on top of other windows
        }

        /// <summary>
        /// Handle event when mouse click on form overlay
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OverlayForm_MouseClick(object sender, MouseEventArgs e)
        {
            IntPtr handle = GetWindowHandleAtCursor();
            OnWindowHandleDetected?.Invoke(handle); // Call the event to send the handle to the main Form
            this.Close(); // Close overlay on click
        }

        /// <summary>
        /// Get the Handle of the window at the mouse cursor position
        /// </summary>
        /// <returns></returns>
        private IntPtr GetWindowHandleAtCursor()
        {
            WinApi.GetCursorPos(out Point cursorPos);
            return WinApi.WindowFromPoint(cursorPos);
        }

        /// <summary>
        /// Use when load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OverlayForm_Load(object sender, EventArgs e)
        {
        }
    }

    /// <summary>
    ///  Class to contain necessary Windows APIs
    /// </summary>
    public static class WinApi
    {
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point p);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);
    }
}
