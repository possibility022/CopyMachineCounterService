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
            this.tbTextBox1 = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.fastObjectListView1 = new BrightIdeasSoftware.FastObjectListView();
            this.olvSerialNumber = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvProvider = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvModel = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvAddress = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvInstallationDateTime = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvServiceAgreement = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbTextBox1
            // 
            this.tbTextBox1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTextBox1.id = 0;
            this.tbTextBox1.Location = new System.Drawing.Point(0, 0);
            this.tbTextBox1.Name = "tbTextBox1";
            this.tbTextBox1.Size = new System.Drawing.Size(567, 25);
            this.tbTextBox1.TabIndex = 1;
            // 
            // fastObjectListView1
            // 
            this.fastObjectListView1.AllColumns.Add(this.olvSerialNumber);
            this.fastObjectListView1.AllColumns.Add(this.olvProvider);
            this.fastObjectListView1.AllColumns.Add(this.olvModel);
            this.fastObjectListView1.AllColumns.Add(this.olvAddress);
            this.fastObjectListView1.AllColumns.Add(this.olvInstallationDateTime);
            this.fastObjectListView1.AllColumns.Add(this.olvServiceAgreement);
            this.fastObjectListView1.CellEditUseWholeCell = false;
            this.fastObjectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvSerialNumber,
            this.olvProvider,
            this.olvModel,
            this.olvAddress,
            this.olvInstallationDateTime,
            this.olvServiceAgreement});
            this.fastObjectListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastObjectListView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fastObjectListView1.Location = new System.Drawing.Point(0, 31);
            this.fastObjectListView1.Name = "fastObjectListView1";
            this.fastObjectListView1.ShowGroups = false;
            this.fastObjectListView1.Size = new System.Drawing.Size(758, 169);
            this.fastObjectListView1.TabIndex = 0;
            this.fastObjectListView1.UseCompatibleStateImageBehavior = false;
            this.fastObjectListView1.View = System.Windows.Forms.View.Details;
            this.fastObjectListView1.VirtualMode = true;
            // 
            // olvSerialNumber
            // 
            this.olvSerialNumber.AspectName = "serial_number";
            this.olvSerialNumber.Text = "Numer Seryjny";
            this.olvSerialNumber.Width = 121;
            // 
            // olvProvider
            // 
            this.olvProvider.AspectName = "provider";
            this.olvProvider.Text = "Producent";
            this.olvProvider.Width = 91;
            // 
            // olvModel
            // 
            this.olvModel.AspectName = "model";
            this.olvModel.Text = "Model";
            this.olvModel.Width = 105;
            // 
            // olvAddress
            // 
            this.olvAddress.AspectName = "address";
            this.olvAddress.Text = "Adres";
            this.olvAddress.Width = 178;
            // 
            // olvInstallationDateTime
            // 
            this.olvInstallationDateTime.AspectName = "instalation_datetime";
            this.olvInstallationDateTime.Text = "Data instalacji";
            this.olvInstallationDateTime.Width = 99;
            // 
            // olvServiceAgreement
            // 
            this.olvServiceAgreement.AspectName = "service_agreement";
            this.olvServiceAgreement.CheckBoxes = true;
            this.olvServiceAgreement.Text = "Umowa Serwisowa";
            this.olvServiceAgreement.TriStateCheckBoxes = true;
            this.olvServiceAgreement.Width = 110;
            // 
            // CDeviceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbTextBox1);
            this.Controls.Add(this.fastObjectListView1);
            this.Name = "CDeviceList";
            this.Size = new System.Drawing.Size(758, 200);
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBoxes.TBTextBox tbTextBox1;
        private BrightIdeasSoftware.OLVColumn olvSerialNumber;
        private BrightIdeasSoftware.OLVColumn olvProvider;
        private BrightIdeasSoftware.OLVColumn olvModel;
        private BrightIdeasSoftware.OLVColumn olvAddress;
        private BrightIdeasSoftware.OLVColumn olvInstallationDateTime;
        private BrightIdeasSoftware.OLVColumn olvServiceAgreement;
        public BrightIdeasSoftware.FastObjectListView fastObjectListView1;
    }
}
