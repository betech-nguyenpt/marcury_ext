using marcury_wpf.Forms;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Input;

namespace marcury_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Set value for check search handle
        private bool isSearchMode = false;
        private OverlayForm? overlayForm; // Form load full desktop
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Handle mouse down in version StatusBarItem
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private void SbiVersion_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbxStatus.Text = "Changed log form need implement first!";
        }

        /// <summary>
        /// Handle event when click button BtnSearchHandle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearchHandle_Click(object sender, RoutedEventArgs e)
        {
            isSearchMode = !isSearchMode;
            this.overlayForm = new OverlayForm();
            if (isSearchMode) {
                // Create and display OverlayForm when starting search             
                this.overlayForm.Show();
                this.overlayForm.MouseClick += OverlayForm_MouseClick;
            } else {
                this.overlayForm.Close();
                this.overlayForm.Dispose();
            }
        }

        /// <summary>
        /// Handle event when mouse click in overlayForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OverlayForm_MouseClick(object? sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (sender is null) return;
            try {
                // When clicking on a position in the OverlayForm, get the handle of the window there
                IntPtr handle = GetWindowHandleAtCursor();
                string fullText = "";
                if (handle == IntPtr.Zero) {
                    throw new Exception("Handle target not found");
                }
                AutomationElement textBoxElement = AutomationElement.FromHandle(handle);
                if (textBoxElement.TryGetCurrentPattern(TextPattern.Pattern, out object patternObject)) {
                    TextPattern textPattern = (TextPattern)patternObject;
                    fullText = textPattern.DocumentRange.GetText(-1);
                    TbxResult.Text = fullText;
                }
                // Close OverlayForm after getting the handle
                if (this.overlayForm != null) this.overlayForm.Close();
            } catch (Exception ex) {
                throw new Exception($"Handle target not found { ex.Message }");
            }
        }

        /// <summary>
        /// Get the Handle of the window at the mouse cursor position
        /// </summary>
        /// <returns></returns>
        private IntPtr GetWindowHandleAtCursor()
        {
            System.Drawing.Point cursorPos = System.Windows.Forms.Cursor.Position;
            return WindowFromPoint(cursorPos); // Get handle at mouse position
        }

        /// <summary>
        /// Call API in user32.dll
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(System.Drawing.Point p);
    }
}