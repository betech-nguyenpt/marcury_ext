using System.Windows.Forms;
using System;
using marcury_ext.Utils;

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
            this.txtHandle = new System.Windows.Forms.TextBox();
            this.BtnStartSearch = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.dataGridViewDb = new System.Windows.Forms.DataGridView();
            this.BtnDone = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDb)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(6, 72);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(784, 140);
            this.txtResult.TabIndex = 0;
            // 
            // BtnClose
            // 
            this.BtnClose.BackColor = System.Drawing.Color.LightCoral;
            this.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnClose.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BtnClose.Location = new System.Drawing.Point(688, 36);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(102, 21);
            this.BtnClose.TabIndex = 1;
            this.BtnClose.Text = "閉じる";
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // txtHandle
            // 
            this.txtHandle.Location = new System.Drawing.Point(6, 37);
            this.txtHandle.Name = "txtHandle";
            this.txtHandle.Size = new System.Drawing.Size(135, 20);
            this.txtHandle.TabIndex = 13;
            // 
            // BtnStartSearch
            // 
            this.BtnStartSearch.BackColor = System.Drawing.Color.LightSkyBlue;
            this.BtnStartSearch.Location = new System.Drawing.Point(158, 36);
            this.BtnStartSearch.Name = "BtnStartSearch";
            this.BtnStartSearch.Size = new System.Drawing.Size(91, 21);
            this.BtnStartSearch.TabIndex = 14;
            this.BtnStartSearch.Text = "検索";
            this.BtnStartSearch.UseVisualStyleBackColor = false;
            this.BtnStartSearch.Click += new System.EventHandler(this.BtnStartSearch_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.ForeColor = System.Drawing.Color.Red;
            this.labelStatus.Location = new System.Drawing.Point(360, 40);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(52, 13);
            this.labelStatus.TabIndex = 15;
            this.labelStatus.Text = "ステータス";
            // 
            // dataGridViewDb
            // 
            this.dataGridViewDb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dataGridViewDb.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDb.Location = new System.Drawing.Point(6, 19);
            this.dataGridViewDb.Name = "dataGridViewDb";
            this.dataGridViewDb.Size = new System.Drawing.Size(784, 283);
            this.dataGridViewDb.TabIndex = 16;
            // 
            // BtnDone
            // 
            this.BtnDone.BackColor = System.Drawing.Color.LightGreen;
            this.BtnDone.Location = new System.Drawing.Point(255, 36);
            this.BtnDone.Name = "BtnDone";
            this.BtnDone.Size = new System.Drawing.Size(99, 21);
            this.BtnDone.TabIndex = 19;
            this.BtnDone.Text = "適用";
            this.BtnDone.UseVisualStyleBackColor = false;
            this.BtnDone.Click += new System.EventHandler(this.BtnDone_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnDone);
            this.groupBox1.Controls.Add(this.txtHandle);
            this.groupBox1.Controls.Add(this.BtnClose);
            this.groupBox1.Controls.Add(this.labelStatus);
            this.groupBox1.Controls.Add(this.txtResult);
            this.groupBox1.Controls.Add(this.BtnStartSearch);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Location = new System.Drawing.Point(9, 340);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(796, 229);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Function";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewDb);
            this.groupBox2.Location = new System.Drawing.Point(9, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(796, 308);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "特記文候補検索";
            // 
            // FormExtract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 581);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormExtract";
            this.Text = "特記サポート";
            this.Load += new System.EventHandler(this.FormExtract_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormExtract_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDb)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.TextBox txtHandle;
        private System.Windows.Forms.Button BtnStartSearch;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.DataGridView dataGridViewDb;
        private System.Windows.Forms.Button BtnDone;
        private System.Windows.Forms.GroupBox groupBox1;
        private GroupBox groupBox2;
    }
}

