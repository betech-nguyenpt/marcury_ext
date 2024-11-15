using marcury_ext;
using marcury_ext.Utils;
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
using System.Xml;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace marcury_ext
{
    public partial class FormExtract : Form
    {
        /// <summary>
        /// Flag dragging mode
        /// </summary>
        private bool isDragging = false;
        private bool isSearchMode = false;
        private OverlayForm overlayForm; // Declare overlayForm
        private CustomCursor customCursor; // Declare CustomCursor

        private IntPtr handleTarget;
        private string textChangeFromMarExtra = "changed the text and turned it red!";
        private const int WM_SETTEXT = 0x0C;

        /// <summary>
        /// Constructor
        /// </summary>
        public FormExtract()
        {
            InitializeComponent();
            // Initialize custom cursor, initial state is not searching
            customCursor = new CustomCursor();
            // Attach the ItemChecked event to the ListView
            lvData.ItemChecked += LvData_ItemChecked;
        }

        /// <summary>
        /// When the search button is pressed (BtnStartSearch)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Load OverLoadForm for get handle textbox target
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                // SendMessage(handle, WM_SETTEXT, 0, this.textChangeFromMarExtra);
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


        /// <summary>
        /// Handle click close button
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            // Quit application
            Application.Exit();
        }

        /// <summary>
        /// Handle click Get text data button
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void BtnGetTextData_Click(object sender, EventArgs e)
        {
            var allText = GetAllTextFromWindowByTitle("FormMarcury");
            this.AppendTextToResult(allText.ToString());
            // Turn on dragging mode
            //this.isDragging = true;
            //this.AppendTextToResult("Dragging mode is ON");
        }

        /// <summary>
        /// Handle mouse click on form
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void FormExtract_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.isDragging)
            {
                this.isDragging = false;
                this.AppendTextToResult("Dragging mode is OFF");
            }
        }

        /// <summary>
        /// Handle form load
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void FormExtract_Load(object sender, EventArgs e)
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

        /// <summary>
        ///  LvData_ItemChecked send text when checked box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

       /* /// <summary>
        ///  HighlightText
        /// </summary>
        /// <param name="handleTextB"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        private void HighlightText(IntPtr handleTextB, int startIndex, int length)
        {
            int endIndex = startIndex + length;
            // Chuyển startIndex và endIndex thành IntPtr
            IntPtr startPtr = new IntPtr(startIndex);
            IntPtr endPtr = new IntPtr(endIndex);
            // Gửi thông điệp để chọn văn bản, làm highlight
            SendMessage(handleTextB, EM_SETSEL, startPtr, endPtr);
        }
        // Declare the SendMessage function from the user32.dll library
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private const int EM_SETSEL = 0xB1; // Message để chọn văn bản*/

        /// <summary>
        /// Handle click Get string distance button
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void BtnGetStringDistance_Click(object sender, EventArgs e)
        {
            int dist = LevenshteinDistance.Calculate(TBXStr1.Text, TBXStr2.Text);
            LBLResult.Text = "Distance is " + dist;
        }

        /// <summary>
        /// Append text to result textbox
        /// </summary>
        /// <param name="text">Text to append</param>
        private void AppendTextToResult(String text)
        {
            txtResult.AppendText(text);
            txtResult.AppendText(Environment.NewLine);
        }

        /// <summary>
        /// Callback method used to collect a list of child windows we need to capture text from.
        /// </summary>
        /// <param name="handle">IntPtr</param>
        /// <param name="pointer">IntPtr</param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"></exception>
        private static bool EnumChildWindowsCallback(IntPtr handle, IntPtr pointer)
        {
            // Creates a managed GCHandle object from the pointer representing a handle to the list created in GetChildWindows.
            var gcHandle = GCHandle.FromIntPtr(pointer);

            // Casts the handle back back to a List<IntPtr>
            var list = gcHandle.Target as List<IntPtr>;

            if (list == null)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }

            // Adds the handle to the list.
            list.Add(handle);

            return true;
        }

        /// <summary>
        /// Returns an IEnumerable<IntPtr> containing the handles of all child windows of the parent window.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        private static IEnumerable<IntPtr> GetChildWindows(IntPtr parent)
        {
            // Create list to store child window handles.
            var result = new List<IntPtr>();

            // Allocate list handle to pass to EnumChildWindows.
            var listHandle = GCHandle.Alloc(result);

            try
            {
                // Enumerates though all the child windows of the parent represented by IntPtr parent, executing EnumChildWindowsCallback for each. 
                EnumChildWindows(parent, EnumChildWindowsCallback, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                // Free the list handle.
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }

            // Return the list of child window handles.
            return result;
        }

        /// <summary>
        /// Gets text text from a control by it's handle.
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        private static string GetText(IntPtr handle)
        {
            const uint WM_GETTEXTLENGTH = 0x000E;
            const uint WM_GETTEXT = 0x000D;

            // Gets the text length.
            var length = (int)SendMessage(handle, WM_GETTEXTLENGTH, IntPtr.Zero, null);

            // Init the string builder to hold the text.
            var sb = new StringBuilder(length + 1);

            // Writes the text from the handle into the StringBuilder
            SendMessage(handle, WM_GETTEXT, (IntPtr)sb.Capacity, sb);

            // Return the text as a string.
            return sb.ToString();
        }

        /// <summary>
        /// Wraps everything together. Will accept a window title and return all text in the window that matches that window title.
        /// </summary>
        /// <param name="windowTitle">Title of window</param>
        /// <returns>String of content</returns>
        private static string GetAllTextFromWindowByTitle(string windowTitle)
        {
            var sb = new StringBuilder();

            try
            {
                // Find the main window's handle by the title.
                var windowHWnd = FindWindowByCaption(IntPtr.Zero, windowTitle);

                // Loop though the child windows, and execute the EnumChildWindowsCallback method
                var childWindows = GetChildWindows(windowHWnd);

                // For each child handle, run GetText
                foreach (var childWindowText in childWindows.Select(GetText))
                {
                    // Append the text to the string builder.
                    sb.Append(childWindowText);
                }

                // Return the windows full text.
                return sb.ToString();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

            return string.Empty;
        }
        
        // Delegate we use to call methods when enumerating child windows.
        private delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr i);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindowByCaption(IntPtr zeroOnly, string lpWindowName);

        private void button1_Click(object sender, EventArgs e)
        {

        }
        
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, [Out] StringBuilder lParam);
    }
}

// Class to contain necessary Windows APIs
public static class WinApi
{
    [DllImport("user32.dll")]
    public static extern IntPtr WindowFromPoint(Point p);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern int GetWindowText(IntPtr hwnd, StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out Point lpPoint);
}
