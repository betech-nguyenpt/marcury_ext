namespace marcury_wpf.Forms
{
    partial class OverlayForm
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
            this.SuspendLayout();

            // OverlayForm settings
            this.BackColor = Color.FromArgb(200, 200, 200); // Xám nhạt
            this.Opacity = 0.15;                            // 15% độ trong suốt
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;

            // Auto-scaling settings
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            // Form name and text
            this.Name = "OverlayForm";
            this.Text = "OverlayForm";

            this.ResumeLayout(false);
        }


        #endregion
    }
}