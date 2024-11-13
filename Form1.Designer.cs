namespace marcury_ext
{
    partial class FormExtract
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
            if (disposing && (components != null))
            {
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
            this.TxtResult = new System.Windows.Forms.TextBox();
            this.BtnClose = new System.Windows.Forms.Button();
            this.BtnGetTextData = new System.Windows.Forms.Button();
            this.DGVMain = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.Content = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatchRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Suggest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Apply = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMain)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtResult
            // 
            this.TxtResult.Location = new System.Drawing.Point(12, 325);
            this.TxtResult.Multiline = true;
            this.TxtResult.Name = "TxtResult";
            this.TxtResult.Size = new System.Drawing.Size(575, 113);
            this.TxtResult.TabIndex = 0;
            // 
            // BtnClose
            // 
            this.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnClose.Location = new System.Drawing.Point(700, 415);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(88, 23);
            this.BtnClose.TabIndex = 1;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnGetTextData
            // 
            this.BtnGetTextData.Location = new System.Drawing.Point(700, 386);
            this.BtnGetTextData.Name = "BtnGetTextData";
            this.BtnGetTextData.Size = new System.Drawing.Size(88, 23);
            this.BtnGetTextData.TabIndex = 2;
            this.BtnGetTextData.Text = "Get text data";
            this.BtnGetTextData.UseVisualStyleBackColor = true;
            this.BtnGetTextData.Click += new System.EventHandler(this.BtnGetTextData_Click);
            // 
            // DGVMain
            // 
            this.DGVMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Content,
            this.MatchRate,
            this.Suggest,
            this.Apply});
            this.DGVMain.Location = new System.Drawing.Point(15, 32);
            this.DGVMain.Name = "DGVMain";
            this.DGVMain.Size = new System.Drawing.Size(776, 287);
            this.DGVMain.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "特記文候補検索";
            // 
            // Content
            // 
            this.Content.HeaderText = "原文";
            this.Content.Name = "Content";
            this.Content.ReadOnly = true;
            this.Content.Width = 300;
            // 
            // MatchRate
            // 
            this.MatchRate.HeaderText = "一致率";
            this.MatchRate.Name = "MatchRate";
            this.MatchRate.ReadOnly = true;
            this.MatchRate.Width = 70;
            // 
            // Suggest
            // 
            this.Suggest.HeaderText = "候補";
            this.Suggest.Name = "Suggest";
            this.Suggest.ReadOnly = true;
            this.Suggest.Width = 300;
            // 
            // Apply
            // 
            this.Apply.HeaderText = "適用";
            this.Apply.Name = "Apply";
            this.Apply.ReadOnly = true;
            // 
            // FormExtract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DGVMain);
            this.Controls.Add(this.BtnGetTextData);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.TxtResult);
            this.Name = "FormExtract";
            this.Text = "特記サポート";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormExtract_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.DGVMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtResult;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Button BtnGetTextData;
        private System.Windows.Forms.DataGridView DGVMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Content;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatchRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Suggest;
        private System.Windows.Forms.DataGridViewTextBoxColumn Apply;
    }
}

