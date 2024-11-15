using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace marcury_ext
{
    public partial class MarcuExtDemo : Form
    {
        private bool isSearchMode = false;
        private OverlayForm overlayForm; // Declare overlayForm
        private CustomCursor customCursor; // Declare CustomCursor

        public MarcuExtDemo()
        {
            InitializeComponent();
            // Initialize custom cursor, initial state is not searching
            customCursor = new CustomCursor();
        }

        private void BtnStartSearch_Click(object sender, EventArgs e)
        {
            isSearchMode = !isSearchMode;
            this.overlayForm = new OverlayForm();
            if (isSearchMode) {
                // Create and display OverlayForm when starting search             
                this.overlayForm.Show();
                // Update search status and change cursor accordingly
                customCursor.IsSearching = isSearchMode;
                customCursor.UpdateCursor();  // Update cursor
                this.overlayForm.MouseClick += OverlayForm_MouseClick;
                labelStatus.Text = "Search mode is on! Click on any window to get the handle.";
            } else {
                // Close OverlayForm if search mode is off
                customCursor.Dispose();  //Free custom cursor
                this.overlayForm?.Close();
            }
        }

        private void OverlayForm_MouseClick(object sender, MouseEventArgs e)
        {
            // When clicking on a position in the OverlayForm, get the handle of the window there
            IntPtr handle = GetWindowHandleAtCursor();
            if (handle != IntPtr.Zero) {
                // Display the window's handle information and text content
                StringBuilder windowText = new StringBuilder(256);
                WinApi.GetWindowText(handle, windowText, windowText.Capacity);
                txtHandle.Text = $"Handle: {handle}";
                txtResult.Text = $"Text: {windowText.ToString()}";
                labelStatus.Text = "Target form information retrieved!";

                // Close OverlayForm after getting the handle
                this.overlayForm.Close();
                isSearchMode = false;  // Turn off search mode
                //Return to default mouse cursor
                this.Cursor = Cursors.Default;
                customCursor.IsSearching = isSearchMode;
                customCursor.UpdateCursor();  // Cập nhật lại con trỏ
                SendMessage(handle, WM_SETTEXT, 0, "Blablabla");
            } else {
                labelStatus.Text = "Target form not found at mouse position.";
            }
        }
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, int
       wParam, [MarshalAs(UnmanagedType.LPStr)] string lParam);
        private const int WM_SETTEXT = 0x0C;

        // Get the Handle of the window at the mouse cursor position
        private IntPtr GetWindowHandleAtCursor()
        {
            Point cursorPos = Cursor.Position;
            return WindowFromPoint(cursorPos); // Get handle at mouse position
        }

        // Get handle at mouse position
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point p);

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void MarcuExtDemo_Load(object sender, EventArgs e)
        {
            // Make sure ListView has columns: 原文, 一致率, 候補, 適用
            lvData.Columns.Add("原文", 300);
            lvData.Columns.Add("一致率", 70);
            lvData.Columns.Add("候補", 300);
            lvData.Columns.Add("適用", 50);

            // Set detail display mode
            lvData.View = View.Details;

            // Read data from XML file
            string filePath = "DataSource\\demo.xml";
            XDocument doc = XDocument.Load(filePath);
            var items = doc.Descendants("Item");

            foreach (var item in items) {
                string originalText = item.Element("原文")?.Value;
                string accuracy = item.Element("一致率")?.Value;
                string candidate = item.Element("候補")?.Value;
                string apply = item.Element("適用")?.Value;

                // Add item to ListView
                ListViewItem listViewItem = new ListViewItem(originalText);
                listViewItem.SubItems.Add(accuracy);
                listViewItem.SubItems.Add(candidate);
                listViewItem.SubItems.Add(apply);

                lvData.Items.Add(listViewItem);
            }

        }
    }
}
