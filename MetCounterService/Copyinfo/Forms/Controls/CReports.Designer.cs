namespace Copyinfo.Forms.Controls
{ 
    partial class CReports
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.col_serialnumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_printbw = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_printcolor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_scan = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_datetime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_serialnumber,
            this.col_printbw,
            this.col_printcolor,
            this.col_scan,
            this.col_datetime});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(823, 335);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // col_serialnumber
            // 
            this.col_serialnumber.Text = "Numer Serii";
            this.col_serialnumber.Width = 98;
            // 
            // col_printbw
            // 
            this.col_printbw.Text = "Czarno Biały";
            this.col_printbw.Width = 110;
            // 
            // col_printcolor
            // 
            this.col_printcolor.Text = "Kolorowy";
            this.col_printcolor.Width = 91;
            // 
            // col_scan
            // 
            this.col_scan.Text = "Skany";
            this.col_scan.Width = 79;
            // 
            // col_datetime
            // 
            this.col_datetime.Text = "Odczyt";
            this.col_datetime.Width = 76;
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView1);
            this.Name = "Reports";
            this.Size = new System.Drawing.Size(823, 335);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader col_serialnumber;
        private System.Windows.Forms.ColumnHeader col_printbw;
        private System.Windows.Forms.ColumnHeader col_printcolor;
        private System.Windows.Forms.ColumnHeader col_scan;
        private System.Windows.Forms.ColumnHeader col_datetime;
    }
}
