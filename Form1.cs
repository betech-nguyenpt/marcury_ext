using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace marcury_ext
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handle click close button
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            // Quit application
            Application.Exit();
        }

        /// <summary>
        /// Handle click Get text data button
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void BtnGetTextData_Click(object sender, EventArgs e)
        {
            List<WinText> windows = new List<WinText>();
            // Find the first "FormMarcury" window
            IntPtr hWnd = FindWindow(null, "FormMarcury");
            //while (hWnd != IntPtr.Zero)
            //{
            //    // Find the control window that has the text
            //    IntPtr hEdit = FindWindowEx(hWnd, IntPtr.Zero, "edit", null);
            //    //initialize the buffer.  using a StringBuilder here
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder(255);  // or length from call with GETTEXTLENGTH

            //    //get the text from the child control
            //    int RetVal = SendMessage(hEdit, WM_GETTEXT, sb.Capacity, sb);

            //    windows.Add(new WinText() { hWnd = hWnd, Text = sb.ToString() });

            //    //find the next window
            //    hWnd = FindWindowEx(IntPtr.Zero, hWnd, "FormMarcury", null);
            //}

            //do something clever
            windows.OrderBy(x => x.Text).ToList().ForEach(y => Console.Write("{0} = {1}\n", y.hWnd, y.Text));
            if (hWnd != IntPtr.Zero)
            {
                Console.Write("\n\nFound FormMarcury window.");
            }
            Console.Write("\n\nFound {0} window(s).", windows.Count);
        }
        private struct WinText
        {
            public IntPtr hWnd;
            public string Text;
        }


        const int WM_GETTEXT = 0x0D;
        const int WM_GETTEXTLENGTH = 0x0E;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int Param, System.Text.StringBuilder text);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        internal delegate int WindowEnumProc(IntPtr hwnd, IntPtr lparam);

        [DllImport("user32.dll")]
        internal static extern bool EnumChildWindows(IntPtr hwnd, WindowEnumProc func, IntPtr lParam);

    }
}
