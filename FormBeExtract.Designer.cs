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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbDataGridView = new System.Windows.Forms.GroupBox();
            this.dgvExtract = new System.Windows.Forms.DataGridView();
            this.gbFunction = new System.Windows.Forms.GroupBox();
            this.lbSearchKeyDown = new System.Windows.Forms.Label();
            this.tbSearchKey = new System.Windows.Forms.TextBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.BtnDone = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.txtHandle = new System.Windows.Forms.TextBox();
            this.BtnStartSearch = new System.Windows.Forms.Button();
            this.BtnTestConnDb = new System.Windows.Forms.Button();
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
            this.gbDataGridView.Text = "DataView";
            // 
            // dgvExtract
            // 
            this.dgvExtract.AllowUserToAddRows = false;
            this.dgvExtract.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvExtract.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvExtract.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvExtract.Location = new System.Drawing.Point(6, 19);
            this.dgvExtract.Name = "dgvExtract";
            this.dgvExtract.Size = new System.Drawing.Size(1216, 311);
            this.dgvExtract.TabIndex = 0;
            this.dgvExtract.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExtract_CellContentClick);
            this.dgvExtract.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvExtract_CellPainting);
            // 
            // gbFunction
            // 
            this.gbFunction.Controls.Add(this.BtnTestConnDb);
            this.gbFunction.Controls.Add(this.lbSearchKeyDown);
            this.gbFunction.Controls.Add(this.tbSearchKey);
            this.gbFunction.Controls.Add(this.labelStatus);
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
            // lbSearchKeyDown
            // 
            this.lbSearchKeyDown.AutoSize = true;
            this.lbSearchKeyDown.Location = new System.Drawing.Point(780, 65);
            this.lbSearchKeyDown.Name = "lbSearchKeyDown";
            this.lbSearchKeyDown.Size = new System.Drawing.Size(62, 13);
            this.lbSearchKeyDown.TabIndex = 22;
            this.lbSearchKeyDown.Text = "Search Key";
            // 
            // tbSearchKey
            // 
            this.tbSearchKey.Location = new System.Drawing.Point(859, 62);
            this.tbSearchKey.Name = "tbSearchKey";
            this.tbSearchKey.Size = new System.Drawing.Size(204, 20);
            this.tbSearchKey.TabIndex = 21;
            this.tbSearchKey.TextChanged += new System.EventHandler(this.tbSearchKey_TextChanged);
            this.tbSearchKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearchKey_KeyDown);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.ForeColor = System.Drawing.Color.Red;
            this.labelStatus.Location = new System.Drawing.Point(419, 63);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(63, 13);
            this.labelStatus.TabIndex = 20;
            this.labelStatus.Text = "※ ステータス";
            // 
            // BtnDone
            // 
            this.BtnDone.BackColor = System.Drawing.Color.LightGreen;
            this.BtnDone.Location = new System.Drawing.Point(303, 55);
            this.BtnDone.Name = "BtnDone";
            this.BtnDone.Size = new System.Drawing.Size(90, 28);
            this.BtnDone.TabIndex = 19;
            this.BtnDone.Text = "適用";
            this.BtnDone.UseVisualStyleBackColor = false;
            // 
            // BtnClose
            // 
            this.BtnClose.BackColor = System.Drawing.Color.LightCoral;
            this.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnClose.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BtnClose.Location = new System.Drawing.Point(1116, 60);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(90, 28);
            this.BtnClose.TabIndex = 1;
            this.BtnClose.Text = "閉じる";
            this.BtnClose.UseVisualStyleBackColor = false;
            // 
            // txtHandle
            // 
            this.txtHandle.Enabled = false;
            this.txtHandle.Location = new System.Drawing.Point(6, 60);
            this.txtHandle.Name = "txtHandle";
            this.txtHandle.Size = new System.Drawing.Size(164, 20);
            this.txtHandle.TabIndex = 16;
            // 
            // BtnStartSearch
            // 
            this.BtnStartSearch.BackColor = System.Drawing.Color.LightSkyBlue;
            this.BtnStartSearch.Location = new System.Drawing.Point(191, 54);
            this.BtnStartSearch.Name = "BtnStartSearch";
            this.BtnStartSearch.Size = new System.Drawing.Size(90, 28);
            this.BtnStartSearch.TabIndex = 14;
            this.BtnStartSearch.Text = "検索";
            this.BtnStartSearch.UseVisualStyleBackColor = false;
            this.BtnStartSearch.Click += new System.EventHandler(this.BtnStartSearch_Click);
            // 
            // BtnTestConnDb
            // 
            this.BtnTestConnDb.Location = new System.Drawing.Point(1113, 18);
            this.BtnTestConnDb.Name = "BtnTestConnDb";
            this.BtnTestConnDb.Size = new System.Drawing.Size(92, 24);
            this.BtnTestConnDb.TabIndex = 23;
            this.BtnTestConnDb.Text = "Test connectDb";
            this.BtnTestConnDb.UseVisualStyleBackColor = true;
            this.BtnTestConnDb.Click += new System.EventHandler(this.BtnTestConnDb_Click);
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
        private GroupBox gbFunction;
        private Button BtnStartSearch;
        private TextBox txtHandle;
        private Button BtnDone;
        private Button BtnClose;
        private Label labelStatus;
        private Label lbSearchKeyDown;
        private TextBox tbSearchKey;
        private Button BtnTestConnDb;
    }
}