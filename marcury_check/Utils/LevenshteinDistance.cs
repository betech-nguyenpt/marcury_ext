﻿using DiffPlex;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DiffPlex.Chunkers;

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
        public static void HandleLevenshtein(RichTextBox richTxtCopyText, string txtTarget, string txtDb)
        {
            /*HighLightPositionstKeyStringDb.Clear();
            HighLightPositionstKeyStringTarget.Clear();*/
            // Separate lines
            var targetLines = txtTarget.Split(new[] { "\r\n" }, StringSplitOptions.None);
            var dbLines = txtDb.Split(new[] { "\r\n" }, StringSplitOptions.None);

            // Compare each row in txtTarget with the rows in txtDb
            //int lineGroup = 0;
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
                //lineGroup++;
            }
        }

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
    }  
}