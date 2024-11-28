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
            this.SuspendLayout();
            // 
            // richTxtCopyText
            // 
            this.richTxtCopyText.Location = new System.Drawing.Point(-2, 2);
            this.richTxtCopyText.Name = "richTxtCopyText";
            this.richTxtCopyText.Size = new System.Drawing.Size(802, 255);
            this.richTxtCopyText.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            this.richTxtCopyText.TabIndex = 0;
            this.richTxtCopyText.Text = "";
            this.richTxtCopyText.TextChanged += new System.EventHandler(this.richTxtCopyText_TextChanged);
            // 
            // frmLoadRichTb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 260);
            this.Controls.Add(this.richTxtCopyText);
            this.Name = "frmLoadRichTb";
            this.Text = "frmLoadRichTb";
            this.Load += new System.EventHandler(this.frmLoadRichTb_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTxtCopyText;
    }
}