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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.emailMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hTMLLicznikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hTMLNumerSeryjnyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dodajUrządzenieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.porównajWybraneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuńToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbTBDate = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.tbTBScan = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.tbTBColor = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.tbTBBandW = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.tbTBSerialNumber = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.tbListView1 = new Copyinfo.Forms.Controls.ListView.TBListView();
            this.col_serialnumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_printbw = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_printcolor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_scan = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_datetime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emailMessageToolStripMenuItem,
            this.hTMLLicznikToolStripMenuItem,
            this.hTMLNumerSeryjnyToolStripMenuItem,
            this.dodajUrządzenieToolStripMenuItem,
            this.porównajWybraneToolStripMenuItem,
            this.usuńToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(189, 136);
            // 
            // emailMessageToolStripMenuItem
            // 
            this.emailMessageToolStripMenuItem.Name = "emailMessageToolStripMenuItem";
            this.emailMessageToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.emailMessageToolStripMenuItem.Text = "Email";
            this.emailMessageToolStripMenuItem.Click += new System.EventHandler(this.emailMessageToolStripMenuItem_Click);
            // 
            // hTMLLicznikToolStripMenuItem
            // 
            this.hTMLLicznikToolStripMenuItem.Name = "hTMLLicznikToolStripMenuItem";
            this.hTMLLicznikToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.hTMLLicznikToolStripMenuItem.Text = "HTML Licznik";
            this.hTMLLicznikToolStripMenuItem.Click += new System.EventHandler(this.hTMLLicznikToolStripMenuItem_Click);
            // 
            // hTMLNumerSeryjnyToolStripMenuItem
            // 
            this.hTMLNumerSeryjnyToolStripMenuItem.Name = "hTMLNumerSeryjnyToolStripMenuItem";
            this.hTMLNumerSeryjnyToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.hTMLNumerSeryjnyToolStripMenuItem.Text = "HTML Numer Seryjny";
            this.hTMLNumerSeryjnyToolStripMenuItem.Click += new System.EventHandler(this.hTMLNumerSeryjnyToolStripMenuItem_Click);
            // 
            // dodajUrządzenieToolStripMenuItem
            // 
            this.dodajUrządzenieToolStripMenuItem.Name = "dodajUrządzenieToolStripMenuItem";
            this.dodajUrządzenieToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.dodajUrządzenieToolStripMenuItem.Text = "Dodaj urządzenie";
            this.dodajUrządzenieToolStripMenuItem.Click += new System.EventHandler(this.dodajUrzadzenieToolStripMenuItem_Click);
            // 
            // porównajWybraneToolStripMenuItem
            // 
            this.porównajWybraneToolStripMenuItem.Name = "porównajWybraneToolStripMenuItem";
            this.porównajWybraneToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.porównajWybraneToolStripMenuItem.Text = "Porównaj wybrane";
            this.porównajWybraneToolStripMenuItem.Click += new System.EventHandler(this.porownajWybraneToolStripMenuItem_Click);
            // 
            // usuńToolStripMenuItem
            // 
            this.usuńToolStripMenuItem.Name = "usuńToolStripMenuItem";
            this.usuńToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.usuńToolStripMenuItem.Text = "Usuń";
            this.usuńToolStripMenuItem.Click += new System.EventHandler(this.usunToolStripMenuItem_Click);
            // 
            // tbTBDate
            // 
            this.tbTBDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTBDate.id = 0;
            this.tbTBDate.Location = new System.Drawing.Point(424, 0);
            this.tbTBDate.Name = "tbTBDate";
            this.tbTBDate.Size = new System.Drawing.Size(100, 25);
            this.tbTBDate.TabIndex = 6;
            this.tbTBDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbTBDate_MouseClick);
            this.tbTBDate.TextChanged += new System.EventHandler(this.tbTBDate_TextChanged);
            // 
            // tbTBScan
            // 
            this.tbTBScan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTBScan.id = 0;
            this.tbTBScan.Location = new System.Drawing.Point(318, 0);
            this.tbTBScan.Name = "tbTBScan";
            this.tbTBScan.Size = new System.Drawing.Size(100, 25);
            this.tbTBScan.TabIndex = 5;
            this.tbTBScan.TextChanged += new System.EventHandler(this.tbTBDate_TextChanged);
            // 
            // tbTBColor
            // 
            this.tbTBColor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTBColor.id = 0;
            this.tbTBColor.Location = new System.Drawing.Point(212, 0);
            this.tbTBColor.Name = "tbTBColor";
            this.tbTBColor.Size = new System.Drawing.Size(100, 25);
            this.tbTBColor.TabIndex = 4;
            this.tbTBColor.TextChanged += new System.EventHandler(this.tbTBDate_TextChanged);
            // 
            // tbTBBandW
            // 
            this.tbTBBandW.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTBBandW.id = 0;
            this.tbTBBandW.Location = new System.Drawing.Point(106, 0);
            this.tbTBBandW.Name = "tbTBBandW";
            this.tbTBBandW.Size = new System.Drawing.Size(100, 25);
            this.tbTBBandW.TabIndex = 3;
            this.tbTBBandW.TextChanged += new System.EventHandler(this.tbTBDate_TextChanged);
            // 
            // tbTBSerialNumber
            // 
            this.tbTBSerialNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTBSerialNumber.id = 0;
            this.tbTBSerialNumber.Location = new System.Drawing.Point(0, 0);
            this.tbTBSerialNumber.Name = "tbTBSerialNumber";
            this.tbTBSerialNumber.Size = new System.Drawing.Size(100, 25);
            this.tbTBSerialNumber.TabIndex = 2;
            this.tbTBSerialNumber.TextChanged += new System.EventHandler(this.tbTBDate_TextChanged);
            // 
            // tbListView1
            // 
            this.tbListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_serialnumber,
            this.col_printbw,
            this.col_printcolor,
            this.col_scan,
            this.col_datetime});
            this.tbListView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbListView1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbListView1.FullRowSelect = true;
            this.tbListView1.Location = new System.Drawing.Point(0, 31);
            this.tbListView1.Name = "tbListView1";
            this.tbListView1.Size = new System.Drawing.Size(823, 400);
            this.tbListView1.TabIndex = 1;
            this.tbListView1.UseCompatibleStateImageBehavior = false;
            this.tbListView1.View = System.Windows.Forms.View.Details;
            this.tbListView1.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.tbListView1_ColumnWidthChanged);
            // 
            // col_serialnumber
            // 
            this.col_serialnumber.Text = "Numer Serii";
            this.col_serialnumber.Width = 114;
            // 
            // col_printbw
            // 
            this.col_printbw.Text = "Licznik B&W";
            this.col_printbw.Width = 89;
            // 
            // col_printcolor
            // 
            this.col_printcolor.Text = "Licznik Kolor";
            this.col_printcolor.Width = 107;
            // 
            // col_scan
            // 
            this.col_scan.Text = "Licznik Skan";
            this.col_scan.Width = 151;
            // 
            // col_datetime
            // 
            this.col_datetime.Text = "Data odczytu";
            this.col_datetime.Width = 179;
            // 
            // CReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbTBDate);
            this.Controls.Add(this.tbTBScan);
            this.Controls.Add(this.tbTBColor);
            this.Controls.Add(this.tbTBBandW);
            this.Controls.Add(this.tbTBSerialNumber);
            this.Controls.Add(this.tbListView1);
            this.Name = "CReports";
            this.Size = new System.Drawing.Size(823, 431);
            this.Resize += new System.EventHandler(this.CReports_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hTMLLicznikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hTMLNumerSeryjnyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dodajUrządzenieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem porównajWybraneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuńToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emailMessageToolStripMenuItem;
        private ListView.TBListView tbListView1;
        private System.Windows.Forms.ColumnHeader col_serialnumber;
        private System.Windows.Forms.ColumnHeader col_printbw;
        private System.Windows.Forms.ColumnHeader col_printcolor;
        private System.Windows.Forms.ColumnHeader col_scan;
        private System.Windows.Forms.ColumnHeader col_datetime;
        private TextBoxes.TBTextBox tbTBSerialNumber;
        private TextBoxes.TBTextBox tbTBBandW;
        private TextBoxes.TBTextBox tbTBColor;
        private TextBoxes.TBTextBox tbTBScan;
        private TextBoxes.TBTextBox tbTBDate;
    }
}
