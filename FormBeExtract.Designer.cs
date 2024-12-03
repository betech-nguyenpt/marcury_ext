using System.Drawing;
using System.Windows.Forms;
using System;

namespace marcury_ext
{
    partial class FormBeExtract
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbDataGridView = new System.Windows.Forms.GroupBox();
            this.dgvExtract = new System.Windows.Forms.DataGridView();
            this.gbFunction = new System.Windows.Forms.GroupBox();
            this.BtnStartSearch = new System.Windows.Forms.Button();
            this.txtHandle = new System.Windows.Forms.TextBox();
            this.BtnDone = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.gbDataGridView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtract)).BeginInit();
            this.gbFunction.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDataGridView
            // 
            this.gbDataGridView.Controls.Add(this.dgvExtract);
            this.gbDataGridView.Location = new System.Drawing.Point(12, 12);
            this.gbDataGridView.Name = "gbDataGridView";
            this.gbDataGridView.Size = new System.Drawing.Size(1228, 346);
            this.gbDataGridView.TabIndex = 0;
            this.gbDataGridView.TabStop = false;
            this.gbDataGridView.Text = "DataGridview";
            // 
            // dgvExtract
            // 
            this.dgvExtract.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExtract.Location = new System.Drawing.Point(6, 19);
            this.dgvExtract.Name = "dgvExtract";
            this.dgvExtract.Size = new System.Drawing.Size(1216, 311);
            this.dgvExtract.TabIndex = 0;
            // 
            // gbFunction
            // 
            this.gbFunction.Controls.Add(this.BtnDone);
            this.gbFunction.Controls.Add(this.BtnClose);
            this.gbFunction.Controls.Add(this.txtHandle);
            this.gbFunction.Controls.Add(this.BtnStartSearch);
            this.gbFunction.Location = new System.Drawing.Point(12, 364);
            this.gbFunction.Name = "gbFunction";
            this.gbFunction.Size = new System.Drawing.Size(1228, 111);
            this.gbFunction.TabIndex = 1;
            this.gbFunction.TabStop = false;
            this.gbFunction.Text = "Function";
            // 
            // BtnStartSearch
            // 
            this.BtnStartSearch.Location = new System.Drawing.Point(187, 54);
            this.BtnStartSearch.Name = "BtnStartSearch";
            this.BtnStartSearch.Size = new System.Drawing.Size(75, 21);
            this.BtnStartSearch.TabIndex = 15;
            this.BtnStartSearch.Text = "Search";
            this.BtnStartSearch.UseVisualStyleBackColor = true;
            // 
            // txtHandle
            // 
            this.txtHandle.Location = new System.Drawing.Point(6, 55);
            this.txtHandle.Name = "txtHandle";
            this.txtHandle.Size = new System.Drawing.Size(164, 20);
            this.txtHandle.TabIndex = 16;
            // 
            // BtnDone
            // 
            this.BtnDone.Location = new System.Drawing.Point(302, 56);
            this.BtnDone.Name = "BtnDone";
            this.BtnDone.Size = new System.Drawing.Size(70, 20);
            this.BtnDone.TabIndex = 21;
            this.BtnDone.Text = "Done";
            this.BtnDone.UseVisualStyleBackColor = true;
            // 
            // BtnClose
            // 
            this.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnClose.Location = new System.Drawing.Point(429, 53);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(88, 23);
            this.BtnClose.TabIndex = 20;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            // 
            // FormBeExtract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 487);
            this.Controls.Add(this.gbFunction);
            this.Controls.Add(this.gbDataGridView);
            this.Name = "FormBeExtract";
            this.Text = "FrmBeExtract";
            this.Load += new System.EventHandler(this.FormBeExtract_Load);
            this.gbDataGridView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtract)).EndInit();
            this.gbFunction.ResumeLayout(false);
            this.gbFunction.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private void CreateColumnDataGridView()
        {
            // Add columns to DataGridView
            dgvExtract.Columns.Add("dgvCol1", "原文");
            dgvExtract.Columns.Add("dgvCol2", "マスタ登録");
            dgvExtract.Columns.Add("dgvCol3", "標準特記");
            dgvExtract.Columns.Add("dgvCol4", "その他の特別な");

            dgvExtract.Columns.Add("dgvCol5", "一致率");
            dgvExtract.Columns.Add("dgvCol6", "標準特記");

            dgvExtract.Columns.Add("dgvCol7", "適用（反映）");

            dgvExtract.Columns.Add("dgvCol8", "コメント");
            dgvExtract.Columns.Add("dgvCol9", "修正後特記事項");
            dgvExtract.Columns.Add("dgvCol10", "参照");



            // Setup color for headers
            dgvExtract.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(173, 219, 72); ;
            dgvExtract.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;  // Chữ màu trắng
            dgvExtract.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold); // Font chữ in đậm
            dgvExtract.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Căn giữa
            dgvExtract.EnableHeadersVisualStyles = false; // Tắt visual styles mặc định
            // Set column width
            dgvExtract.Columns["dgvCol1"].Width = 350;
            dgvExtract.Columns["dgvCol2"].Width = 150;
            dgvExtract.Columns["dgvCol3"].Width = 150;
            dgvExtract.Columns["dgvCol4"].Width = 150;
            dgvExtract.Columns["dgvCol5"].Width = 150;
            dgvExtract.Columns["dgvCol6"].Width = 350;
            dgvExtract.Columns["dgvCol7"].Width = 150;
            dgvExtract.Columns["dgvCol8"].Width = 200;
            dgvExtract.Columns["dgvCol9"].Width = 350;
            dgvExtract.Columns["dgvCol10"].Width = 150;

            /*dgvExtract.Columns["dgvCol8"].Width = 300;*/

            // Assign events to DataGridView
            /*dgvExtract.CellContentClick += dgvExtract_CellContentClick;
            dgvExtract.CellValueChanged += dgvExtract_CellValueChanged;*/
            // Change the height of the DataGridView
            //dgvExtract.Height = 250; // Customize the height of the DataGridView

        }

        private System.Windows.Forms.GroupBox gbDataGridView;
        private System.Windows.Forms.DataGridView dgvExtract;
        private GroupBox gbFunction;
        private Button BtnStartSearch;
        private TextBox txtHandle;
        private Button BtnDone;
        private Button BtnClose;
    }
}