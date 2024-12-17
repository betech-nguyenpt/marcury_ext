using marcury_wpf.Forms;
using marcury_wpf.SubWindow;
using marcury_wpf.Utils;
using System.Drawing;
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
using Cursors = System.Windows.Input.Cursors;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace marcury_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isSearchMode = false;
        private OverlayForm overlayForm; // Declare overlayForm
        private System.Windows.Controls.TextBox textResult; // Declare overlayForm
        private OverlayWindow overlayWindow;
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

        // Import function from WinAPI
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);

        // Define
        private const uint WM_GETTEXT = 0x000D;
        private const uint WM_GETTEXTLENGTH = 0x000E;



        private void BtnGetText_Click(object sender, RoutedEventArgs e)
        {
            textBox.Text = GetText();
        }

        private string GetText()
        {
            // Search by name orther app
            // Search main (form) of orther app
            IntPtr mainWindowHandle = FindWindow(null, "FormMarcury");

            if (mainWindowHandle == IntPtr.Zero) {
                MessageBox.Show("Not found window.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
                return "";
            }

            // Iterate through the entire control tree and get the text
            StringBuilder collectedText = new StringBuilder();
            GetAllControlsText(mainWindowHandle, collectedText);
            return collectedText.ToString();
        }

        private void GetAllControlsText(IntPtr parentHandle, StringBuilder collectedText)
        {
            IntPtr childHandle = IntPtr.Zero;

            // Loop through child controls
            while ((childHandle = FindWindowEx(parentHandle, childHandle, null, null)) != IntPtr.Zero) {
                // Get text from control
                string controlText = GetContentTextFromHandle(childHandle);
                if (!string.IsNullOrEmpty(controlText)) {
                    collectedText.AppendLine($"Handle: {childHandle}, Text: {controlText}");
                }

                // Recursively check child controls of this control
                GetAllControlsText(childHandle, collectedText);
            }
        }

        private string GetContentTextFromHandle(IntPtr hWnd)
        { 
             // Get length text
             int length = SendMessage(hWnd, WM_GETTEXTLENGTH, 0, null);
             if (length == 0) return string.Empty;

             // Create buffer for get text
             StringBuilder sb = new StringBuilder(length + 1);
             SendMessage(hWnd, WM_GETTEXT, sb.Capacity, sb);

             return sb.ToString();
        }

        private void BtnSearchHandleWFA_Click(object sender, RoutedEventArgs e)
        {
            isSearchMode = true;
            this.overlayForm = new OverlayForm();
            if (isSearchMode) {
                // Create and display OverlayForm when starting search             
                this.overlayForm.Show();
                // Register the Paint event to draw the border
                //this.overlayForm.MouseClick += OverlayForm_MouseClick;
                textResult = textBox;
            } else {
                this.overlayForm?.Close();
            }
        }

        /// <summary>
        /// Load OverLoadForm for get handle textbox target
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OverlayForm_MouseClick(object sender, MouseEventArgs e)
        {
            try {
                // When clicking on a position in the OverlayForm, get the handle of the window there
                IntPtr handle = GetWindowHandleAtCursor();
                //IntPtr handle = this.overlayForm.GetWindowHandleAtCursor();
                IntPtr overlayFormHandle = FindWindow(null, "OverlayForm");
                string fullText = "";
                if (handle != IntPtr.Zero) {
                    if (overlayFormHandle == handle) {
                        MessageBox.Show("Error.", "This is handle of overlayForm: " + handle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    //WinApi.GetWindowText(handle, windowText, windowText.Capacity); // Stop use WinApi.GetWindowText
                    AutomationElement textBoxElement = AutomationElement.FromHandle(handle); // Use UI Automation change 
                    textBox.Text = $"Text: {fullText}";
                    // Close OverlayForm after getting the handle
                    this.overlayForm.Close();
                    isSearchMode = false;  // Turn off search mode
                } else {
                    textBox.Text = "Target form not found at mouse position.";
                }
            } catch (Exception) {

                throw;
                // TODO: output message error
            }

        }

        // Get the Handle of the window at the mouse cursor position
        private IntPtr GetWindowHandleAtCursor()
        {
            var position = MouseHelper.GetMousePosition();
            Console.WriteLine($"Mouse Position: {position.X}, {position.Y}");
            System.Drawing.Point cursorPos = position;
            return WindowFromPoint(cursorPos); // Get handle at mouse position
        }

        // Get handle at mouse position
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(System.Drawing.Point p);

        private void BtnSearchHandleWPF_Click(object sender, RoutedEventArgs e)
        {
            if (overlayWindow == null) {
                overlayWindow = new OverlayWindow();
            }
            overlayWindow.Show();

        }
    }
}