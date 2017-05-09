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
            this.showDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showClient = new System.Windows.Forms.ToolStripMenuItem();
            this.emailMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hTMLLicznikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hTMLNumerSeryjnyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.porównajWybraneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuńToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fastObjectListView1 = new BrightIdeasSoftware.FastObjectListView();
            this.olvDateTime = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvClientName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvAddress = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvDeviceModel = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvSerialNumber = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvBandW = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColor = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvScan = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvTonerLevel_K = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvTonerLevel_C = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvTonerLevel_M = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvTonerLevel_Y = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tbTextBox1 = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.wyszukajNaMapieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDeviceToolStripMenuItem,
            this.showClient,
            this.emailMessageToolStripMenuItem,
            this.hTMLLicznikToolStripMenuItem,
            this.hTMLNumerSeryjnyToolStripMenuItem,
            this.porównajWybraneToolStripMenuItem,
            this.wyszukajNaMapieToolStripMenuItem,
            this.usuńToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(189, 202);
            // 
            // showDeviceToolStripMenuItem
            // 
            this.showDeviceToolStripMenuItem.Name = "showDeviceToolStripMenuItem";
            this.showDeviceToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.showDeviceToolStripMenuItem.Text = "Pokaż urządzenie";
            this.showDeviceToolStripMenuItem.Click += new System.EventHandler(this.showDeviceToolStripMenuItem_Click);
            // 
            // showClient
            // 
            this.showClient.Name = "showClient";
            this.showClient.Size = new System.Drawing.Size(188, 22);
            this.showClient.Text = "Wyświetl Klienta";
            this.showClient.Click += new System.EventHandler(this.showDevice_Click);
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
            this.hTMLLicznikToolStripMenuItem.Click += new System.EventHandler(this.HtmlLicznikToolStripMenuItem_Click);
            // 
            // hTMLNumerSeryjnyToolStripMenuItem
            // 
            this.hTMLNumerSeryjnyToolStripMenuItem.Name = "hTMLNumerSeryjnyToolStripMenuItem";
            this.hTMLNumerSeryjnyToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.hTMLNumerSeryjnyToolStripMenuItem.Text = "HTML Numer Seryjny";
            this.hTMLNumerSeryjnyToolStripMenuItem.Click += new System.EventHandler(this.HtmlNumerSeryjnyToolStripMenuItem_Click);
            // 
            // porównajWybraneToolStripMenuItem
            // 
            this.porównajWybraneToolStripMenuItem.Enabled = false;
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
            // fastObjectListView1
            // 
            this.fastObjectListView1.AllColumns.Add(this.olvDateTime);
            this.fastObjectListView1.AllColumns.Add(this.olvClientName);
            this.fastObjectListView1.AllColumns.Add(this.olvAddress);
            this.fastObjectListView1.AllColumns.Add(this.olvDeviceModel);
            this.fastObjectListView1.AllColumns.Add(this.olvSerialNumber);
            this.fastObjectListView1.AllColumns.Add(this.olvBandW);
            this.fastObjectListView1.AllColumns.Add(this.olvColor);
            this.fastObjectListView1.AllColumns.Add(this.olvScan);
            this.fastObjectListView1.AllColumns.Add(this.olvTonerLevel_K);
            this.fastObjectListView1.AllColumns.Add(this.olvTonerLevel_C);
            this.fastObjectListView1.AllColumns.Add(this.olvTonerLevel_M);
            this.fastObjectListView1.AllColumns.Add(this.olvTonerLevel_Y);
            this.fastObjectListView1.CellEditUseWholeCell = false;
            this.fastObjectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvDateTime,
            this.olvClientName,
            this.olvAddress,
            this.olvDeviceModel,
            this.olvSerialNumber,
            this.olvBandW,
            this.olvColor,
            this.olvScan,
            this.olvTonerLevel_K,
            this.olvTonerLevel_C,
            this.olvTonerLevel_M,
            this.olvTonerLevel_Y});
            this.fastObjectListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastObjectListView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fastObjectListView1.Location = new System.Drawing.Point(0, 31);
            this.fastObjectListView1.Name = "fastObjectListView1";
            this.fastObjectListView1.ShowGroups = false;
            this.fastObjectListView1.Size = new System.Drawing.Size(1003, 400);
            this.fastObjectListView1.TabIndex = 1;
            this.fastObjectListView1.UseCompatibleStateImageBehavior = false;
            this.fastObjectListView1.View = System.Windows.Forms.View.Details;
            this.fastObjectListView1.VirtualMode = true;
            // 
            // olvDateTime
            // 
            this.olvDateTime.AspectName = "datetime";
            this.olvDateTime.DisplayIndex = 4;
            this.olvDateTime.Text = "Data odczytu";
            this.olvDateTime.Width = 121;
            // 
            // olvClientName
            // 
            this.olvClientName.AspectName = "clientName";
            this.olvClientName.DisplayIndex = 0;
            this.olvClientName.Text = "Klient";
            this.olvClientName.Width = 134;
            // 
            // olvAddress
            // 
            this.olvAddress.AspectName = "deviceAddress";
            this.olvAddress.DisplayIndex = 1;
            this.olvAddress.Text = "Adres";
            this.olvAddress.Width = 150;
            // 
            // olvDeviceModel
            // 
            this.olvDeviceModel.AspectName = "modelName";
            this.olvDeviceModel.DisplayIndex = 2;
            this.olvDeviceModel.Text = "Model";
            this.olvDeviceModel.Width = 66;
            // 
            // olvSerialNumber
            // 
            this.olvSerialNumber.AspectName = "serial_number";
            this.olvSerialNumber.DisplayIndex = 3;
            this.olvSerialNumber.Text = "Numer Seryjny";
            this.olvSerialNumber.Width = 109;
            // 
            // olvBandW
            // 
            this.olvBandW.AspectName = "print_counter_black_and_white";
            this.olvBandW.Text = "B&W";
            this.olvBandW.Width = 91;
            // 
            // olvColor
            // 
            this.olvColor.AspectName = "print_counter_color";
            this.olvColor.Text = "Kolor";
            this.olvColor.Width = 93;
            // 
            // olvScan
            // 
            this.olvScan.AspectName = "scan_counter";
            this.olvScan.Text = "Skany";
            this.olvScan.Width = 88;
            // 
            // olvTonerLevel_K
            // 
            this.olvTonerLevel_K.AspectName = "tonerlevel_k";
            this.olvTonerLevel_K.Text = "Czarny";
            this.olvTonerLevel_K.Width = 67;
            // 
            // olvTonerLevel_C
            // 
            this.olvTonerLevel_C.AspectName = "tonerlevel_c";
            this.olvTonerLevel_C.Text = "Cyjan";
            this.olvTonerLevel_C.Width = 56;
            // 
            // olvTonerLevel_M
            // 
            this.olvTonerLevel_M.AspectName = "tonerlevel_m";
            this.olvTonerLevel_M.Text = "Magenta";
            this.olvTonerLevel_M.Width = 61;
            // 
            // olvTonerLevel_Y
            // 
            this.olvTonerLevel_Y.AspectName = "tonerlevel_y";
            this.olvTonerLevel_Y.Text = "Żółty";
            // 
            // tbTextBox1
            // 
            this.tbTextBox1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTextBox1.id = 0;
            this.tbTextBox1.Location = new System.Drawing.Point(0, 0);
            this.tbTextBox1.Name = "tbTextBox1";
            this.tbTextBox1.Size = new System.Drawing.Size(473, 25);
            this.tbTextBox1.TabIndex = 2;
            // 
            // wyszukajNaMapieToolStripMenuItem
            // 
            this.wyszukajNaMapieToolStripMenuItem.Name = "wyszukajNaMapieToolStripMenuItem";
            this.wyszukajNaMapieToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.wyszukajNaMapieToolStripMenuItem.Text = "Wyszukaj na mapie";
            this.wyszukajNaMapieToolStripMenuItem.Click += new System.EventHandler(this.SearchInGoogleMaps);
            // 
            // CReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbTextBox1);
            this.Controls.Add(this.fastObjectListView1);
            this.Name = "CReports";
            this.Size = new System.Drawing.Size(1003, 431);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hTMLLicznikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hTMLNumerSeryjnyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem porównajWybraneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuńToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emailMessageToolStripMenuItem;
        public BrightIdeasSoftware.FastObjectListView fastObjectListView1;
        private TextBoxes.TBTextBox tbTextBox1;
        private BrightIdeasSoftware.OLVColumn olvSerialNumber;
        private BrightIdeasSoftware.OLVColumn olvDateTime;
        private BrightIdeasSoftware.OLVColumn olvBandW;
        private BrightIdeasSoftware.OLVColumn olvColor;
        private BrightIdeasSoftware.OLVColumn olvScan;
        private BrightIdeasSoftware.OLVColumn olvTonerLevel_K;
        private BrightIdeasSoftware.OLVColumn olvTonerLevel_C;
        private BrightIdeasSoftware.OLVColumn olvTonerLevel_M;
        private BrightIdeasSoftware.OLVColumn olvTonerLevel_Y;
        private System.Windows.Forms.ToolStripMenuItem showClient;
        private BrightIdeasSoftware.OLVColumn olvClientName;
        private BrightIdeasSoftware.OLVColumn olvDeviceModel;
        private BrightIdeasSoftware.OLVColumn olvAddress;
        private System.Windows.Forms.ToolStripMenuItem showDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wyszukajNaMapieToolStripMenuItem;
    }
}
