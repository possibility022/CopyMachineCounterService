namespace Copyinfo.Forms.Controls
{ 
    partial class CDeviceList
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
            this.wyświetlRaportyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbListView1 = new Copyinfo.Forms.Controls.ListView.TBListView();
            this.Provider = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Model = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Serial_number = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Address = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbTBData = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.tbTBAddress = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.tbTBSerialNumber = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.tbTBModel = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.tbTBProvider = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wyświetlRaportyToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(163, 26);
            // 
            // wyświetlRaportyToolStripMenuItem
            // 
            this.wyświetlRaportyToolStripMenuItem.Name = "wyświetlRaportyToolStripMenuItem";
            this.wyświetlRaportyToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.wyświetlRaportyToolStripMenuItem.Text = "Wyświetl raporty";
            this.wyświetlRaportyToolStripMenuItem.Click += new System.EventHandler(this.showReportsForThisDeviceToolStripMenuItem_Click);
            // 
            // tbListView1
            // 
            this.tbListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Provider,
            this.Model,
            this.Serial_number,
            this.Address,
            this.DateTime});
            this.tbListView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbListView1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbListView1.FullRowSelect = true;
            this.tbListView1.Location = new System.Drawing.Point(0, 31);
            this.tbListView1.Name = "tbListView1";
            this.tbListView1.Size = new System.Drawing.Size(690, 169);
            this.tbListView1.TabIndex = 7;
            this.tbListView1.UseCompatibleStateImageBehavior = false;
            this.tbListView1.View = System.Windows.Forms.View.Details;
            this.tbListView1.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.listView_ColumnWidthChanged);
            this.tbListView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView2_MouseClick);
            // 
            // Provider
            // 
            this.Provider.Text = "Producent";
            this.Provider.Width = 96;
            // 
            // Model
            // 
            this.Model.Text = "Model";
            this.Model.Width = 101;
            // 
            // Serial_number
            // 
            this.Serial_number.Text = "Numer Seryjny";
            this.Serial_number.Width = 133;
            // 
            // Address
            // 
            this.Address.Text = "Adres";
            this.Address.Width = 184;
            // 
            // DateTime
            // 
            this.DateTime.Text = "Data instalacji";
            this.DateTime.Width = 172;
            // 
            // tbTBData
            // 
            this.tbTBData.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTBData.id = 0;
            this.tbTBData.Location = new System.Drawing.Point(533, 0);
            this.tbTBData.Name = "tbTBData";
            this.tbTBData.Size = new System.Drawing.Size(100, 25);
            this.tbTBData.TabIndex = 6;
            this.tbTBData.TextChanged += new System.EventHandler(this.tbTBProvider_TextChanged);
            // 
            // tbTBAddress
            // 
            this.tbTBAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTBAddress.id = 0;
            this.tbTBAddress.Location = new System.Drawing.Point(374, 0);
            this.tbTBAddress.Name = "tbTBAddress";
            this.tbTBAddress.Size = new System.Drawing.Size(100, 25);
            this.tbTBAddress.TabIndex = 5;
            this.tbTBAddress.TextChanged += new System.EventHandler(this.tbTBProvider_TextChanged);
            // 
            // tbTBSerialNumber
            // 
            this.tbTBSerialNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTBSerialNumber.id = 0;
            this.tbTBSerialNumber.Location = new System.Drawing.Point(248, 0);
            this.tbTBSerialNumber.Name = "tbTBSerialNumber";
            this.tbTBSerialNumber.Size = new System.Drawing.Size(100, 25);
            this.tbTBSerialNumber.TabIndex = 4;
            this.tbTBSerialNumber.TextChanged += new System.EventHandler(this.tbTBProvider_TextChanged);
            // 
            // tbTBModel
            // 
            this.tbTBModel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTBModel.id = 0;
            this.tbTBModel.Location = new System.Drawing.Point(128, 0);
            this.tbTBModel.Name = "tbTBModel";
            this.tbTBModel.Size = new System.Drawing.Size(100, 25);
            this.tbTBModel.TabIndex = 3;
            this.tbTBModel.TextChanged += new System.EventHandler(this.tbTBProvider_TextChanged);
            // 
            // tbTBProvider
            // 
            this.tbTBProvider.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTBProvider.id = 0;
            this.tbTBProvider.Location = new System.Drawing.Point(3, 0);
            this.tbTBProvider.Name = "tbTBProvider";
            this.tbTBProvider.Size = new System.Drawing.Size(100, 25);
            this.tbTBProvider.TabIndex = 2;
            this.tbTBProvider.TextChanged += new System.EventHandler(this.tbTBProvider_TextChanged);
            // 
            // CDeviceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbListView1);
            this.Controls.Add(this.tbTBData);
            this.Controls.Add(this.tbTBAddress);
            this.Controls.Add(this.tbTBSerialNumber);
            this.Controls.Add(this.tbTBModel);
            this.Controls.Add(this.tbTBProvider);
            this.Name = "CDeviceList";
            this.Size = new System.Drawing.Size(690, 200);
            this.Resize += new System.EventHandler(this.CDeviceList_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem wyświetlRaportyToolStripMenuItem;
        private TextBoxes.TBTextBox tbTBProvider;
        private TextBoxes.TBTextBox tbTBModel;
        private TextBoxes.TBTextBox tbTBSerialNumber;
        private TextBoxes.TBTextBox tbTBAddress;
        private TextBoxes.TBTextBox tbTBData;
        private System.Windows.Forms.ColumnHeader Provider;
        private System.Windows.Forms.ColumnHeader Model;
        private System.Windows.Forms.ColumnHeader Serial_number;
        private System.Windows.Forms.ColumnHeader Address;
        private System.Windows.Forms.ColumnHeader DateTime;
        public ListView.TBListView tbListView1;
    }
}
