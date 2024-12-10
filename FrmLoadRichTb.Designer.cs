using System.Drawing;
using System.Windows.Forms;

namespace marcury_ext
{
    partial class FrmLoadRichTb
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
            this.richTxtCopyText = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTxtCopyText
            // 
            this.richTxtCopyText.BackColor = System.Drawing.Color.White;
            this.richTxtCopyText.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.richTxtCopyText.Location = new System.Drawing.Point(-2, 2);
            this.richTxtCopyText.Name = "richTxtCopyText";
            this.richTxtCopyText.Size = new System.Drawing.Size(340, 255);
            this.richTxtCopyText.TabIndex = 0;
            this.richTxtCopyText.Text = "";
            this.richTxtCopyText.TextChanged += new System.EventHandler(this.richTxtCopyText_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Location = new System.Drawing.Point(12, 263);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(313, 52);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Option";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(197, 19);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(91, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(33, 19);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(91, 27);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "MARCURYへ転記";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // FrmLoadRichTb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 324);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.richTxtCopyText);
            this.Name = "FrmLoadRichTb";
            this.Load += new System.EventHandler(this.frmLoadRichTb_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTxtCopyText;
        private GroupBox groupBox1;
        private Button btnCancel;
        private Button btnUpdate;
    }
}