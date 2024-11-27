using marcury_ext;
using marcury_ext.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using TextBox = System.Windows.Forms.TextBox;
using System.Windows.Automation;

namespace marcury_ext
{
    public partial class FormExtract : Form
    {
        private string txtDummy = "入力された2つの文章を比較し、差分チェックします。\r\n 発見された差分は以下のようにハイライト表示されます。\r\n・文字単位の比較を行い、変更部分を緑のハイライトで表示します。\r\n・文字単位の差分が発見された行番号を赤のハイライトで表示します。\r\n・改行や文章挿入箇所はグレーのハイライトが表示されます。";
        private bool isDragging = false;
        private bool isSearchMode = false;

        // Step status
        public static int m_status = NOTHING_STATUS;
        public static int NOTHING_STATUS = 0;
        public static int START_EDIT_STATUS = 1;
        public static int IN_UPDATING_STATUS = 2;
        public static int END_UPDATED_STATUS = 3;

        private OverlayForm overlayForm; // Declare overlayForm
        private CustomCursor customCursor; // Declare CustomCursor

        private IntPtr handleTarget;
        private string textTarget;
        const int WM_SETTEXT = 0x000C;
        private FrmLoadRichTb frmTransparent;

        public static void setStatus(int status) { m_status = status; }
        public static int getStatus() { return m_status; }

        private void BtnSearchRichTextBox_Click(object sender, EventArgs e)
        {
            // Btn BtnSearchRichTextBox

        }

        /// <summary>
        /// Constructor
        /// </summary>
        public FormExtract()
        {
            InitializeComponent();
            // Initialize custom cursor, initial state is not searching
            customCursor = new CustomCursor();
            SetTitleBarColor();
            // Attach the ItemChecked event to the ListView
            //lvData.ItemChecked += LvData_ItemChecked;
        }

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20; // Hoặc 19 cho các phiên bản cũ

        /// <summary>
        /// 
        /// </summary>
        private void SetTitleBarColor()
        {
            int color = 0x00FF00; // Mã màu xanh (RGB: 00FF00)
            DwmSetWindowAttribute(this.Handle, 35, ref color, Marshal.SizeOf(color)); // 35 là mã thuộc tính màu
        }

        /// <summary>
        /// When the search button is pressed (BtnStartSearch)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStartSearch_Click(object sender, EventArgs e)
        {
            if (getStatus() == IN_UPDATING_STATUS) {
                // Case not yet update content for textbox, but start search handle
                UpdateContentForTextBoxOriginAndCloseFormLoad();
            }
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
            try {
                // When clicking on a position in the OverlayForm, get the handle of the window there
                IntPtr handle = GetWindowHandleAtCursor();
                string fullText = "";
                if (handle != IntPtr.Zero) {
                    //WinApi.GetWindowText(handle, windowText, windowText.Capacity); // Stop use WinApi.GetWindowText
                    AutomationElement textBoxElement = AutomationElement.FromHandle(handle); // Use UI Automation change 
                    /* textBoxElement = null;*/
                    int errorCode = UtilCheckError.CheckAutomationElement(textBoxElement);
                    if (errorCode != UtilCheckError.NOT_ERROR) {
                        fullText = UtilCheckError.GetErrorMessage(errorCode);
                    } else if (textBoxElement.TryGetCurrentPattern(TextPattern.Pattern, out object patternObject)) {
                        TextPattern textPattern = (TextPattern)patternObject;
                        fullText = textPattern.DocumentRange.GetText(-1);
                    }
                    setStatus(START_EDIT_STATUS);
                    txtHandle.Text = $"Handle: {handle}";
                    //textTarget = windowText.ToString();
                    //txtResult.Text = $"Text: {textTarget}";
                    textTarget = fullText;
                    txtResult.Text = $"Text: {fullText}";
                    labelStatus.Text = "Target form information retrieved!";
                    handleTarget = handle; // Set value handle target SendMessage(handle, WM_SETTEXT, 0, this.textChangeFromMarExtra);
                    // Close OverlayForm after getting the handle
                    this.overlayForm.Close();
                    isSearchMode = false;  // Turn off search mode
                    // Load form new have richtextbox
                    LoadFormRich(errorCode);
                    Handlelevenshtein();
                    //Return to default mouse cursor
                    this.Cursor = Cursors.Default;
                    customCursor.IsSearching = isSearchMode;
                    customCursor.UpdateCursor();  // Update cursor in handle search mode: (currently not working) 
                } else {
                    labelStatus.Text = "Target form not found at mouse position.";
                    if (handle == IntPtr.Zero) UtilCheckError.GetErrorMessage(UtilCheckError.ERROR_HANDLE_ZERO);
                }
            } catch (Exception) {

                throw;
                // TODO: output message error
            }
           
        }

        /// <summary>
        /// Handlelevenshtein
        /// </summary>
        private void Handlelevenshtein()
        {
            //Clear old content on DataGridView and RichTextBox
            dataGridViewDb.Rows.Clear();
            frmTransparent.richTextBox.Clear();
            // Calculator Levenshtein after load Form have richTextBox
            LevenshteinDistance.HandleLevenshtein(dataGridViewDb, frmTransparent.richTextBox, textTarget, txtDummy);
        }

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.LPStr)] string lParam);

        /// <summary>
        /// LoadFormRich
        /// </summary>
        private void LoadFormRich(int errorCode)
        {
            frmTransparent = new FrmLoadRichTb(handleTarget);
            // Use public methods to update content and background color
            frmTransparent.SetRichTextBoxText(textTarget);
            if (errorCode == UtilCheckError.NOT_ERROR) setStatus(IN_UPDATING_STATUS);
            // Show form frmLoadRichTb
            frmTransparent.Show();
        }

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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMarkDataTextBox_Click(object sender, EventArgs e)
        {
            // Mark text in TextBox of form other
            MarkTextWithRange();
        }

        /// <summary>
        /// 
        /// </summary>
        private void MarkTextWithRange()
        {
            // Get content from TextBox
            string textMark = textTarget;
            // Check if index range is valid
            int startIndex = 19;
            int endIndex = 34;
            if (textMark != null) {
                if (startIndex >= 0 && endIndex <= textMark.Length) {
                    // Split the text before the paragraph to be edited
                    string before = textMark.Substring(0, startIndex);
                    // Split the text to be edited (from index 10 to 20)
                    string middle = textMark.Substring(startIndex, endIndex - startIndex);
                    // Split the text after the paragraph to be edited
                    string after = textMark.Substring(endIndex);
                    // Insert (***) into the part to be edited
                    string newText = before + "(***" + middle + "***)" + after;
                    IntPtr ptr = Marshal.StringToHGlobalUni(newText);
                    // Reattach content to TextBox
                    SendMessage(handleTarget, WM_SETTEXT, 0, ptr);
                }
            }  
        }

       /* private void SelectTextByHandle()
        {
            if (handleTarget != IntPtr.Zero) {
                Console.WriteLine($"Text handleTarget: {handleTarget}");
                int startIndex = 5;
                int endIndex = 10;
                int lParam = (endIndex << 16) | startIndex; // Cách tính lParam để chọn văn bản
                SendMessage(handleTarget, EM_SETSEL, 0, lParam);
                // Gửi thông điệp để đảm bảo TextBox có focus (đảm bảo TextBox nhận sự kiện chọn văn bản)
                SendMessage(handleTarget, 0x000B, 1, 0); // 0x000B là WM_SETREDRAW
            }
        }*/

      /*  // P/Invoke to call Windows API
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        private const int EM_SETSEL = 0xB1; // How to calculate lParam to select text*/


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
        /// FormExtract_Load
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void FormExtract_Load(object sender, EventArgs e)
        {
            // Add columns to DataGridView
            dataGridViewDb.Columns.Add("原文", "原文");
            dataGridViewDb.Columns.Add("一致率", "一致率");
            dataGridViewDb.Columns.Add("候補", "候補");

            // Add "適用" column as checkbox
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "適用";
            checkBoxColumn.Name = "適用";  // Tên cột là "適用"
            dataGridViewDb.Columns.Add(checkBoxColumn);
<<<<<<< HEAD
=======

            // Setup color for headers
            dataGridViewDb.ColumnHeadersDefaultCellStyle.BackColor = Color.Purple; // Nền màu tím
            dataGridViewDb.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;  // Chữ màu trắng
            dataGridViewDb.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold); // Font chữ in đậm
            dataGridViewDb.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Căn giữa
            dataGridViewDb.EnableHeadersVisualStyles = false; // Tắt visual styles mặc định
>>>>>>> c2021fc18da9a1e614ef30b4ed221cc6f7353028
            // Set column width
            dataGridViewDb.Columns["原文"].Width = 300;
            dataGridViewDb.Columns["一致率"].Width = 70;
            dataGridViewDb.Columns["候補"].Width = 300;
         
            // Assign events to DataGridView
            dataGridViewDb.CellContentClick += DataGridViewDb_CellContentClick;         
            dataGridViewDb.CellValueChanged += DataGridViewDb_CellValueChanged;
            // Change the height of the DataGridView
            dataGridViewDb.Height = 250; // Customize the height of the DataGridView
        }

        

        /// <summary>
        /// DataGridViewDb_CellContentClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewDb_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (getStatus() == IN_UPDATING_STATUS) {
                // Check if the clicked cell is a checkbox of the "適用" column
                if (e.ColumnIndex == dataGridViewDb.Columns["適用"].Index && e.RowIndex >= 0) {
                    // Get the current value of a cell
                    var cellValue = dataGridViewDb.Rows[e.RowIndex].Cells["適用"].Value;

                    // Check if value is null, set default value to false
                    if (cellValue == null || !(cellValue is bool)) {
                        dataGridViewDb.Rows[e.RowIndex].Cells["適用"].Value = true; // Check checkbox on click
                    } else {
                        // Otherwise, reverse the checkbox state
                        dataGridViewDb.Rows[e.RowIndex].Cells["適用"].Value = !(bool)cellValue;
                    }
                }
            }
        }

        /// <summary>
        /// DataGridViewDb_CellValueChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewDb_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the changed cell is a column checkbox
            if (e.ColumnIndex == dataGridViewDb.Columns["適用"].Index && e.RowIndex >= 0) {
                bool isChecked = (bool)dataGridViewDb.Rows[e.RowIndex].Cells["適用"].Value;

                if (isChecked) {
                    // Get value from column "候補" (column 3, index 2)
                    string candidateText = dataGridViewDb.Rows[e.RowIndex].Cells["候補"].Value.ToString();
                    string textNeedEdit = dataGridViewDb.Rows[e.RowIndex].Cells["原文"].Value.ToString();

                    // Change text in RichTextBox from indexStart to indexEnd
                    frmTransparent.UpdateTextOfLineRichTextBox(candidateText, textNeedEdit);
                    // Lấy chỉ số hàng vừa click
                    int clickedRowIndex = e.RowIndex;

                    // Xác định nhóm dựa trên hàng click (mỗi nhóm 3 hàng)
                    int groupIndex = clickedRowIndex/3;

                    // Vô hiệu hóa checkbox của các cell trong nhóm
                    DisableCheckboxesForGroup(groupIndex);
                    /*// Get the entire text from a RichTextBox
                    string updatedText = frmTransparent.GetDataRichTextBox();

                    // Convert text to IntPtr for use with SendMessage
                    IntPtr ptr = Marshal.StringToHGlobalUni(updatedText);

                    // Send message to update text in handleTarget (TextBox)
                    SendMessage(handleTarget, WM_SETTEXT, 0, ptr);

                    // Free up memory
                    Marshal.FreeHGlobal(ptr);*/
                    // Call again becase content in RichTextBox changed TODO:
                    // LevenshteinDistance.HandleLevenshtein(dataGridViewDb, frmTransparent.richTextBox, textTarget, txtDummy);
                }            
                /*// Once done, close and release frmTransparent
                CloseFrmLoadRich();*/
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        private void DisableGroupCheckbox(int startRow, int endRow)
        {
            // Loop through rows in the specified range
            for (int i = startRow; i < endRow && i < dataGridViewDb.Rows.Count; i++) {
                DataGridViewCell cell = dataGridViewDb.Rows[i].Cells["適用"];
                // Disable cell (just make it non-editable)
                cell.ReadOnly = true;
                // Disable cell (just make it non-editable)
                cell.ReadOnly = true;

                // Change the background color of the cell when it is disabled (dark or whatever color you want)
                cell.Style.BackColor = Color.LightGray;  // Màu sáng xám để biểu thị disable

                // If this cell is a checkbox, disable the checkbox and change the color
                if (cell is DataGridViewCheckBoxCell checkBoxCell) {
                    checkBoxCell.ReadOnly = true;
                    // Change the color of the checkbox so it looks disabled
                    checkBoxCell.Style.BackColor = Color.Silver;
                    checkBoxCell.Style.ForeColor = Color.Gray;
                }
            }
        }

        private void DisableCheckboxesForGroup(int groupIndex)
        {
            int startRow = groupIndex * 3;
            int endRow = startRow + 3;
            DisableGroupCheckbox(startRow, endRow);
        }


        /// <summary>
        /// Send updated data from RichTextBox to TextBox and close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDone_Click(object sender, EventArgs e)
        {
            UpdateContentForTextBoxOriginAndCloseFormLoad();
        }

        /// <summary>
        /// UpdateContentForTextBoxOriginAndCloseFormLoad
        /// </summary>
        private void UpdateContentForTextBoxOriginAndCloseFormLoad()
        {
            // Get the entire text from a RichTextBox and normalize line breaks
            string updatedText = frmTransparent.GetDataRichTextBox();
            updatedText = updatedText.Replace("\n", "\r\n"); // Ensure correct line breaks for TextBox

            // Get the target TextBox (optional, for additional checks)
            TextBox targetTextBox = (TextBox)Control.FromHandle(handleTarget);
            if (targetTextBox != null) {
                targetTextBox.Multiline = true; // Ensure it supports multiline
            }

            // Convert text to IntPtr for SendMessage
            IntPtr ptr = Marshal.StringToHGlobalUni(updatedText);

            // Send message to update text in TextBox
            SendMessage(handleTarget, WM_SETTEXT, 0, ptr);

            // Free allocated memory
            Marshal.FreeHGlobal(ptr);

            // Update status and close the form
            setStatus(END_UPDATED_STATUS);
            CloseFrmLoadRich();
        }

        /// <summary>
        /// CloseFrmLoadRich
        /// </summary>
        private void CloseFrmLoadRich()
        {
            // Once done, close and release frmTransparent
            frmTransparent.Close();  // Close form
            frmTransparent.Dispose();  // Free up resources
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


        /// <summary>
        /// Handle click Get string distance button
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void BtnHighLight_Click(object sender, EventArgs e)
        {
            LevenshteinDistance.HighlightDifferences(RichTextBoxHighLight, RichTextBoxHighLight.Text, TextBoxDB.Text);
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

        private void TBXStr1_TextChanged(object sender, EventArgs e)
        {

        }
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
