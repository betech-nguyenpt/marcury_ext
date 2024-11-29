using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marcury_ext.Utils
{
    internal class SizeControlslUI
    {
        // Save default sizes for buttons
        public static Size DefaultButtonSize => new Size(75, 25);
        public static Size LargeButtonSize => new Size(100, 40);
        public static Size SmallButtonSize => new Size(40, 15);

        /// <summary>
        /// Get Button Size
        /// </summary>
        /// <param name="sizeType"></param>
        /// <returns></returns>
        public static Size GetButtonSize(string sizeType)
        {
            // Version C# current is 7.3 -> can not use
            /*return sizeType switch {
                "Large" => LargeButtonSize,
                "Small" => SmallButtonSize,
                _ => DefaultButtonSize,
            };*/

            Size resultSize;

            switch (sizeType) {
                case "Large":
                    resultSize = LargeButtonSize;
                    break;
                case "Small":
                    resultSize = SmallButtonSize;
                    break;
                default:
                    resultSize = DefaultButtonSize;
                    break;
            }

            return resultSize;
        }

        // Save default sizes for RichTextBox
        public static Size DefauRichTBSize => new Size(120, 40);
        public static Size LargeRichTBSize => new Size(200, 60);
        public static Size SmallRichTBSize => new Size(80, 30);

        /// <summary>
        /// Get RichTextBox Size
        /// </summary>
        /// <param name="sizeType"></param>
        /// <returns></returns>
        public static Size GetRichTBSize(string sizeType)
        {
            Size resultSize;

            switch (sizeType) {
                case "Large":
                    resultSize = LargeButtonSize;
                    break;
                case "Small":
                    resultSize = SmallButtonSize;
                    break;
                default:
                    resultSize = DefaultButtonSize;
                    break;
            }

            return resultSize;
        }
    }
}
