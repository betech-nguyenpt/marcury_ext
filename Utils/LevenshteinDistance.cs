using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace marcury_ext.Utils
{
    internal class LevenshteinDistance
    {
        /*public static Dictionary<string, List<int>> HighLightPositionstKeyStringDb = new Dictionary<string, List<int>>();
        public static Dictionary<string, List<int>> HighLightPositionstKeyStringTarget = new Dictionary<string, List<int>>();*/
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

            // Compare each row in txtTarget with the rows in txtDb
            int lineGroup = 0;
            foreach (var targetLine in targetLines) {
                var results = new List<(string dbLine, double similarity)>();
                foreach (var dbLine in dbLines) {
                    double similarity = ComputeSimilarity(targetLine, dbLine);
                    results.Add((dbLine, similarity));
                }

                // Take the 3 lines with the highest similarity
                var topMatches = results.OrderByDescending(r => r.similarity).Take(3).ToList();
/*
                if (HighLightPositionstKeyStringDb.Count != 0) {
                    if (HighLightPositionstKeyStringDb.ContainsKey(topMatches[0].dbLine)) {
                        if (!HighLightPositionstKeyStringTarget.ContainsKey(topMatches[0].dbLine)) {
                            HighLightPositionstKeyStringTarget[targetLine] = new List<int>();
                        }
                        HighLightPositionstKeyStringTarget[targetLine] = HighLightPositionstKeyStringDb[topMatches[0].dbLine];
                        HighLightPositionstKeyStringDb.Clear();
                    }
                }*/

                // Color the different characters only in the line with the highest similarity
                AddOriginalDataToRichTextBoxAndHighLight(richTxtCopyText, targetLine, topMatches[0].dbLine);
                // Color the different characters only in the line with the highest similarity
                /*if (HighLightPositionstKeyStringTarget.ContainsKey(targetLine)) {
                    // Lấy danh sách vị trí cần tô màu từ từ điển
                    List<int> highlightPositions = HighLightPositionstKeyStringTarget[targetLine];
                    AddOriginalDataToRichTextBoxAndHighLight(richTxtCopyText, targetLine, topMatches[0].dbLine, highlightPositions);
                } else {
                    AddOriginalDataToRichTextBoxAndHighLight(richTxtCopyText, targetLine, topMatches[0].dbLine, null);
                }*/


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
        }

        /// <summary>
        /// Add original data to RichTextBoxAndHighLight
        /// </summary>
        /// <param name="richTxtCopyText"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        //private static void AddOriginalDataToRichTextBoxAndHighLight(RichTextBox richTxtCopyText, string source, string target, List<int> highlightPositions)
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
            /*if (highlightPositions != null) {
                // Highlight the words based on the positions provided in highlightPositions
                foreach (int position in highlightPositions) {
                    // Ensure that the position is within the bounds of the string
                    if (position >= 0 && position < source.Length) {
                        richTxtCopyText.Select(start + position, 1); // Select the character at the given position
                        richTxtCopyText.SelectionBackColor = Color.Red; // Highlight the character (or word) in red
                    }
                }
            }*/

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
                                                        // Nếu phép biến đổi là "xóa", lưu vị trí ký tự bị xóa
                    /*if (distance[i, j] == distance[i - 1, j] + 1) {
                        if (j == n) {
                            if (!HighLightPositionstKeyStringDb.ContainsKey(target)) {
                                // Nếu khóa chưa tồn tại, khởi tạo danh sách mới
                                HighLightPositionstKeyStringDb[target] = new List<int>();
                            }
                            HighLightPositionstKeyStringDb[target].Add(i - 1); // Lưu vị trí của ký tự bị xóa trong source
                        }
                    }*/
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
    }
}
