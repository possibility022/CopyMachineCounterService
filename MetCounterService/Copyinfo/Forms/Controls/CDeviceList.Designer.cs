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
            this.listView2 = new System.Windows.Forms.ListView();
            this.chProvider = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cdModel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cdSerialNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cdInstallationPlace = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cdDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.wyświetlRaportyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chProvider,
            this.cdModel,
            this.cdSerialNumber,
            this.cdInstallationPlace,
            this.cdDate});
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listView2.FullRowSelect = true;
            this.listView2.Location = new System.Drawing.Point(0, 0);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(690, 317);
            this.listView2.TabIndex = 1;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView2_MouseClick);
            // 
            // chProvider
            // 
            this.chProvider.Text = "Producent";
            this.chProvider.Width = 126;
            // 
            // cdModel
            // 
            this.cdModel.Text = "Model";
            this.cdModel.Width = 99;
            // 
            // cdSerialNumber
            // 
            this.cdSerialNumber.Text = "Numer Seryjny";
            this.cdSerialNumber.Width = 143;
            // 
            // cdInstallationPlace
            // 
            this.cdInstallationPlace.Text = "Adres";
            this.cdInstallationPlace.Width = 145;
            // 
            // cdDate
            // 
            this.cdDate.Text = "Data";
            this.cdDate.Width = 123;
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
            // CDeviceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView2);
            this.Name = "CDeviceList";
            this.Size = new System.Drawing.Size(690, 317);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader chProvider;
        private System.Windows.Forms.ColumnHeader cdModel;
        private System.Windows.Forms.ColumnHeader cdSerialNumber;
        private System.Windows.Forms.ColumnHeader cdInstallationPlace;
        private System.Windows.Forms.ColumnHeader cdDate;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem wyświetlRaportyToolStripMenuItem;
    }
}
