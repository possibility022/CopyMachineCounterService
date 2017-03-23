namespace Copyinfo.Forms.Controls
{
    public partial class CClientList
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
            this.fastObjectListView1 = new BrightIdeasSoftware.FastObjectListView();
            this.tbTextBox1 = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.olvNIP = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvAddress = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvNote = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvPhones = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // fastObjectListView1
            // 
            this.fastObjectListView1.AllColumns.Add(this.olvNIP);
            this.fastObjectListView1.AllColumns.Add(this.olvName);
            this.fastObjectListView1.AllColumns.Add(this.olvAddress);
            this.fastObjectListView1.AllColumns.Add(this.olvNote);
            this.fastObjectListView1.AllColumns.Add(this.olvPhones);
            this.fastObjectListView1.CellEditUseWholeCell = false;
            this.fastObjectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvNIP,
            this.olvName,
            this.olvAddress,
            this.olvNote,
            this.olvPhones});
            this.fastObjectListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastObjectListView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fastObjectListView1.Location = new System.Drawing.Point(0, 31);
            this.fastObjectListView1.Name = "fastObjectListView1";
            this.fastObjectListView1.ShowGroups = false;
            this.fastObjectListView1.Size = new System.Drawing.Size(1039, 344);
            this.fastObjectListView1.TabIndex = 0;
            this.fastObjectListView1.UseCompatibleStateImageBehavior = false;
            this.fastObjectListView1.View = System.Windows.Forms.View.Details;
            this.fastObjectListView1.VirtualMode = true;
            // 
            // tbTextBox1
            // 
            this.tbTextBox1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTextBox1.id = 0;
            this.tbTextBox1.Location = new System.Drawing.Point(0, 0);
            this.tbTextBox1.Name = "tbTextBox1";
            this.tbTextBox1.Size = new System.Drawing.Size(749, 25);
            this.tbTextBox1.TabIndex = 1;
            // 
            // olvNIP
            // 
            this.olvNIP.AspectName = "NIP";
            this.olvNIP.Text = "NIP";
            this.olvNIP.Width = 87;
            // 
            // olvName
            // 
            this.olvName.AspectName = "name";
            this.olvName.Text = "Nazwa";
            this.olvName.Width = 159;
            // 
            // olvAddress
            // 
            this.olvAddress.AspectName = "address";
            this.olvAddress.Text = "Adres";
            this.olvAddress.Width = 221;
            // 
            // olvNote
            // 
            this.olvNote.AspectName = "notes";
            this.olvNote.Text = "Notka";
            this.olvNote.Width = 192;
            // 
            // olvPhones
            // 
            this.olvPhones.AspectName = "p_numbers";
            this.olvPhones.Text = "Telefony";
            this.olvPhones.Width = 131;
            // 
            // CClientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbTextBox1);
            this.Controls.Add(this.fastObjectListView1);
            this.Name = "CClientList";
            this.Size = new System.Drawing.Size(1039, 375);
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBoxes.TBTextBox tbTextBox1;
        private BrightIdeasSoftware.OLVColumn olvNIP;
        private BrightIdeasSoftware.OLVColumn olvName;
        private BrightIdeasSoftware.OLVColumn olvAddress;
        private BrightIdeasSoftware.OLVColumn olvNote;
        private BrightIdeasSoftware.OLVColumn olvPhones;
        public BrightIdeasSoftware.FastObjectListView fastObjectListView1;
    }
}
