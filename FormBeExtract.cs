﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace marcury_ext
{
    public partial class FormBeExtract : Form
    {
        public FormBeExtract()
        {
            InitializeComponent();
        }

        private void FormBeExtract_Load(object sender, EventArgs e)
        {
            CreateColumnDataGridView();

        }
    }
}
