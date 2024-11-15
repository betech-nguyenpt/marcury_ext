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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace marcury_ext
{
    public partial class MarcuExtDemo : Form
    {
        private bool isSearchMode = false;
        private OverlayForm overlayForm; // Declare overlayForm
        private CustomCursor customCursor; // Declare CustomCursor

        private IntPtr handleTarget;
        private string textChangeFromMarExtra = "changed the text and turned it red!";
        private const int WM_SETTEXT = 0x0C;


        public MarcuExtDemo()
        {
            InitializeComponent();
            // Initialize custom cursor, initial state is not searching
            customCursor = new CustomCursor();
            // Gắn sự kiện ItemChecked vào ListView
            lvData.ItemChecked += LvData_ItemChecked;
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
                string texttarget = windowText.ToString();
                txtResult.Text = $"Text: {texttarget}";
                labelStatus.Text = "Target form information retrieved!";
                handleTarget = handle; // Set value handle target
                // Highlight all text in TextBox
                // Highlight alkin text in TextBox
                int textLength = texttarget.Length/2;
                HighlightText(handleTarget, 0, textLength);
                //SendMessage(handle, WM_SETTEXT, 0, this.textChangeFromMarExtra);
                // Close OverlayForm after getting the handle
                this.overlayForm.Close();
                isSearchMode = false;  // Turn off search mode
                //Return to default mouse cursor
                this.Cursor = Cursors.Default;
                customCursor.IsSearching = isSearchMode;
                customCursor.UpdateCursor();  // Update cursor in handle search mode: (currently not working)           
            } else {
                labelStatus.Text = "Target form not found at mouse position.";
            }
        }
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.LPStr)] string lParam);

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
            // Make sure ListView has columns: 適用, 原文, 一致率, 候補
            lvData.Columns.Add("適用", 50);
            lvData.Columns.Add("原文", 300);
            lvData.Columns.Add("一致率", 70);
            lvData.Columns.Add("候補", 300);

            // Enable Checkboxes for ListView items
            lvData.CheckBoxes = true;
            // Set detail display mode
            lvData.View = View.Details;

            // Enable gridlines for column and row separation
            lvData.GridLines = true;

            // Set full row selection (optional)
            lvData.FullRowSelect = true;

            // Add 23 rows of data
            for (int i = 1; i <= 10; i++) {
                string originalText = $"原文 {i}";
                string accuracy = $"{(i * 4)}%";
                string candidate = $"候補 {i}";

                // Add item to ListView
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Checked = false; // Set checkbox checked or unchecked for the item

                // Add the rest of the columns
                listViewItem.SubItems.Add(originalText);
                listViewItem.SubItems.Add(accuracy);
                listViewItem.SubItems.Add(candidate);

                lvData.Items.Add(listViewItem);
            }
        }

        private void LvData_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            // Check if checkbox is selected
            if (e.Item.Checked) {
                // Get the value from column "候補" (column 3, index 2)
                string candidateText = e.Item.SubItems[3].Text;
                // Convert the string to IntPtr for correct Unicode encoding
                IntPtr ptr = Marshal.StringToHGlobalUni(candidateText);
                // Send a message SendMessage to change the text
                SendMessage(handleTarget, WM_SETTEXT, 0, ptr);
                // Free up memory
                Marshal.FreeHGlobal(ptr);
            }
        }
        // Import library with IntPtr receiving version
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        private void HighlightText(IntPtr handleTextB, int startIndex, int length)
        {
            int endIndex = startIndex + length;
            // Chuyển startIndex và endIndex thành IntPtr
            IntPtr startPtr = new IntPtr(startIndex);
            IntPtr endPtr = new IntPtr(endIndex);
            // Gửi thông điệp để chọn văn bản, làm highlight
            SendMessage(handleTextB, EM_SETSEL, startPtr, endPtr);
        }
        // Khai báo hàm SendMessage từ thư viện user32.dll
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private const int EM_SETSEL = 0xB1; // Message để chọn văn bản
    }
}
