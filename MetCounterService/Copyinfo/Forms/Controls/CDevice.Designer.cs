namespace Copyinfo.Forms.Controls
{
    partial class CDevice
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.txtInstallationDate = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.objectListView1 = new BrightIdeasSoftware.ObjectListView();
            this.olvDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvTechnican = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvDescription = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.cReports1 = new Copyinfo.Forms.Controls.CReports();
            this.tblSerialNumber = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tblModel = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tblProvider = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tblAddress = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tblNipName = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tblClientName = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tbLabel3 = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tbLabel2 = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tbLabel1 = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tbButton1 = new Copyinfo.Forms.Controls.Buttons.TBButton();
            this.cAddress1 = new Copyinfo.Forms.Controls.CAddress();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 21);
            this.label1.TabIndex = 6;
            this.label1.Text = "Producent";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(6, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 21);
            this.label2.TabIndex = 7;
            this.label2.Text = "Model";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 21);
            this.label3.TabIndex = 8;
            this.label3.Text = "Numer Seryjny";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(6, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 21);
            this.label4.TabIndex = 9;
            this.label4.Text = "Miejsce instalacji";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(488, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Data instalacji";
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.FirstDayOfWeek = System.Windows.Forms.Day.Monday;
            this.monthCalendar1.Location = new System.Drawing.Point(414, 38);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 11;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            // 
            // txtInstallationDate
            // 
            this.txtInstallationDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtInstallationDate.Location = new System.Drawing.Point(414, 212);
            this.txtInstallationDate.Name = "txtInstallationDate";
            this.txtInstallationDate.Size = new System.Drawing.Size(269, 26);
            this.txtInstallationDate.TabIndex = 12;
            this.txtInstallationDate.Visible = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(695, 292);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(255, 302);
            this.richTextBox1.TabIndex = 26;
            this.richTextBox1.Text = "";
            // 
            // objectListView1
            // 
            this.objectListView1.AllColumns.Add(this.olvDate);
            this.objectListView1.AllColumns.Add(this.olvTechnican);
            this.objectListView1.AllColumns.Add(this.olvDescription);
            this.objectListView1.CellEditUseWholeCell = false;
            this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvDate,
            this.olvTechnican});
            this.objectListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListView1.FullRowSelect = true;
            this.objectListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.objectListView1.Location = new System.Drawing.Point(695, 38);
            this.objectListView1.Name = "objectListView1";
            this.objectListView1.ShowGroups = false;
            this.objectListView1.Size = new System.Drawing.Size(255, 248);
            this.objectListView1.TabIndex = 27;
            this.objectListView1.UseCellFormatEvents = true;
            this.objectListView1.UseCompatibleStateImageBehavior = false;
            this.objectListView1.View = System.Windows.Forms.View.Details;
            // 
            // olvDate
            // 
            this.olvDate.AspectName = "DateOfServiceClosed";
            this.olvDate.DisplayIndex = 1;
            this.olvDate.Text = "Data";
            this.olvDate.Width = 118;
            // 
            // olvTechnican
            // 
            this.olvTechnican.AspectName = "Technican";
            this.olvTechnican.DisplayIndex = 0;
            this.olvTechnican.Text = "Serwisant";
            this.olvTechnican.Width = 109;
            // 
            // olvDescription
            // 
            this.olvDescription.AspectName = "Description";
            this.olvDescription.IsVisible = false;
            this.olvDescription.Text = "Opis";
            // 
            // cReports1
            // 
            this.cReports1.Location = new System.Drawing.Point(0, 332);
            this.cReports1.Name = "cReports1";
            this.cReports1.Size = new System.Drawing.Size(683, 265);
            this.cReports1.TabIndex = 24;
            // 
            // tblSerialNumber
            // 
            this.tblSerialNumber.AutoSize = true;
            this.tblSerialNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tblSerialNumber.Location = new System.Drawing.Point(198, 51);
            this.tblSerialNumber.Name = "tblSerialNumber";
            this.tblSerialNumber.Size = new System.Drawing.Size(62, 19);
            this.tblSerialNumber.TabIndex = 23;
            this.tblSerialNumber.Text = "tbLabel6";
            // 
            // tblModel
            // 
            this.tblModel.AutoSize = true;
            this.tblModel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tblModel.Location = new System.Drawing.Point(198, 30);
            this.tblModel.Name = "tblModel";
            this.tblModel.Size = new System.Drawing.Size(62, 19);
            this.tblModel.TabIndex = 22;
            this.tblModel.Text = "tbLabel5";
            // 
            // tblProvider
            // 
            this.tblProvider.AutoSize = true;
            this.tblProvider.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tblProvider.Location = new System.Drawing.Point(198, 9);
            this.tblProvider.Name = "tblProvider";
            this.tblProvider.Size = new System.Drawing.Size(62, 19);
            this.tblProvider.TabIndex = 21;
            this.tblProvider.Text = "tbLabel4";
            // 
            // tblAddress
            // 
            this.tblAddress.AutoSize = true;
            this.tblAddress.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tblAddress.Location = new System.Drawing.Point(70, 307);
            this.tblAddress.Name = "tblAddress";
            this.tblAddress.Size = new System.Drawing.Size(70, 21);
            this.tblAddress.TabIndex = 20;
            this.tblAddress.Text = "tbLabel6";
            // 
            // tblNipName
            // 
            this.tblNipName.AutoSize = true;
            this.tblNipName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tblNipName.Location = new System.Drawing.Point(70, 286);
            this.tblNipName.Name = "tblNipName";
            this.tblNipName.Size = new System.Drawing.Size(70, 21);
            this.tblNipName.TabIndex = 19;
            this.tblNipName.Text = "tbLabel5";
            // 
            // tblClientName
            // 
            this.tblClientName.AutoSize = true;
            this.tblClientName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tblClientName.Location = new System.Drawing.Point(70, 265);
            this.tblClientName.Name = "tblClientName";
            this.tblClientName.Size = new System.Drawing.Size(76, 21);
            this.tblClientName.TabIndex = 18;
            this.tblClientName.Text = "tbLabel4";
            // 
            // tbLabel3
            // 
            this.tbLabel3.AutoSize = true;
            this.tbLabel3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbLabel3.Location = new System.Drawing.Point(6, 307);
            this.tbLabel3.Name = "tbLabel3";
            this.tbLabel3.Size = new System.Drawing.Size(50, 21);
            this.tbLabel3.TabIndex = 17;
            this.tbLabel3.Text = "Adres";
            // 
            // tbLabel2
            // 
            this.tbLabel2.AutoSize = true;
            this.tbLabel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbLabel2.Location = new System.Drawing.Point(6, 286);
            this.tbLabel2.Name = "tbLabel2";
            this.tbLabel2.Size = new System.Drawing.Size(35, 21);
            this.tbLabel2.TabIndex = 16;
            this.tbLabel2.Text = "NIP";
            // 
            // tbLabel1
            // 
            this.tbLabel1.AutoSize = true;
            this.tbLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbLabel1.Location = new System.Drawing.Point(6, 265);
            this.tbLabel1.Name = "tbLabel1";
            this.tbLabel1.Size = new System.Drawing.Size(49, 21);
            this.tbLabel1.TabIndex = 15;
            this.tbLabel1.Text = "Klient";
            // 
            // tbButton1
            // 
            this.tbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbButton1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tbButton1.Location = new System.Drawing.Point(414, 292);
            this.tbButton1.Name = "tbButton1";
            this.tbButton1.Size = new System.Drawing.Size(269, 34);
            this.tbButton1.TabIndex = 14;
            this.tbButton1.Text = "Pokaż szczegóły klienta";
            this.tbButton1.UseVisualStyleBackColor = true;
            this.tbButton1.Click += new System.EventHandler(this.tbButton1_Click);
            // 
            // cAddress1
            // 
            this.cAddress1.BackColor = System.Drawing.Color.White;
            this.cAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cAddress1.Location = new System.Drawing.Point(10, 93);
            this.cAddress1.Name = "cAddress1";
            this.cAddress1.Size = new System.Drawing.Size(319, 154);
            this.cAddress1.TabIndex = 13;
            // 
            // CDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.objectListView1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.cReports1);
            this.Controls.Add(this.tblSerialNumber);
            this.Controls.Add(this.tblModel);
            this.Controls.Add(this.tblProvider);
            this.Controls.Add(this.tblAddress);
            this.Controls.Add(this.tblNipName);
            this.Controls.Add(this.tblClientName);
            this.Controls.Add(this.tbLabel3);
            this.Controls.Add(this.tbLabel2);
            this.Controls.Add(this.tbLabel1);
            this.Controls.Add(this.tbButton1);
            this.Controls.Add(this.cAddress1);
            this.Controls.Add(this.txtInstallationDate);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CDevice";
            this.Size = new System.Drawing.Size(953, 597);
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.TextBox txtInstallationDate;
        private CAddress cAddress1;
        private Buttons.TBButton tbButton1;
        private Labels.TBLabel tbLabel1;
        private Labels.TBLabel tbLabel2;
        private Labels.TBLabel tbLabel3;
        private Labels.TBLabel tblClientName;
        private Labels.TBLabel tblNipName;
        private Labels.TBLabel tblAddress;
        private Labels.TBLabel tblProvider;
        private Labels.TBLabel tblModel;
        private Labels.TBLabel tblSerialNumber;
        private CReports cReports1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private BrightIdeasSoftware.OLVColumn olvTechnican;
        private BrightIdeasSoftware.OLVColumn olvDate;
        private BrightIdeasSoftware.OLVColumn olvDescription;
        private BrightIdeasSoftware.ObjectListView objectListView1;
    }
}
