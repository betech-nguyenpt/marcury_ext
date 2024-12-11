using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace marcury_ext.Utils
{
    public partial class FormShop : Form
    {
        public static DataGridViewCellEventArgs cellIndexTarget;
        public static DataGridView dgvFormMain;
        public static FrmLoadRichTb frmTransparent; 

        public FormShop(DataGridView dgv, FrmLoadRichTb frm, DataGridViewCellEventArgs cell)
        {
            InitializeComponent();
            dgvFormMain = dgv;
            frmTransparent = frm;
            cellIndexTarget = cell;         
        }

        /// <summary>
        /// SetRichTextBoxText
        /// </summary>
        /// <param name="text"></param>
        public void SetRichTextBoxText(string text)
        {
            rtbShop1.Text = text;
            // Make sure RichTextBox is not transparent
            rtbShop1.ReadOnly = true;
        }

        /// <summary>
        /// When the search button is pressed (BtnStartSearch)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnApply_Click(object sender, EventArgs e)
        {
            //DataGridViewCell cell = dgvFormMain.Rows[cellIndexTarget.RowIndex].Cells["dgvCol9"];
            DataGridViewCell cell = dgvFormMain.Rows[cellIndexTarget.RowIndex].Cells["dgvCol16"];
            string oldText = cell.Value.ToString();
            // Thêm hoặc sửa nội dung của ô
            cell.Value = rtbShop1.Text; // Thay thế "New Text" bằng nội dung bạn muốn thêm

            // Change text in RichTextBox from indexStart to indexEnd
            frmTransparent.UpdateTextOfLineRichTextBox(rtbShop1.Text, oldText);
            FormBeExtract.setStatus(FormBeExtract.APPLYED_STATUS);
        }

        private void FormShop_Load(object sender, EventArgs e)
        {

        }
    }
}
