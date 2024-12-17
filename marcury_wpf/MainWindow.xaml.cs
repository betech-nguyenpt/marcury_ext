using marcury_wpf.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace marcury_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isSearchMode = false;
        private OverlayForm overlayForm; // Declare overlayForm
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
        /// Load OverLoadForm for get handle textbox target
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OverlayForm_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try {
                // When clicking on a position in the OverlayForm, get the handle of the window there
                IntPtr handle = GetWindowHandleAtCursor();
                string fullText = "";
                if (handle == IntPtr.Zero) {
                    throw new Exception("123");
                }
                AutomationElement textBoxElement = AutomationElement.FromHandle(handle);
                if (textBoxElement.TryGetCurrentPattern(TextPattern.Pattern, out object patternObject)) {
                    TextPattern textPattern = (TextPattern)patternObject;
                    fullText = textPattern.DocumentRange.GetText(-1);
                    TbxResult.Text = fullText;
                }
                // Close OverlayForm after getting the handle
                this.overlayForm.Close();
            } catch (Exception ex) {
            }
        }

        // Get the Handle of the window at the mouse cursor position
        private IntPtr GetWindowHandleAtCursor()
        {
            System.Drawing.Point cursorPos = System.Windows.Forms.Cursor.Position;
            return WindowFromPoint(cursorPos); // Get handle at mouse position
        }

        // Get handle at mouse position
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(System.Drawing.Point p);
    }
}