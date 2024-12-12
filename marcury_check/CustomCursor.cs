using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace marcury_ext
{
    public class CustomCursor : IDisposable
    {
        private Cursor originalCursor;
        public bool IsSearching { get; set; }

        public CustomCursor()
        {
            originalCursor = Cursors.Default; // Save original pointer
        }

        public void UpdateCursor()
        {
            Debug.WriteLine($"IsSearching {IsSearching}");  // Write the IsSearching value to Output
            if (this.IsSearching) {
                // Set cursor to red plus sign when searching
                Cursor.Current = CreateYellowCircleCursor();
            } else {
                // Return to original cursor
                Cursor.Current = originalCursor;
            }
        }

        private Cursor CreateRedCircleCursor()
        {
            // Create a circular red cursor (custom)
            Bitmap cursorBitmap = new Bitmap(32, 32);
            using (Graphics g = Graphics.FromImage(cursorBitmap)) {
                g.Clear(Color.Transparent);
                g.FillEllipse(Brushes.Red, 0, 0, 32, 32);  // Draw a red circle
            }
            return new Cursor(cursorBitmap.GetHicon());
        }

        public void Dispose()
        {
            // Return to default cursor when no longer in use
            Cursor.Current = originalCursor;
        }
        public static Cursor CreateYellowCircleCursor()
        {
            // Tạo con trỏ hình tròn màu vàng đậm
            int radius = 15;  // Đường kính con trỏ
            Bitmap bitmap = new Bitmap(radius * 2, radius * 2);

            using (Graphics g = Graphics.FromImage(bitmap)) {
                g.Clear(Color.Transparent); // Nền trong suốt
                g.FillEllipse(Brushes.Yellow, 0, 0, radius * 2, radius * 2); // Vẽ hình tròn vàng đậm
            }

            // Tạo con trỏ từ bitmap
            IntPtr hIcon = bitmap.GetHicon();
            return new Cursor(hIcon);
        }
    }
}