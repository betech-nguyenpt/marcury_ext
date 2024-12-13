using System.Drawing;
using System.Windows.Forms;
using System;

namespace marcury_ext
{
    partial class FormMainExtract
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbDataGridView = new System.Windows.Forms.GroupBox();
            this.dgvExtract = new System.Windows.Forms.DataGridView();
            this.BtnStartSearch = new System.Windows.Forms.Button();
            this.BtnFinalConfirm = new System.Windows.Forms.Button();
            this.gbDataGridView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtract)).BeginInit();
            this.SuspendLayout();
            // 
            // gbDataGridView
            // 
            this.gbDataGridView.Controls.Add(this.BtnFinalConfirm);
            this.gbDataGridView.Controls.Add(this.BtnStartSearch);
            this.gbDataGridView.Controls.Add(this.dgvExtract);
            this.gbDataGridView.Location = new System.Drawing.Point(12, 3);
            this.gbDataGridView.Name = "gbDataGridView";
            this.gbDataGridView.Size = new System.Drawing.Size(1228, 420);
            this.gbDataGridView.TabIndex = 0;
            this.gbDataGridView.TabStop = false;
            this.gbDataGridView.Text = "設定　ヘルプ";
            // 
            // dgvExtract
            // 
            this.dgvExtract.AllowUserToAddRows = false;
            this.dgvExtract.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvExtract.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvExtract.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvExtract.Location = new System.Drawing.Point(6, 37);
            this.dgvExtract.Name = "dgvExtract";
            this.dgvExtract.Size = new System.Drawing.Size(1216, 351);
            this.dgvExtract.TabIndex = 0;
            this.dgvExtract.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExtract_CellContentClick);
            this.dgvExtract.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvExtract_CellPainting);
            // 
            // BtnStartSearch
            // 
            this.BtnStartSearch.Location = new System.Drawing.Point(889, 7);
            this.BtnStartSearch.Name = "BtnStartSearch";
            this.BtnStartSearch.Size = new System.Drawing.Size(185, 24);
            this.BtnStartSearch.TabIndex = 1;
            this.BtnStartSearch.Text = "クリップボードのテキストで検索";
            this.BtnStartSearch.UseVisualStyleBackColor = true;
            // 
            // BtnFinalConfirm
            // 
            this.BtnFinalConfirm.Location = new System.Drawing.Point(1106, 6);
            this.BtnFinalConfirm.Name = "BtnFinalConfirm";
            this.BtnFinalConfirm.Size = new System.Drawing.Size(116, 25);
            this.BtnFinalConfirm.TabIndex = 2;
            this.BtnFinalConfirm.Text = "最終確認";
            this.BtnFinalConfirm.UseVisualStyleBackColor = true;
            // 
            // FormMainExtract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 435);
            this.Controls.Add(this.gbDataGridView);
            this.Name = "FormMainExtract";
            this.Text = "FrmMainExtract";
            this.Load += new System.EventHandler(this.FormBeExtract_Load);
            this.gbDataGridView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtract)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private void CreateColumnDataGridView()
        {
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "dgvCol1";
            imageColumn.HeaderText = "dgvCol1";
            dgvExtract.Columns.Add(imageColumn);
            dgvExtract.Columns.Add("dgvCol2", "dgvCol2");
            dgvExtract.Columns.Add("dgvCol3", "dgvCol3");

            imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "dgvCol4";
            imageColumn.HeaderText = "dgvCol4";
            dgvExtract.Columns.Add(imageColumn);
        
            dgvExtract.Columns.Add("dgvCol5", "dgvCol5");
            dgvExtract.Columns.Add("dgvCol6", "dgvCol6");

            imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "dgvCol7";
            imageColumn.HeaderText = "dgvCol7";
            dgvExtract.Columns.Add(imageColumn);
      
            dgvExtract.Columns.Add("dgvCol8", "dgvCol8");
            dgvExtract.Columns.Add("dgvCol9", "dgvCol9");

            dgvExtract.Columns.Add("dgvCol10", "dgvCol10");
            dgvExtract.Columns.Add("dgvCol11", "dgvCol11");

            imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "dgvCol12";
            imageColumn.HeaderText = "dgvCol12";
            dgvExtract.Columns.Add(imageColumn);

            imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "dgvCol13";
            imageColumn.HeaderText = "dgvCol13";
            dgvExtract.Columns.Add(imageColumn);

            dgvExtract.Columns.Add("dgvCol14", "dgvCol14");
            dgvExtract.Columns.Add("dgvCol15", "dgvCol15");
            dgvExtract.Columns.Add("dgvCol16", "dgvCol16");
            dgvExtract.Columns.Add("dgvCol17", "dgvCol17");


            // Setup color for headers
            dgvExtract.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(124, 170, 66); ;
            dgvExtract.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;  // Chữ màu trắng
            dgvExtract.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold); // Font chữ in đậm
            dgvExtract.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Căn giữa
            dgvExtract.EnableHeadersVisualStyles = false; // Tắt visual styles mặc định
            // Set column width
            dgvExtract.Columns["dgvCol1"].Width = 80;
            dgvExtract.Columns["dgvCol2"].Width = 350;
            dgvExtract.Columns["dgvCol3"].Width = 200;
            dgvExtract.Columns["dgvCol4"].Width = 80;
            dgvExtract.Columns["dgvCol5"].Width = 350;
            dgvExtract.Columns["dgvCol6"].Width = 200;
            dgvExtract.Columns["dgvCol7"].Width = 350;
            dgvExtract.Columns["dgvCol8"].Width = 200;
            dgvExtract.Columns["dgvCol9"].Width = 200;
            dgvExtract.Columns["dgvCol10"].Width = 100;

            dgvExtract.Columns["dgvCol11"].Width = 100;
            dgvExtract.Columns["dgvCol12"].Width = 200;
            dgvExtract.Columns["dgvCol13"].Width = 100;
            dgvExtract.Columns["dgvCol14"].Width = 100;
            dgvExtract.Columns["dgvCol15"].Width = 100;
            dgvExtract.Columns["dgvCol16"].Width = 350;
            dgvExtract.Columns["dgvCol17"].Width = 200;


        }

        private void CreateColumnDataGridViewOld()
        {
            // Add columns to DataGridView
            dgvExtract.Columns.Add("dgvCol1", "原文");

            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "dgvCol2";
            imageColumn.HeaderText = "マスタ登録";
            dgvExtract.Columns.Add(imageColumn);
            //dgvExtract.Columns.Add("dgvCol2", "マスタ登録");
            imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "dgvCol3";
            imageColumn.HeaderText = "標準特記";
            dgvExtract.Columns.Add(imageColumn);
            //dgvExtract.Columns.Add("dgvCol3", "標準特記");
            imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "dgvCol4";
            imageColumn.HeaderText = "その他の特別な";
            dgvExtract.Columns.Add(imageColumn);
            //dgvExtract.Columns.Add("dgvCol4", "その他の特別な");

            dgvExtract.Columns.Add("dgvCol5", "一致率");
            dgvExtract.Columns.Add("dgvCol6", "標準特記");

            imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "dgvCol7";
            imageColumn.HeaderText = "適用（反映)";
            dgvExtract.Columns.Add(imageColumn);
            //dgvExtract.Columns.Add("dgvCol7", "適用（反映）");

            dgvExtract.Columns.Add("dgvCol8", "コメント");
            dgvExtract.Columns.Add("dgvCol9", "修正後特記事項");

            imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "dgvCol10";
            imageColumn.HeaderText = "参照";
            dgvExtract.Columns.Add(imageColumn);
            //dgvExtract.Columns.Add("dgvCol10", "参照");

            // Setup color for headers
            dgvExtract.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(124, 170, 66); ;
            dgvExtract.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;  // Chữ màu trắng
            dgvExtract.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold); // Font chữ in đậm
            dgvExtract.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Căn giữa
            dgvExtract.EnableHeadersVisualStyles = false; // Tắt visual styles mặc định
            // Set column width
            dgvExtract.Columns["dgvCol1"].Width = 350;
            dgvExtract.Columns["dgvCol2"].Width = 125;
            dgvExtract.Columns["dgvCol3"].Width = 125;
            dgvExtract.Columns["dgvCol4"].Width = 125;
            dgvExtract.Columns["dgvCol5"].Width = 125;
            dgvExtract.Columns["dgvCol6"].Width = 350;
            dgvExtract.Columns["dgvCol7"].Width = 125;
            dgvExtract.Columns["dgvCol8"].Width = 200;
            dgvExtract.Columns["dgvCol9"].Width = 350;
            dgvExtract.Columns["dgvCol10"].Width = 125;

            /*dgvExtract.Columns["dgvCol8"].Width = 300;*/

            // Assign events to DataGridView
            /*dgvExtract.CellContentClick += dgvExtract_CellContentClick;
            dgvExtract.CellValueChanged += dgvExtract_CellValueChanged;*/
            // Change the height of the DataGridView
            //dgvExtract.Height = 250; // Customize the height of the DataGridView

        }

        private System.Windows.Forms.GroupBox gbDataGridView;
        private System.Windows.Forms.DataGridView dgvExtract;
        private Button BtnFinalConfirm;
        private Button BtnStartSearch;
    }
}