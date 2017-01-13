﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copyinfo.Forms
{
    static class Style
    {
        public static System.Drawing.Font defaultFont = new System.Drawing.Font("Segoe UI", 10);

        public static System.Drawing.Color txtErrorColor = System.Drawing.Color.FromArgb(255, 82, 82);
        public static System.Windows.Forms.DataVisualization.Charting.SeriesChartType chartType_Multi = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        public static System.Windows.Forms.DataVisualization.Charting.SeriesChartType chartType_Duo = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        public static string DateTimeFormat = "dd/MM/yyyy";

        public static System.Drawing.Font btnFont = new System.Drawing.Font("Segoe UI", 12);
        public static System.Drawing.Size btnSize = new System.Drawing.Size(170, 34);
        public static System.Windows.Forms.FlatStyle btnFlatStyle = System.Windows.Forms.FlatStyle.Flat;
        public static System.Drawing.Font btnFont_Small = defaultFont;
        public static System.Drawing.Size btnSize_Small = new System.Drawing.Size(90, 32);

        public static System.Drawing.Font labelFont = new System.Drawing.Font("Segoe UI", 10);

        public static System.Drawing.Font listboxFont = defaultFont;

        public static System.Drawing.Font listViewFont = defaultFont;

        public static System.Drawing.Font textBoxFont = defaultFont;

        public static System.Drawing.Color comboBoxBackColor = System.Drawing.Color.Green;
    }
}