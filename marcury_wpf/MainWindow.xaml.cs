using marcury_wpf.Forms;
using System.Collections.ObjectModel;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Input;
using System.Windows.Documents;
using System.ComponentModel.Design;
using System.Windows.Media;

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
        public ObservableCollection<DataItem> Items { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Items = new ObservableCollection<DataItem>();
            this.dgMarcuryEx.ItemsSource = Items;

            // Add sample data
            for (int i = 0; i < 10; i++) {
                Items.Add(new DataItem {
                    Column2 = $"Item {i + 1}",
                    Column3 = $"Details {i + 1}",
                    Column5 = $"More info {i + 1}",
                    Column6 = $"Text {i + 1}",
                    Column8 = $"Note {i + 1}",
                    Column9 = $"Other {i + 1}",
                    Column10 = $"Additional {i + 1}",
                    Column13 = $"Field {i + 1}",
                    Column16 = $"Last {i + 1}",
                    ImageSource = "D:\\source\\marcury_ext\\marcury_wpf\\Images\\imgEdit.png"
                });
            }
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

    /// <summary>
    /// Calss DataItem for data grid
    /// </summary>
    public class DataItem
    {
        public bool Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
        public bool Column4 { get; set; }
        public string Column5 { get; set; }
        public string Column6 { get; set; }
        public bool Column7 { get; set; }
        public string Column8 { get; set; }
        public string Column9 { get; set; }
        public string Column10 { get; set; }
        public bool Column12 { get; set; }
        public string Column13 { get; set; }
        public string Column14 { get; set; }
        public string ImageSource { get; set; }
        public string Column16 { get; set; }
        public string Column17 { get; set; }
    }
}