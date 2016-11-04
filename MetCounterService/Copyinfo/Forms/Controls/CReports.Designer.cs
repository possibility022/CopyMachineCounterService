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
            this.listView1 = new System.Windows.Forms.ListView();
            this.col_serialnumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_printbw = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_printcolor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_scan = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_datetime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.hTMLLicznikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hTMLNumerSeryjnyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dodajUrządzenieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.porównajWybraneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuńToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emailMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
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
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(823, 335);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView1.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView1_DrawItem);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // col_serialnumber
            // 
            this.col_serialnumber.Text = "Numer Serii";
            this.col_serialnumber.Width = 122;
            // 
            // col_printbw
            // 
            this.col_printbw.Text = "Czarno Biały";
            this.col_printbw.Width = 125;
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
            this.col_datetime.Width = 149;
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(189, 158);
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
            // emailMessageToolStripMenuItem
            // 
            this.emailMessageToolStripMenuItem.Name = "emailMessageToolStripMenuItem";
            this.emailMessageToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.emailMessageToolStripMenuItem.Text = "Email";
            this.emailMessageToolStripMenuItem.Click += new System.EventHandler(this.emailMessageToolStripMenuItem_Click);
            // 
            // CReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView1);
            this.Name = "CReports";
            this.Size = new System.Drawing.Size(823, 335);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader col_serialnumber;
        private System.Windows.Forms.ColumnHeader col_printbw;
        private System.Windows.Forms.ColumnHeader col_printcolor;
        private System.Windows.Forms.ColumnHeader col_scan;
        private System.Windows.Forms.ColumnHeader col_datetime;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hTMLLicznikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hTMLNumerSeryjnyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dodajUrządzenieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem porównajWybraneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuńToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emailMessageToolStripMenuItem;
    }
}
