using DiffPlex.Chunkers;
using DiffPlex;
using marcury_ext.ThrowException;
using marcury_ext.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace marcury_ext
{
    public partial class FormMainExtract : Form
    {
        public static string txtDummy = "入力された2つの文章を比較し、差分チェックします。\r\n 発見された差分は以下のようにハイライト表示されます。\r\n・文字単位の比較を行い、変更部分を緑のハイライトで表示します。\r\n・文字単位の差分が発見された行番号を赤のハイライトで表示します。\r\n・改行や文章挿入箇所はグレーのハイライトが表示されます。";
        private bool isSearchMode = false;

        // Step status
        public static int m_status = NOTHING_STATUS;
        public const int ERROR_STATUS = -1;
        public const int NOTHING_STATUS = 0;
        public const int START_EDIT_STATUS = 1;
        public const int IN_UPDATING_STATUS = 2;
        public const int APPLYED_STATUS = 3;
        public const int END_UPDATED_STATUS = 4;

        private OverlayForm overlayForm; // Declare overlayForm
        private CustomCursor customCursor; // Declare CustomCursor

        private IntPtr m_handleTarget;
        private string m_textTarget;
        const int WM_SETTEXT = 0x000C;
        private FrmLoadRichTb frmTransparent;
        private FormShop frmShop;

        public static void setStatus(int status) { m_status = status; }
        public static int getStatus() { return m_status; }

        public FormMainExtract()
        {
            InitializeComponent();
            customCursor = new CustomCursor();
        }

        private void FormBeExtract_Load(object sender, EventArgs e)
        {
            CreateColumnDataGridView();
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
        /// Handlelevenshtein
        /// </summary>
        private void Handlelevenshtein()
        {
            //Clear old content on DataGridView and RichTextBox
            dgvExtract.Rows.Clear();
            frmTransparent.richTextBox.Clear();
            // Calculator Levenshtein after load Form have richTextBox
            LevenshteinDistance.HandleLevenshtein(dgvExtract, frmTransparent.richTextBox, m_textTarget, txtDummy);
        }

        /// <summary>
        /// LoadFormRich
        /// </summary>
        private void LoadFormRich(int errorCode)
        {
            frmTransparent = new FrmLoadRichTb(m_handleTarget, this);
            frmTransparent.IsNotLimitSize = false;
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

        /// <summary>
        /// UpdateContentForTextBoxOriginAndCloseFormLoad
        /// </summary>
        public void UpdateContentForTextBoxOriginAndCloseFormLoad()
        {
            if (getStatus() == APPLYED_STATUS) {
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
            CloseFrmShop();
        }

        // Import library with IntPtr receiving version
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        /// <summary>
        /// CloseFrmLoadRich
        /// </summary>
        public void CloseFrmLoadRich()
        {
            if (frmTransparent == null) return;
            // Once done, close and release frmTransparent
            frmTransparent.Close();  // Close form
            frmTransparent.Dispose();  // Free up resources
        }

        /// <summary>
        /// CloseFrmLoadRich
        /// </summary>
        public void CloseFrmShop()
        {
            if (frmShop == null) return;
            frmShop.Close();
            frmShop.Dispose();
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

        private void dgvExtract_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có click vào cell của cột chứa ảnh (ví dụ cột "dgvCol10")
            if (e.ColumnIndex == dgvExtract.Columns["dgvCol12"].Index) {
                // Lấy dòng của cell đã click
                DataGridViewRow clickedRow = dgvExtract.Rows[e.RowIndex];
                // Lấy text của cell trong cột "dgvCol6"
                string col6Text = dgvExtract.Rows[e.RowIndex].Cells["dgvCol8"].Value.ToString();
                // Thay đổi màu nền của cell khi click
                clickedRow.Cells["dgvCol12"].Style.BackColor = Color.Yellow; // Ví dụ thay đổi màu vàng

                // Hoặc thay đổi màu nền của cả dòng
                //clickedRow.DefaultCellStyle.BackColor = Color.LightGreen; // Ví dụ thay đổi màu nền của dòng
                // Load form new have richtextbox
                LoadFormShop(e, col6Text, UtilErrors.SUCCESS);
            }
        }

        /// <summary>
        /// LoadFormRich
        /// </summary>
        private void LoadFormShop(DataGridViewCellEventArgs cellIndexTarget, String text, int errorCode)
        {
            frmShop = new FormShop(dgvExtract, frmTransparent, cellIndexTarget);
            if (errorCode == UtilErrors.SUCCESS) {
                labelStatus.Text = GetStatusMessage(IN_UPDATING_STATUS);
                // Move status in update Text
                setStatus(IN_UPDATING_STATUS);
                FormShop.cellIndexTarget = cellIndexTarget;
                FormShop.dgvFormMain = dgvExtract;
                frmShop.SetRichTextBoxText(text);
            } else {
                setStatus(ERROR_STATUS);
                string infoErrorStr = $"Error code: {errorCode}, Exception caught: {UtilErrors.GetErrorMessage(errorCode)}";
                frmShop.SetRichTextBoxText(infoErrorStr);
            }
            // Show form frmLoadRichTb
            frmShop.Show();
        }

        // Hàm CellPainting cho DataGridView
        private void dgvExtract_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            e.PaintBackground(e.ClipBounds, true);
            e.PaintContent(e.ClipBounds);

            if (string.IsNullOrEmpty(searchString)) return;

            var cellValue = e.FormattedValue?.ToString();
            if (string.IsNullOrEmpty(cellValue)) return;

            var positions = Regex.Matches(cellValue, searchString);
            if (positions.Count == 0) return;

            using (var format = new StringFormat(StringFormatFlags.FitBlackBox)) {
                format.LineAlignment = StringAlignment.Center;

                if (e.CellStyle.WrapMode == DataGridViewTriState.False ||
                    e.CellStyle.WrapMode == DataGridViewTriState.NotSet) {
                    format.FormatFlags |= StringFormatFlags.NoWrap;
                }

                format.SetMeasurableCharacterRanges(positions.OfType<Match>()
                      .Select(m => new CharacterRange(m.Index, m.Length)).ToArray());

                var regions = e.Graphics.MeasureCharacterRanges(cellValue, e.CellStyle.Font, e.CellBounds, format);

                using (var brush = new SolidBrush(Color.FromArgb(80, Color.Fuchsia))) {
                    foreach (var region in regions) {
                        e.Graphics.FillRegion(brush, region);
                        e.Graphics.DrawRectangle(Pens.Red, Rectangle.Round(region.GetBounds(e.Graphics)));
                    }
                }
            }

            e.Handled = true;
        }


        private string searchString = "";

        // Hàm KeyDown cho TextBox
        private void tbSearchKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                e.SuppressKeyPress = true;
                searchString = (sender as Control).Text;
                dgvExtract.Invalidate(); // Yêu cầu DataGridView vẽ lại để áp dụng highlight
            }
        }

        // Sự kiện TextChanged cho TextBox
        private void tbSearchKey_TextChanged(object sender, EventArgs e)
        {
            searchString = (sender as TextBox)?.Text ?? string.Empty;
            dgvExtract.Invalidate(); // Yêu cầu DataGridView vẽ lại để áp dụng highlight
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

        private void BtnTestConnDb_Click(object sender, EventArgs e)
        {
            TestConnectServer();
        }

        private void TestConnectServer()
        {
            string connectionString = @"Server=BETECH_ANHLT\SQLEXPRESS;Database=ExtractDb;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                try {
                    connection.Open();
                    Console.WriteLine("Kết nối thành công!");
                    
                } catch (Exception ex) {
                    Console.WriteLine("Lỗi kết nối: " + ex.Message);
                }
                connection.Close();
            }

            string resourceFolder = @"DataSource\"; // Thay bằng đường dẫn thư mục chứa file CSV
                                                    // Mở kết nối và thực hiện nhập dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                DeleteData(connection);
                // Nhập dữ liệu từ Partner.csv vào bảng Partner
                ImportCsvToDatabase(connection, connectionString, Path.Combine(resourceFolder, "Partner.csv"), "Partner");
                // Nhập dữ liệu từ ShopPartner.csv vào bảng ShopPartner
                ImportCsvToDatabase(connection, connectionString, Path.Combine(resourceFolder, "ShopPartner.csv"), "ShopPartner");
                connection.Close();
            }
                

            
        }

        static void ImportCsvToDatabase(SqlConnection connection, string connectionString, string csvFilePath, string tableName)
        {
            try {
               
                Console.WriteLine($"Bắt đầu nhập dữ liệu từ {csvFilePath} vào bảng {tableName}...");

                // Đọc dữ liệu từ file CSV
                DataTable dataTable = ReadCsvToDataTable(csvFilePath);
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection)) {
                    bulkCopy.DestinationTableName = $"dbo.{tableName}";

                    // Ánh xạ các cột từ DataTable sang bảng SQL Server
                    foreach (DataColumn column in dataTable.Columns) {
                        bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                    }

                    // Nhập dữ liệu
                    bulkCopy.WriteToServer(dataTable);
                }

                Console.WriteLine($"Dữ liệu từ {csvFilePath} đã nhập thành công vào bảng {tableName}.");
            } catch (Exception ex) {
                Console.WriteLine($"Lỗi khi nhập dữ liệu vào bảng {tableName}: {ex.Message}");
            }
        }

        static DataTable ReadCsvToDataTable(string csvFilePath)
        {
            DataTable dataTable = new DataTable();

            try {
                using (StreamReader reader = new StreamReader(csvFilePath, System.Text.Encoding.UTF8)) {
                    string[] headers = reader.ReadLine().Split(',');

                    // Tạo cột cho DataTable
                    foreach (string header in headers) {
                        dataTable.Columns.Add(header);
                    }

                    // Thêm dữ liệu từ CSV vào DataTable
                    while (!reader.EndOfStream) {
                        string[] rows = reader.ReadLine().Split(',');

                        // Xử lý dữ liệu trước khi thêm vào DataTable
                        for (int i = 0; i < rows.Length; i++) {
                            // Kiểm tra và thay thế giá trị không hợp lệ cho cột kiểu int
                            if (dataTable.Columns[i].DataType == typeof(int)) {
                                // Nếu giá trị là chuỗi không hợp lệ, thay thế bằng 0 (hoặc giá trị mặc định khác)
                                if (!int.TryParse(rows[i], out int result)) {
                                    rows[i] = "0"; // Hoặc có thể thay thế bằng giá trị NULL nếu cột cho phép NULL
                                }
                            }
                        }

                        // Thêm hàng vào DataTable
                        dataTable.Rows.Add(rows);
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine($"Lỗi khi đọc file CSV: {ex.Message}");
            }

            return dataTable;
        }

        static void DeleteData(SqlConnection connection)
        {

            // Tạo câu lệnh SQL để xóa dữ liệu
            string deleteShopPartnerQuery = "DELETE FROM dbo.ShopPartner;";
            string deletePartnerQuery = "DELETE FROM dbo.Partner;";

            using (SqlCommand command = new SqlCommand(deleteShopPartnerQuery, connection)) {
                // Thực thi câu lệnh xóa dữ liệu trong bảng ShopPartner
                command.ExecuteNonQuery();
                Console.WriteLine("Dữ liệu trong bảng ShopPartner đã được xóa.");
            }

            using (SqlCommand command = new SqlCommand(deletePartnerQuery, connection)) {
                // Thực thi câu lệnh xóa dữ liệu trong bảng Partner
                command.ExecuteNonQuery();
                Console.WriteLine("Dữ liệu trong bảng Partner đã được xóa.");
            }
            
        }

    }
}
