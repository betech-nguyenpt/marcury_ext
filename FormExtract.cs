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
using marcury_ext.ThrowException;

namespace marcury_ext
{
    public partial class FormExtract : Form
    {
        private string txtDummy = "入力された2つの文章を比較し、差分チェックします。\r\n 発見された差分は以下のようにハイライト表示されます。\r\n・文字単位の比較を行い、変更部分を緑のハイライトで表示します。\r\n・文字単位の差分が発見された行番号を赤のハイライトで表示します。\r\n・改行や文章挿入箇所はグレーのハイライトが表示されます。";
        private bool isSearchMode = false;

        // Step status
        public static int m_status = NOTHING_STATUS;
        public const int ERROR_STATUS = -1;
        public const int NOTHING_STATUS = 0;
        public const int START_EDIT_STATUS = 1;
        public const int IN_UPDATING_STATUS = 2;
        public const int END_UPDATED_STATUS = 3;

        private OverlayForm overlayForm; // Declare overlayForm
        private CustomCursor customCursor; // Declare CustomCursor

        private IntPtr m_handleTarget;
        private string m_textTarget;
        const int WM_SETTEXT = 0x000C;
        private FrmLoadRichTb frmTransparent;

        public static void setStatus(int status) { m_status = status; }
        public static int getStatus() { return m_status; }

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

        /// <summary>
        /// 
        /// </summary>
        private void SetTitleBarColor()
        {
            int color = 0x00FF00; // 青色のカラーコード (RGB: 00FF00)
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
                labelStatus.Text = GetStatusMessage(START_EDIT_STATUS);            
            } else {
                // Close OverlayForm if search mode is off
                customCursor.Dispose();  //Free custom cursor
                this.overlayForm?.Close();
                this.overlayForm.Dispose();
            }
        }

        /// <summary>
        /// Get Status Message
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public string GetStatusMessage(int statusCodeNotice)
        {
            string message;

            switch (statusCodeNotice) {
                case START_EDIT_STATUS:
                    message = "※ ハンドルを取得するには任意のウィンドウをクリックします.";
                    break;
                case IN_UPDATING_STATUS:
                    message = "※ ターゲットフォーム情報が取得されました!";
                    break;
                case END_UPDATED_STATUS:
                    message = "※ テキスト編集を終了!";
                    break;
                case ERROR_STATUS:
                    message = "※ エラー。もう一度お試しください.";
                    break;
                default:
                    message = "※ 無効なステータスコード.";
                    break;
            }

            return message;
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
                m_handleTarget = handle;
                string fullText = "";
                if (handle == IntPtr.Zero) {
                    throw new MarcuryExtractException(UtilErrors.ERROR_HANDLE_ZERO, UtilErrors.GetErrorMessage(UtilErrors.ERROR_HANDLE_ZERO));
                }
                //WinApi.GetWindowText(handle, windowText, windowText.Capacity);
                // Stop use WinApi.GetWindowText  Use UI Automation change 
                AutomationElement textBoxElement = AutomationElement.FromHandle(handle);
                UtilErrors.CheckAutomationElement(textBoxElement);
                if (textBoxElement.TryGetCurrentPattern(TextPattern.Pattern, out object patternObject)) {
                    TextPattern textPattern = (TextPattern)patternObject;
                    fullText = textPattern.DocumentRange.GetText(-1);
                }
                setStatus(START_EDIT_STATUS);
                txtHandle.Text = $"Handle: {handle}";
                //m_textTarget = windowText.ToString();
                //txtResult.Text = $"Text: {textTarget}";
                m_textTarget = fullText;   
                txtResult.Text = $"Text: {fullText}";
                // Close OverlayForm after getting the handle
                this.overlayForm.Close();
                isSearchMode = false; // Turn off search mode
                // Load form new have richtextbox
                LoadFormRich(UtilErrors.SUCCESS);
                Handlelevenshtein();
                //Return to default mouse cursor
                this.Cursor = Cursors.Default;
                customCursor.IsSearching = isSearchMode;
                customCursor.UpdateCursor();  // Update cursor in handle search mode: (currently not working) 
            } catch (MarcuryExtractException ex) {
                labelStatus.Text = GetStatusMessage(ERROR_STATUS);
                setStatus(ERROR_STATUS);
                // TODO: write log file
                Console.WriteLine($"Error code: {ex.ErrorCode}, Exception caught: {ex.Message}");
                LoadFormRich(ex.ErrorCode);
            } catch (Exception ex) {
                labelStatus.Text = GetStatusMessage(ERROR_STATUS);
                setStatus(ERROR_STATUS);
                // Print error information before rethrowing exception
                Console.WriteLine($"Error code: {UtilErrors.ERROR_UNKNOW_CODE}, Exception caught: {ex.Message}");
                LoadFormRich(UtilErrors.ERROR_UNKNOW_CODE);
                throw;
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
            LevenshteinDistance.HandleLevenshtein(dataGridViewDb, frmTransparent.richTextBox, m_textTarget, txtDummy);
        }

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.LPStr)] string lParam);

        /// <summary>
        /// LoadFormRich
        /// </summary>
        private void LoadFormRich(int errorCode)
        {
            frmTransparent = new FrmLoadRichTb(m_handleTarget);
            if (errorCode == UtilErrors.SUCCESS) {
                labelStatus.Text = GetStatusMessage(IN_UPDATING_STATUS);
                // Move status in update Text
                setStatus(IN_UPDATING_STATUS);           
                frmTransparent.SetRichTextBoxText(m_textTarget);
            } else {
                setStatus(ERROR_STATUS);
                frmTransparent.IsNotLimitSize = false;
                string infoErrorStr = $"Error code: {errorCode}, Exception caught: {UtilErrors.GetErrorMessage(errorCode)}";
                frmTransparent.SetRichTextBoxText(infoErrorStr);
            }          
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
        /// FormExtract_Load
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void FormExtract_Load(object sender, EventArgs e)
        {
            CreateColumnsForDataGridView();
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
                }            
            }
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
            if (getStatus() == ERROR_STATUS || getStatus() != IN_UPDATING_STATUS) {
                // Do nothing
            } else {
                // Get the entire text from a RichTextBox and normalize line breaks
                string updatedText = frmTransparent.GetDataRichTextBox();
                updatedText = updatedText.Replace("\n", "\r\n"); // Ensure correct line breaks for TextBox

                // Get the target TextBox (optional, for additional checks)
                TextBox targetTextBox = (TextBox)Control.FromHandle(m_handleTarget);
                if (targetTextBox != null) {
                    targetTextBox.Multiline = true; // Ensure it supports multiline
                }

                // Convert text to IntPtr for SendMessage
                IntPtr ptr = Marshal.StringToHGlobalUni(updatedText);

                // Send message to update text in TextBox
                SendMessage(m_handleTarget, WM_SETTEXT, 0, ptr);

                // Free allocated memory
                Marshal.FreeHGlobal(ptr);
            }
            // Update status and close the form
            labelStatus.Text = GetStatusMessage(END_UPDATED_STATUS);
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
        ///  LvData_ItemChecked send text when checked box // Not use
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
                SendMessage(m_handleTarget, WM_SETTEXT, 0, ptr);
                // Free up memory
                Marshal.FreeHGlobal(ptr);
            }
        }
        // Import library with IntPtr receiving version
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

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
