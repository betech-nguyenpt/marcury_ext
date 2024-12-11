using DiffPlex;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DiffPlex.Chunkers;
using System.IO;
using System.Reflection;

namespace marcury_ext.Utils
{
    internal class LevenshteinDistance
    {
        /// <summary>
        /// HandleLevenshtein
        /// </summary>
        /// <param name="dataGridViewDb"></param>
        /// <param name="richTxtCopyText"></param>
        /// <param name="txtTarget"></param>
        /// <param name="txtDb"></param>
        public static void HandleLevenshtein(DataGridView dataGridViewDb, RichTextBox richTxtCopyText, string txtTarget, string txtDb)
        {
            /*HighLightPositionstKeyStringDb.Clear();
            HighLightPositionstKeyStringTarget.Clear();*/
            // Separate lines
            var targetLines = txtTarget.Split(new[] { "\r\n" }, StringSplitOptions.None);
            var dbLines = txtDb.Split(new[] { "\r\n" }, StringSplitOptions.None);

            Color clA = Color.FromArgb(255, 240, 255, 255); // Xanh lá chuối tối xạm
            Color clB = Color.FromArgb(255, 245, 245, 220); // Màu xen kẽ với clA
            string imagePath = @"Images\selectImg.png"; // Đường dẫn tuyệt đối
            Image selectImg = Image.FromFile(imagePath);  // Đọc hình ảnh từ file
            imagePath = @"Images\unselectImg.png"; // Đường dẫn tuyệt đối
            Image unselectImg = Image.FromFile(imagePath);  // Đọc hình ảnh từ file
            imagePath = @"Images\btnRefer.png"; // Đường dẫn tuyệt đối
            Image referenceImg = Image.FromFile(imagePath);  // Đọc hình ảnh từ file
            // Compare each row in txtTarget with the rows in txtDb
            int colorOder = 0;
            foreach (var targetLine in targetLines) {
                if (targetLine.Length <= 0) continue;
                var results = new List<(string dbLine, double similarity)>();
                foreach (var dbLine in dbLines) {
                    double similarity = ComputeSimilarity(targetLine, dbLine);
                    results.Add((dbLine, similarity));
                }

                // Take the 3 lines with the highest similarity
                var topMatches = results.OrderByDescending(r => r.similarity).Take(1).ToList();
                //var topMatches = results.OrderByDescending(r => r.similarity).Take(3).ToList();
                // AddOriginalDataToRichTextBoxAndHighLight(richTxtCopyText, targetLine, topMatches[0].dbLine); // Old not use
                HighlightDifferences(richTxtCopyText, targetLine, topMatches[0].dbLine);

                // Add data to DataGridView
                //bool isFirstRowInGroup = true;
              
                foreach (var match in topMatches) {
                    int rowIndex = dataGridViewDb.Rows.Add();
                    var row = dataGridViewDb.Rows[rowIndex];

                    // Add plain text (no color) data to DataGridView
                    row.Cells["dgvCol1"].Value = selectImg;
                    row.Cells["dgvCol2"].Value = targetLine;
                    string formattedValue = ((int)Math.Round(match.similarity)).ToString() + "%";
                    row.Cells["dgvCol5"].Value = match.dbLine;
                    row.Cells["dgvCol6"].Value = formattedValue;
                    if (formattedValue.Equals("100%")) {
                        //row.Cells["dgvCol3"].Value = selectImg;
                        row.Cells["dgvCol4"].Value = selectImg;
                        row.Cells["dgvCol7"].Value = selectImg;
                    } else {
                        //row.Cells["dgvCol3"].Value = unselectImg;
                        row.Cells["dgvCol4"].Value = unselectImg;
                        row.Cells["dgvCol7"].Value = unselectImg;
                    }
                    row.Cells["dgvCol8"].Value = match.dbLine;
                    row.Cells["dgvCol9"].Value = formattedValue;
                    //row.Cells["dgvCol10"].Value = referenceImg;
                    row.Cells["dgvCol12"].Value = referenceImg;
                    row.Cells["dgvCol16"].Value = targetLine;
                    // Xen kẽ màu nền
                    if (colorOder % 2 == 1)  // Dòng lẻ
                    {
                        row.Cells["dgvCol13"].Value = selectImg; // TODO: 
                        /* // Lấy đường dẫn tài nguyên nhúng
                         var assembly = Assembly.GetExecutingAssembly();
                         var resourceName = "marcury_extract.Images.imgSelect.png"; // Tên tài nguyên nhúng

                         // Lấy hình ảnh từ tài nguyên nhúng
                         using (Stream stream = assembly.GetManifestResourceStream(resourceName)) {
                             if (stream != null) {
                                 Image iamge = Image.FromStream(stream);
                                 // Sử dụng hình ảnh img (ví dụ chèn vào DataGridView)
                                 dataGridViewDb.Rows.Add(); // Thêm dòng mới vào DataGridView
                                 dataGridViewDb.Rows[0].Cells["ImageColumn"].Value = img;  // Chèn hình ảnh vào cột
                             } else {
                                 MessageBox.Show("Hình ảnh không tìm thấy.");
                             }
                         }*/
                    } else {
                        row.DefaultCellStyle.BackColor = clB;
                       row.Cells["dgvCol13"].Value = unselectImg;
                    }
                    var cell = dataGridViewDb.Rows[rowIndex].Cells["dgvCol6"];
                    //HighlightDifferences(cell, topMatches[0].dbLine, targetLine);

                    colorOder++;

                   /* if (!isFirstRowInGroup) {
                        // Hide values ​​in "原文" column but keep data
                        row.Cells["原文"].Style.ForeColor = Color.Transparent;
                        row.Cells["原文"].Style.SelectionForeColor = Color.Transparent;

                        // If you want to hide the entire row, use the following line instead:
                        // row.Visible = false;
                    }

                    isFirstRowInGroup = false;*/
                }
            }
        }

       /* public static void HandleLevenshtein(DataGridView dataGridViewDb, RichTextBox richTxtCopyText, string txtTarget, string txtDb)
        {
            *//*HighLightPositionstKeyStringDb.Clear();
            HighLightPositionstKeyStringTarget.Clear();*//*
            // Separate lines
            var targetLines = txtTarget.Split(new[] { "\r\n" }, StringSplitOptions.None);
            var dbLines = txtDb.Split(new[] { "\r\n" }, StringSplitOptions.None);

            // Compare each row in txtTarget with the rows in txtDb
            int lineGroup = 0;
            foreach (var targetLine in targetLines) {
                if (targetLine.Length <= 0) continue;
                var results = new List<(string dbLine, double similarity)>();
                foreach (var dbLine in dbLines) {
                    double similarity = ComputeSimilarity(targetLine, dbLine);
                    results.Add((dbLine, similarity));
                }

                // Take the 3 lines with the highest similarity
                var topMatches = results.OrderByDescending(r => r.similarity).Take(3).ToList();
                // AddOriginalDataToRichTextBoxAndHighLight(richTxtCopyText, targetLine, topMatches[0].dbLine); // Old not use
                HighlightDifferences(richTxtCopyText, targetLine, topMatches[0].dbLine);

                // Add data to DataGridView
                bool isFirstRowInGroup = true;
                int colorOder = 0;
                foreach (var match in topMatches) {
                    int rowIndex = dataGridViewDb.Rows.Add();
                    var row = dataGridViewDb.Rows[rowIndex];

                    // Add plain text (no color) data to DataGridView
                    row.Cells["原文"].Value = targetLine;
                    row.Cells["一致率"].Value = $"{match.similarity:F2}%";
                    row.Cells["候補"].Value = match.dbLine;

                    colorOder++;
                    if (colorOder == 1) {
                        row.DefaultCellStyle.BackColor = Color.LightSalmon; // Light orange
                    }
                    // 2nd and 3rd lines (light purple)
                    else if (colorOder == 2) {
                        row.DefaultCellStyle.BackColor = Color.Lavender; // Light purple
                    } else if (colorOder == 3) {
                        row.DefaultCellStyle.BackColor = Color.Thistle; // Lighter purple
                    }

                    if (!isFirstRowInGroup) {
                        // Hide values ​​in "原文" column but keep data
                        row.Cells["原文"].Style.ForeColor = Color.Transparent;
                        row.Cells["原文"].Style.SelectionForeColor = Color.Transparent;

                        // If you want to hide the entire row, use the following line instead:
                        // row.Visible = false;
                    }

                    isFirstRowInGroup = false;
                }

                lineGroup++;
            }
        }*/

        /// <summary>
        /// Add original data to RichTextBoxAndHighLight
        /// </summary>
        /// <param name="richTxtCopyText"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        private static void AddOriginalDataToRichTextBoxAndHighLight(RichTextBox richTxtCopyText, string source, string target)
        {
            int minLen = Math.Min(source.Length, target.Length);

            // Add original line to RichTextBox
            int start = richTxtCopyText.Text.Length;
            richTxtCopyText.AppendText(source + Environment.NewLine);

            // Colorize only characters that are in the source but not in the target
            for (int i = 0; i < minLen; i++) {
                if (source[i] != target[i]) {
                    richTxtCopyText.Select(start + i, 1);
                    richTxtCopyText.SelectionBackColor = Color.Yellow; // Color different characters
                }
            }

            richTxtCopyText.DeselectAll();
        }


        /// <summary>
        /// Levenshtein distance calculation method
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CalculateLevenshteinDistance(string source, string target)
        {
            int m = source.Length;
            int n = target.Length;
            int[,] distance = new int[m + 1, n + 1];

            for (int i = 0; i <= m; i++) distance[i, 0] = i;
            for (int j = 0; j <= n; j++) distance[0, j] = j;

            for (int i = 1; i <= m; i++) {
                for (int j = 1; j <= n; j++) {
                    int cost = (source[i - 1] == target[j - 1]) ? 0 : 1;
                    distance[i, j] = Math.Min(Math.Min(
                        distance[i - 1, j] + 1,    // delete
                        distance[i, j - 1] + 1),   // insert
                        distance[i - 1, j - 1] + cost); // change
                }
            }
            return distance[m, n];
        }

        /// <summary>
        /// Same rate calculation method
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private static double ComputeSimilarity(string source, string target)
        {
            int maxLength = Math.Max(source.Length, target.Length);
            if (maxLength == 0) return 100.0; // If both strings are empty then they are 100% identical
            int levenshteinDistance = CalculateLevenshteinDistance(source, target);
            return (1.0 - (double)levenshteinDistance / maxLength) * 100;
        }

        /// <summary>
        /// HighlightDifferences
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="highLightText"></param>
        /// <param name="textDb"></param>
        public static void HighlightDifferences(RichTextBox richTextBox, string highLightText, string textDb)
        {
            var dmp = new Differ();
            var chunker = new CharacterChunker(); // Sử dụng CharacterChunker
            var diff = dmp.CreateDiffs(highLightText, textDb, false, false, chunker);

            int currentIndex = 0;

            foreach (var block in diff.DiffBlocks) {
                // Add unchanged paragraph
                if (block.DeleteStartA > currentIndex) {
                    string unchanged = highLightText.Substring(currentIndex, block.DeleteStartA - currentIndex);
                    richTextBox.AppendText(unchanged);
                }

                if (block.DeleteCountA > 0) {
                    string deletedPart = highLightText.Substring(block.DeleteStartA, block.DeleteCountA);

                    // Highlight and darken the background
                    richTextBox.SelectionColor = Color.Green;
                    richTextBox.SelectionBackColor = Color.Yellow;
                    richTextBox.SelectionFont = new Font(richTextBox.Font, FontStyle.Bold);

                    richTextBox.AppendText(deletedPart);

                    // Reset to default color
                    richTextBox.SelectionColor = richTextBox.ForeColor;
                    richTextBox.SelectionBackColor = richTextBox.BackColor;
                    richTextBox.SelectionFont = richTextBox.Font;
                }

                // Update current index
                currentIndex = block.DeleteStartA + block.DeleteCountA;
               /* richTextBox.AppendText(Environment.NewLine);*/
            }

            //Add the last paragraph if missing
            if (currentIndex < highLightText.Length) {
                richTextBox.AppendText(highLightText.Substring(currentIndex));
                /*richTextBox.AppendText(Environment.NewLine);*/
            }
            richTextBox.AppendText(Environment.NewLine);
        }

        public static void HighlightDifferences(object target, string textNeedHightLight, string textCompare)
        {
            var dmp = new Differ();
            var chunker = new CharacterChunker(); // Sử dụng CharacterChunker
            var diff = dmp.CreateDiffs(textNeedHightLight, textCompare, false, false, chunker);

            int currentIndex = 0;

            foreach (var block in diff.DiffBlocks) {
                // Add unchanged paragraph
                if (block.DeleteStartA > currentIndex) {
                    string unchanged = textNeedHightLight.Substring(currentIndex, block.DeleteStartA - currentIndex);
                    AppendToTarget(target, unchanged, false);
                }

                if (block.DeleteCountA > 0) {
                    string deletedPart = textNeedHightLight.Substring(block.DeleteStartA, block.DeleteCountA);

                    // Highlight and darken the background
                    AppendToTarget(target, deletedPart, true);
                }

                // Update current index
                currentIndex = block.DeleteStartA + block.DeleteCountA;
            }

            // Add the last paragraph if missing
            if (currentIndex < textNeedHightLight.Length) {
                AppendToTarget(target, textNeedHightLight.Substring(currentIndex), false);
            }
        }

        private static void AppendToTarget(object target, string text, bool highlight)
        {
            if (target is RichTextBox rtb) {
                // Nếu target là RichTextBox, thực hiện việc highlight
                if (highlight) {
                    rtb.SelectionColor = Color.Green;
                    rtb.SelectionBackColor = Color.Yellow;
                    rtb.SelectionFont = new Font(rtb.Font, FontStyle.Bold);
                }

                rtb.AppendText(text);

                if (highlight) {
                    rtb.SelectionColor = rtb.ForeColor;
                    rtb.SelectionBackColor = rtb.BackColor;
                    rtb.SelectionFont = rtb.Font;
                }
            } else if (target is DataGridViewCell cell) {
                // Nếu target là DataGridViewCell, chúng ta sẽ highlight trong cell
                var cellValue = cell.FormattedValue?.ToString();
                if (cellValue != null && cellValue.Contains(text)) {
                    // Highlight trực tiếp trong cell bằng cách vẽ lại
                    cell.Style.ForeColor = Color.Green;
                    cell.Style.BackColor = Color.Yellow;
                }
            }
        }

    }
}
