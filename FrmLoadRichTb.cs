using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace marcury_ext
{
    public partial class FrmLoadRichTb : Form
    {
        private IntPtr handleTarget;
        public RichTextBox richTextBox;
        public FrmLoadRichTb(IntPtr targetHandle)
        {
            InitializeComponent();
            handleTarget = targetHandle;
        }

        private void frmLoadRichTb_Load(object sender, EventArgs e)
        {
            // Get position and size from handleTarget
            RECT rect;
            if (GetWindowRect(handleTarget, out rect)) {
                // Form configuration
                this.FormBorderStyle = FormBorderStyle.None;
                this.StartPosition = FormStartPosition.Manual;
                this.Top = rect.Top;
                this.Left = rect.Left;
                this.Width = rect.Right - rect.Left;
                this.Height = rect.Bottom - rect.Top;
            } else {
                MessageBox.Show("Cannot get TextBox position information from handle.");
                this.Close(); // Close the form
            }

            richTextBox = richTxtCopyText;
        }

        // RECT structure to get window information
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        // P/Invoke function to get window size
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        private void richTxtCopyText_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// SetRichTextBoxText
        /// </summary>
        /// <param name="text"></param>
        public void SetRichTextBoxText(string text)
        {
            richTxtCopyText.Text = text;
            // Make sure RichTextBox is not transparent
            richTxtCopyText.BackColor = Color.LightBlue;
            richTxtCopyText.ReadOnly = true;
        }

        /// <summary>
        /// UpdateTextOfLineRichTextBox
        /// </summary>
        /// <param name="newText"></param>
        /// <param name="oldText"></param>
        public void UpdateTextOfLineRichTextBox(string newText, string oldText)
        {
            if (FormExtract.getStatus() == FormExtract.IN_UPDATING_STATUS) {
                string textCurrent = richTxtCopyText.Text;
                // Check if textb exists in texta
                if (textCurrent.Contains(oldText)) {
                    // Replace textb with textupdate
                    richTxtCopyText.Text = textCurrent.Replace(oldText, newText);
                } else {
                    Console.WriteLine("The text to replace was not found.");
                }
            }           
        }

        /// <summary>
        /// GetDataRichTextBox
        /// </summary>
        /// <returns></returns>
        public string GetDataRichTextBox()
        {
            return richTxtCopyText.Text;
        }
    }
}
