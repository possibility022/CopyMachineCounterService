namespace Copyinfo.Forms.Controls
{
    partial class AddClient
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
            this.tbNote = new System.Windows.Forms.TextBox();
            this.cDeviceList1 = new Copyinfo.Forms.Controls.CDeviceList();
            this.btnEditAddress = new Copyinfo.Forms.Controls.Buttons.TBButton_Small();
            this.lCity = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.lStreet = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tbLabel10 = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tbLabel9 = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.btnDelete = new Copyinfo.Forms.Controls.Buttons.TBButton_Small();
            this.btnAdd = new Copyinfo.Forms.Controls.Buttons.TBButton_Small();
            this.tbCbPhones = new Copyinfo.Forms.Controls.Combobox.TBCombobox();
            this.tbCbFax = new Copyinfo.Forms.Controls.Combobox.TBCombobox();
            this.tbCbMails = new Copyinfo.Forms.Controls.Combobox.TBCombobox();
            this.tbCbSites = new Copyinfo.Forms.Controls.Combobox.TBCombobox();
            this.lNIP = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.lName = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tbLabel8 = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tbLabel7 = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tbLabel6 = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tbLabel5 = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tbLabel4 = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tbLabel3 = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tbLabel2 = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tbLabel1 = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tbNote
            // 
            this.tbNote.Location = new System.Drawing.Point(7, 60);
            this.tbNote.Multiline = true;
            this.tbNote.Name = "tbNote";
            this.tbNote.Size = new System.Drawing.Size(303, 94);
            this.tbNote.TabIndex = 9;
            // 
            // cDeviceList1
            // 
            this.cDeviceList1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cDeviceList1.Location = new System.Drawing.Point(0, 200);
            this.cDeviceList1.Name = "cDeviceList1";
            this.cDeviceList1.Size = new System.Drawing.Size(694, 197);
            this.cDeviceList1.TabIndex = 30;
            // 
            // btnEditAddress
            // 
            this.btnEditAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnEditAddress.Location = new System.Drawing.Point(414, 160);
            this.btnEditAddress.Name = "btnEditAddress";
            this.btnEditAddress.Size = new System.Drawing.Size(224, 32);
            this.btnEditAddress.TabIndex = 29;
            this.btnEditAddress.Text = "Edytuj Adress";
            this.btnEditAddress.UseVisualStyleBackColor = true;
            this.btnEditAddress.Click += new System.EventHandler(this.btnEditAddress_Click);
            // 
            // lCity
            // 
            this.lCity.AutoSize = true;
            this.lCity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lCity.Location = new System.Drawing.Point(410, 133);
            this.lCity.Name = "lCity";
            this.lCity.Size = new System.Drawing.Size(70, 19);
            this.lCity.TabIndex = 27;
            this.lCity.Text = "tbLabel11";
            // 
            // lStreet
            // 
            this.lStreet.AutoSize = true;
            this.lStreet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lStreet.Location = new System.Drawing.Point(410, 114);
            this.lStreet.Name = "lStreet";
            this.lStreet.Size = new System.Drawing.Size(70, 19);
            this.lStreet.TabIndex = 26;
            this.lStreet.Text = "tbLabel11";
            // 
            // tbLabel10
            // 
            this.tbLabel10.AutoSize = true;
            this.tbLabel10.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbLabel10.Location = new System.Drawing.Point(316, 133);
            this.tbLabel10.Name = "tbLabel10";
            this.tbLabel10.Size = new System.Drawing.Size(85, 19);
            this.tbLabel10.TabIndex = 25;
            this.tbLabel10.Text = "Miejscowość";
            // 
            // tbLabel9
            // 
            this.tbLabel9.AutoSize = true;
            this.tbLabel9.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbLabel9.Location = new System.Drawing.Point(316, 114);
            this.tbLabel9.Name = "tbLabel9";
            this.tbLabel9.Size = new System.Drawing.Size(38, 19);
            this.tbLabel9.TabIndex = 24;
            this.tbLabel9.Text = "Ulica";
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDelete.Location = new System.Drawing.Point(181, 160);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 32);
            this.btnDelete.TabIndex = 22;
            this.btnDelete.Text = "Usuń";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAdd.Location = new System.Drawing.Point(85, 160);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 32);
            this.btnAdd.TabIndex = 21;
            this.btnAdd.Text = "Dodaj";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.tbButton_Small1_Click);
            // 
            // tbCbPhones
            // 
            this.tbCbPhones.FormattingEnabled = true;
            this.tbCbPhones.Location = new System.Drawing.Point(414, 86);
            this.tbCbPhones.Name = "tbCbPhones";
            this.tbCbPhones.Size = new System.Drawing.Size(224, 21);
            this.tbCbPhones.TabIndex = 19;
            // 
            // tbCbFax
            // 
            this.tbCbFax.FormattingEnabled = true;
            this.tbCbFax.Location = new System.Drawing.Point(414, 60);
            this.tbCbFax.Name = "tbCbFax";
            this.tbCbFax.Size = new System.Drawing.Size(224, 21);
            this.tbCbFax.TabIndex = 18;
            // 
            // tbCbMails
            // 
            this.tbCbMails.FormattingEnabled = true;
            this.tbCbMails.Location = new System.Drawing.Point(414, 34);
            this.tbCbMails.Name = "tbCbMails";
            this.tbCbMails.Size = new System.Drawing.Size(224, 21);
            this.tbCbMails.TabIndex = 17;
            // 
            // tbCbSites
            // 
            this.tbCbSites.FormattingEnabled = true;
            this.tbCbSites.Location = new System.Drawing.Point(414, 8);
            this.tbCbSites.Name = "tbCbSites";
            this.tbCbSites.Size = new System.Drawing.Size(224, 21);
            this.tbCbSites.TabIndex = 16;
            // 
            // lNIP
            // 
            this.lNIP.AutoSize = true;
            this.lNIP.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lNIP.Location = new System.Drawing.Point(75, 19);
            this.lNIP.Name = "lNIP";
            this.lNIP.Size = new System.Drawing.Size(62, 19);
            this.lNIP.TabIndex = 15;
            this.lNIP.Text = "tbLabel9";
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lName.Location = new System.Drawing.Point(75, 0);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(62, 19);
            this.lName.TabIndex = 14;
            this.lName.Text = "tbLabel9";
            // 
            // tbLabel8
            // 
            this.tbLabel8.AutoSize = true;
            this.tbLabel8.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbLabel8.Location = new System.Drawing.Point(3, 168);
            this.tbLabel8.Name = "tbLabel8";
            this.tbLabel8.Size = new System.Drawing.Size(76, 19);
            this.tbLabel8.TabIndex = 7;
            this.tbLabel8.Text = "Urządzenia";
            // 
            // tbLabel7
            // 
            this.tbLabel7.AutoSize = true;
            this.tbLabel7.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbLabel7.Location = new System.Drawing.Point(3, 38);
            this.tbLabel7.Name = "tbLabel7";
            this.tbLabel7.Size = new System.Drawing.Size(58, 19);
            this.tbLabel7.TabIndex = 6;
            this.tbLabel7.Text = "Notatka";
            // 
            // tbLabel6
            // 
            this.tbLabel6.AutoSize = true;
            this.tbLabel6.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbLabel6.Location = new System.Drawing.Point(316, 8);
            this.tbLabel6.Name = "tbLabel6";
            this.tbLabel6.Size = new System.Drawing.Size(92, 19);
            this.tbLabel6.TabIndex = 5;
            this.tbLabel6.Text = "Strony WWW";
            // 
            // tbLabel5
            // 
            this.tbLabel5.AutoSize = true;
            this.tbLabel5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbLabel5.Location = new System.Drawing.Point(316, 34);
            this.tbLabel5.Name = "tbLabel5";
            this.tbLabel5.Size = new System.Drawing.Size(42, 19);
            this.tbLabel5.TabIndex = 4;
            this.tbLabel5.Text = "Maile";
            // 
            // tbLabel4
            // 
            this.tbLabel4.AutoSize = true;
            this.tbLabel4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbLabel4.Location = new System.Drawing.Point(316, 60);
            this.tbLabel4.Name = "tbLabel4";
            this.tbLabel4.Size = new System.Drawing.Size(43, 19);
            this.tbLabel4.TabIndex = 3;
            this.tbLabel4.Text = "Faksy";
            // 
            // tbLabel3
            // 
            this.tbLabel3.AutoSize = true;
            this.tbLabel3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbLabel3.Location = new System.Drawing.Point(316, 86);
            this.tbLabel3.Name = "tbLabel3";
            this.tbLabel3.Size = new System.Drawing.Size(59, 19);
            this.tbLabel3.TabIndex = 2;
            this.tbLabel3.Text = "Telefony";
            // 
            // tbLabel2
            // 
            this.tbLabel2.AutoSize = true;
            this.tbLabel2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbLabel2.Location = new System.Drawing.Point(3, 19);
            this.tbLabel2.Name = "tbLabel2";
            this.tbLabel2.Size = new System.Drawing.Size(31, 19);
            this.tbLabel2.TabIndex = 1;
            this.tbLabel2.Text = "NIP";
            // 
            // tbLabel1
            // 
            this.tbLabel1.AutoSize = true;
            this.tbLabel1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbLabel1.Location = new System.Drawing.Point(3, 0);
            this.tbLabel1.Name = "tbLabel1";
            this.tbLabel1.Size = new System.Drawing.Size(49, 19);
            this.tbLabel1.TabIndex = 0;
            this.tbLabel1.Text = "Nazwa";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(194, 40);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(116, 17);
            this.checkBox1.TabIndex = 31;
            this.checkBox1.Text = "Umowa Serwisowa";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // AddClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cDeviceList1);
            this.Controls.Add(this.btnEditAddress);
            this.Controls.Add(this.lCity);
            this.Controls.Add(this.lStreet);
            this.Controls.Add(this.tbLabel10);
            this.Controls.Add(this.tbLabel9);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.tbCbPhones);
            this.Controls.Add(this.tbCbFax);
            this.Controls.Add(this.tbCbMails);
            this.Controls.Add(this.tbCbSites);
            this.Controls.Add(this.lNIP);
            this.Controls.Add(this.lName);
            this.Controls.Add(this.tbNote);
            this.Controls.Add(this.tbLabel8);
            this.Controls.Add(this.tbLabel7);
            this.Controls.Add(this.tbLabel6);
            this.Controls.Add(this.tbLabel5);
            this.Controls.Add(this.tbLabel4);
            this.Controls.Add(this.tbLabel3);
            this.Controls.Add(this.tbLabel2);
            this.Controls.Add(this.tbLabel1);
            this.Name = "AddClient";
            this.Size = new System.Drawing.Size(694, 397);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Labels.TBLabel tbLabel1;
        private Labels.TBLabel tbLabel2;
        private Labels.TBLabel tbLabel3;
        private Labels.TBLabel tbLabel4;
        private Labels.TBLabel tbLabel5;
        private Labels.TBLabel tbLabel6;
        private Labels.TBLabel tbLabel7;
        private Labels.TBLabel tbLabel8;
        private System.Windows.Forms.TextBox tbNote;
        private Labels.TBLabel lName;
        private Labels.TBLabel lNIP;
        private Combobox.TBCombobox tbCbSites;
        private Combobox.TBCombobox tbCbMails;
        private Combobox.TBCombobox tbCbFax;
        private Combobox.TBCombobox tbCbPhones;
        private Buttons.TBButton_Small btnAdd;
        private Buttons.TBButton_Small btnDelete;
        private Labels.TBLabel tbLabel9;
        private Labels.TBLabel tbLabel10;
        private Labels.TBLabel lStreet;
        private Labels.TBLabel lCity;
        private Buttons.TBButton_Small btnEditAddress;
        private CDeviceList cDeviceList1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
