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
            this.txtResult = new System.Windows.Forms.TextBox();
            this.BtnClose = new System.Windows.Forms.Button();
            this.BtnGetTextData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxDB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnHighLight = new System.Windows.Forms.Button();
            this.txtHandle = new System.Windows.Forms.TextBox();
            this.BtnStartSearch = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.dataGridViewDb = new System.Windows.Forms.DataGridView();
            this.btnMark = new System.Windows.Forms.Button();
            this.BtnDone = new System.Windows.Forms.Button();
            this.RichTextBoxHighLight = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDb)).BeginInit();
            this.SuspendLayout();
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(12, 564);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(575, 113);
            this.txtResult.TabIndex = 0;
            // 
            // BtnClose
            // 
            this.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnClose.Location = new System.Drawing.Point(652, 600);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(88, 23);
            this.BtnClose.TabIndex = 1;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnGetTextData
            // 
            this.BtnGetTextData.Location = new System.Drawing.Point(652, 562);
            this.BtnGetTextData.Name = "BtnGetTextData";
            this.BtnGetTextData.Size = new System.Drawing.Size(88, 23);
            this.BtnGetTextData.TabIndex = 2;
            this.BtnGetTextData.Text = "Get text data";
            this.BtnGetTextData.UseVisualStyleBackColor = true;
            this.BtnGetTextData.Click += new System.EventHandler(this.BtnGetTextData_Click);
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
            // TextBoxDB
            // 
            this.TextBoxDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxDB.Location = new System.Drawing.Point(6, 424);
            this.TextBoxDB.Name = "TextBoxDB";
            this.TextBoxDB.Size = new System.Drawing.Size(776, 23);
            this.TextBoxDB.TabIndex = 8;
            this.TextBoxDB.Text = "・改行や文章挿入箇所はグレーのハイライトが表示されます。";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 327);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "RichTextBox HighLight";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 404);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Text DB";
            // 
            // BtnHighLight
            // 
            this.BtnHighLight.Location = new System.Drawing.Point(9, 459);
            this.BtnHighLight.Name = "BtnHighLight";
            this.BtnHighLight.Size = new System.Drawing.Size(106, 23);
            this.BtnHighLight.TabIndex = 11;
            this.BtnHighLight.Text = "HighLight Diff";
            this.BtnHighLight.UseVisualStyleBackColor = true;
            this.BtnHighLight.Click += new System.EventHandler(this.BtnHighLight_Click);
            // 
            // txtHandle
            // 
            this.txtHandle.Location = new System.Drawing.Point(9, 515);
            this.txtHandle.Name = "txtHandle";
            this.txtHandle.Size = new System.Drawing.Size(109, 20);
            this.txtHandle.TabIndex = 13;
            // 
            // BtnStartSearch
            // 
            this.BtnStartSearch.Location = new System.Drawing.Point(133, 515);
            this.BtnStartSearch.Name = "BtnStartSearch";
            this.BtnStartSearch.Size = new System.Drawing.Size(75, 21);
            this.BtnStartSearch.TabIndex = 14;
            this.BtnStartSearch.Text = "Search";
            this.BtnStartSearch.UseVisualStyleBackColor = true;
            this.BtnStartSearch.Click += new System.EventHandler(this.BtnStartSearch_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(318, 518);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(37, 13);
            this.labelStatus.TabIndex = 15;
            this.labelStatus.Text = "Status";
            // 
            // dataGridViewDb
            // 
            this.dataGridViewDb.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDb.Location = new System.Drawing.Point(9, 42);
            this.dataGridViewDb.Name = "dataGridViewDb";
            this.dataGridViewDb.Size = new System.Drawing.Size(773, 258);
            this.dataGridViewDb.TabIndex = 16;
            // 
            // btnMark
            // 
            this.btnMark.Location = new System.Drawing.Point(652, 639);
            this.btnMark.Name = "btnMark";
            this.btnMark.Size = new System.Drawing.Size(88, 21);
            this.btnMark.TabIndex = 17;
            this.btnMark.Text = "markText";
            this.btnMark.UseVisualStyleBackColor = true;
            this.btnMark.Click += new System.EventHandler(this.BtnMarkDataTextBox_Click);
            // 
            // BtnDone
            // 
            this.BtnDone.Location = new System.Drawing.Point(223, 515);
            this.BtnDone.Name = "BtnDone";
            this.BtnDone.Size = new System.Drawing.Size(70, 20);
            this.BtnDone.TabIndex = 19;
            this.BtnDone.Text = "Done";
            this.BtnDone.UseVisualStyleBackColor = true;
            this.BtnDone.Click += new System.EventHandler(this.BtnDone_Click);
            // 
            // RichTextBoxHighLight
            // 
            this.RichTextBoxHighLight.Location = new System.Drawing.Point(9, 356);
            this.RichTextBoxHighLight.Name = "RichTextBoxHighLight";
            this.RichTextBoxHighLight.Size = new System.Drawing.Size(773, 29);
            this.RichTextBoxHighLight.TabIndex = 20;
            this.RichTextBoxHighLight.Text = "・改行やテキスト挿入箇所はグレーのハイライトで";
            // 
            // FormExtract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 689);
            this.Controls.Add(this.RichTextBoxHighLight);
            this.Controls.Add(this.BtnDone);
            this.Controls.Add(this.btnMark);
            this.Controls.Add(this.dataGridViewDb);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.BtnStartSearch);
            this.Controls.Add(this.txtHandle);
            this.Controls.Add(this.BtnHighLight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextBoxDB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnGetTextData);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.txtResult);
            this.Name = "FormExtract";
            this.Text = "特記サポート";
            this.Load += new System.EventHandler(this.FormExtract_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormExtract_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Button BtnGetTextData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBoxDB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnHighLight;
        private System.Windows.Forms.TextBox txtHandle;
        private System.Windows.Forms.Button BtnStartSearch;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.DataGridView dataGridViewDb;
        private System.Windows.Forms.Button btnMark;
        private System.Windows.Forms.Button BtnDone;
        private System.Windows.Forms.RichTextBox RichTextBoxHighLight;
    }
}

