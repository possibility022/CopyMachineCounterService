﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Copyinfo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Global.Initialize();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reports1.fillList(Global.database.getAllReports());
        }
    }
}
