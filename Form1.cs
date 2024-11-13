using marcury_ext.Utils;
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
using System.Xml;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace marcury_ext
{
    public partial class FormExtract : Form
    {
        private bool isDragging = false;

        public FormExtract()
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
            var allText = GetAllTextFromWindowByTitle("FormMarcury");
            this.AppendTextToResult(allText.ToString());
            // Turn on dragging mode
            //this.isDragging = true;
            //this.AppendTextToResult("Dragging mode is ON");
        }
        // Delegate we use to call methods when enumerating child windows.
        private delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr i);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindowByCaption(IntPtr zeroOnly, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, [Out] StringBuilder lParam);

        // Callback method used to collect a list of child windows we need to capture text from.
        private static bool EnumChildWindowsCallback(IntPtr handle, IntPtr pointer)
        {
            // Creates a managed GCHandle object from the pointer representing a handle to the list created in GetChildWindows.
            var gcHandle = GCHandle.FromIntPtr(pointer);

            // Casts the handle back back to a List<IntPtr>
            var list = gcHandle.Target as List<IntPtr>;

            if (list == null)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }

            // Adds the handle to the list.
            list.Add(handle);

            return true;
        }

        // Returns an IEnumerable<IntPtr> containing the handles of all child windows of the parent window.
        private static IEnumerable<IntPtr> GetChildWindows(IntPtr parent)
        {
            // Create list to store child window handles.
            var result = new List<IntPtr>();

            // Allocate list handle to pass to EnumChildWindows.
            var listHandle = GCHandle.Alloc(result);

            try
            {
                // Enumerates though all the child windows of the parent represented by IntPtr parent, executing EnumChildWindowsCallback for each. 
                EnumChildWindows(parent, EnumChildWindowsCallback, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                // Free the list handle.
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }

            // Return the list of child window handles.
            return result;
        }

        // Gets text text from a control by it's handle.
        private static string GetText(IntPtr handle)
        {
            const uint WM_GETTEXTLENGTH = 0x000E;
            const uint WM_GETTEXT = 0x000D;

            // Gets the text length.
            var length = (int)SendMessage(handle, WM_GETTEXTLENGTH, IntPtr.Zero, null);

            // Init the string builder to hold the text.
            var sb = new StringBuilder(length + 1);

            // Writes the text from the handle into the StringBuilder
            SendMessage(handle, WM_GETTEXT, (IntPtr)sb.Capacity, sb);

            // Return the text as a string.
            return sb.ToString();
        }

        // Wraps everything together. Will accept a window title and return all text in the window that matches that window title.
        private static string GetAllTextFromWindowByTitle(string windowTitle)
        {
            var sb = new StringBuilder();

            try
            {
                // Find the main window's handle by the title.
                var windowHWnd = FindWindowByCaption(IntPtr.Zero, windowTitle);

                // Loop though the child windows, and execute the EnumChildWindowsCallback method
                var childWindows = GetChildWindows(windowHWnd);

                // For each child handle, run GetText
                foreach (var childWindowText in childWindows.Select(GetText))
                {
                    // Append the text to the string builder.
                    sb.Append(childWindowText);
                }

                // Return the windows full text.
                return sb.ToString();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

            return string.Empty;
        }

        private void FormExtract_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.isDragging)
            {
                this.isDragging = false;
                this.AppendTextToResult("Dragging mode is OFF");
            }
        }

        private void AppendTextToResult(String text)
        {
            TxtResult.AppendText(text);
            TxtResult.AppendText(Environment.NewLine);
        }

        private void FormExtract_Load(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml("DataSource\\demo.xml");
            //DGVMain.DataSource = dataSet.Tables[0];

            // Load data for ListView
            LVData.View = View.Details;
            LVData.GridLines = true;
            //LVData.Sorting = SortOrder.Ascending;
            LVData.Columns.Add("原文", 300);
            LVData.Columns.Add("一致率", 70);
            LVData.Columns.Add("候補", 300);
            LVData.Columns.Add("適用", 50);
            LVData.Items.Clear();
            var doc = XDocument.Load("DataSource\\demo.xml");
            var output = from x in doc.Root.Elements("result")
                         select new ListViewItem(new[]
                         {
                             x.Element("content").Value,
                             x.Element("matchrate").Value,
                             x.Element("suggest").Value,
                             x.Element("apply").Value,
                         });
            LVData.Items.AddRange(output.Reverse().ToArray());
            //using (XmlReader reader = XmlReader.Create("DataSource\\demo.xml"))
            //{
            //    int i = 0;
            //    while (reader.Read())
            //    {
            //        ListViewItem item = new ListViewItem();
            //        switch (reader.Name.ToString())
            //        {
            //            case "content":
            //                item.Text = reader.GetAttribute("content");
            //                break;
            //            default:
            //                break;
            //        }
            //        LVData.Items.Add(item);
            //    }
            //}
        }

        private void BtnGetStringDistance_Click(object sender, EventArgs e)
        {
            int dist = LevenshteinDistance.Calculate(TBXStr1.Text, TBXStr2.Text);
            LBLResult.Text = "Distance is " + dist;
        }
    }
}
