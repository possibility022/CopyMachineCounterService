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
            this.tbTBNote = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.tbTBEmail = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.tbTBPhone = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.tbTBCity = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.tbTBStreet = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.tbTBNIP = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.tbTBClient = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.tbClientListView1 = new Copyinfo.Forms.Controls.ListView.TBClientListView();
            this.ClientName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ClientNip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ClientStreet = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ClientCity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ClientPhone = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ClientEmail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ClientNote = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ClientServiceAgrement = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbTBServiceAgrement = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.SuspendLayout();
            // 
            // tbTBNote
            // 
            this.tbTBNote.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTBNote.id = 0;
            this.tbTBNote.Location = new System.Drawing.Point(151, 0);
            this.tbTBNote.Name = "tbTBNote";
            this.tbTBNote.Size = new System.Drawing.Size(100, 25);
            this.tbTBNote.TabIndex = 9;
            this.tbTBNote.TextChanged += new System.EventHandler(this.tbTBClient_TextChanged);
            // 
            // tbTBEmail
            // 
            this.tbTBEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTBEmail.id = 0;
            this.tbTBEmail.Location = new System.Drawing.Point(245, 0);
            this.tbTBEmail.Name = "tbTBEmail";
            this.tbTBEmail.Size = new System.Drawing.Size(100, 25);
            this.tbTBEmail.TabIndex = 8;
            this.tbTBEmail.TextChanged += new System.EventHandler(this.tbTBClient_TextChanged);
            // 
            // tbTBPhone
            // 
            this.tbTBPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTBPhone.id = 0;
            this.tbTBPhone.Location = new System.Drawing.Point(327, 0);
            this.tbTBPhone.Name = "tbTBPhone";
            this.tbTBPhone.Size = new System.Drawing.Size(100, 25);
            this.tbTBPhone.TabIndex = 7;
            this.tbTBPhone.TextChanged += new System.EventHandler(this.tbTBClient_TextChanged);
            // 
            // tbTBCity
            // 
            this.tbTBCity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTBCity.id = 0;
            this.tbTBCity.Location = new System.Drawing.Point(151, 0);
            this.tbTBCity.Name = "tbTBCity";
            this.tbTBCity.Size = new System.Drawing.Size(100, 25);
            this.tbTBCity.TabIndex = 6;
            this.tbTBCity.TextChanged += new System.EventHandler(this.tbTBClient_TextChanged);
            // 
            // tbTBStreet
            // 
            this.tbTBStreet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTBStreet.id = 0;
            this.tbTBStreet.Location = new System.Drawing.Point(196, 0);
            this.tbTBStreet.Name = "tbTBStreet";
            this.tbTBStreet.Size = new System.Drawing.Size(100, 25);
            this.tbTBStreet.TabIndex = 5;
            this.tbTBStreet.TextChanged += new System.EventHandler(this.tbTBClient_TextChanged);
            // 
            // tbTBNIP
            // 
            this.tbTBNIP.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTBNIP.id = 0;
            this.tbTBNIP.Location = new System.Drawing.Point(291, 0);
            this.tbTBNIP.Name = "tbTBNIP";
            this.tbTBNIP.Size = new System.Drawing.Size(100, 25);
            this.tbTBNIP.TabIndex = 4;
            this.tbTBNIP.TextChanged += new System.EventHandler(this.tbTBClient_TextChanged);
            // 
            // tbTBClient
            // 
            this.tbTBClient.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTBClient.id = 0;
            this.tbTBClient.Location = new System.Drawing.Point(0, 0);
            this.tbTBClient.Name = "tbTBClient";
            this.tbTBClient.Size = new System.Drawing.Size(133, 25);
            this.tbTBClient.TabIndex = 3;
            this.tbTBClient.TextChanged += new System.EventHandler(this.tbTBClient_TextChanged);
            // 
            // tbClientListView1
            // 
            this.tbClientListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ClientName,
            this.ClientNip,
            this.ClientStreet,
            this.ClientCity,
            this.ClientPhone,
            this.ClientEmail,
            this.ClientNote,
            this.ClientServiceAgrement});
            this.tbClientListView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbClientListView1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbClientListView1.FullRowSelect = true;
            this.tbClientListView1.Location = new System.Drawing.Point(0, 31);
            this.tbClientListView1.Name = "tbClientListView1";
            this.tbClientListView1.Size = new System.Drawing.Size(1039, 344);
            this.tbClientListView1.TabIndex = 0;
            this.tbClientListView1.UseCompatibleStateImageBehavior = false;
            this.tbClientListView1.View = System.Windows.Forms.View.Details;
            this.tbClientListView1.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.tbClientListView1_ColumnWidthChanged);
            // 
            // ClientName
            // 
            this.ClientName.Text = "Klient";
            this.ClientName.Width = 132;
            // 
            // ClientNip
            // 
            this.ClientNip.Text = "NIP";
            this.ClientNip.Width = 122;
            // 
            // ClientStreet
            // 
            this.ClientStreet.Text = "Ulica";
            this.ClientStreet.Width = 136;
            // 
            // ClientCity
            // 
            this.ClientCity.Text = "City";
            this.ClientCity.Width = 116;
            // 
            // ClientPhone
            // 
            this.ClientPhone.Text = "Telefon";
            this.ClientPhone.Width = 83;
            // 
            // ClientEmail
            // 
            this.ClientEmail.Text = "Email";
            this.ClientEmail.Width = 120;
            // 
            // ClientNote
            // 
            this.ClientNote.Text = "Notatka";
            this.ClientNote.Width = 192;
            // 
            // ClientServiceAgrement
            // 
            this.ClientServiceAgrement.Text = "Umowa serwisowa";
            this.ClientServiceAgrement.Width = 123;
            // 
            // tbTBServiceAgrement
            // 
            this.tbTBServiceAgrement.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTBServiceAgrement.id = 0;
            this.tbTBServiceAgrement.Location = new System.Drawing.Point(433, 0);
            this.tbTBServiceAgrement.Name = "tbTBServiceAgrement";
            this.tbTBServiceAgrement.Size = new System.Drawing.Size(100, 25);
            this.tbTBServiceAgrement.TabIndex = 10;
            // 
            // CClientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbTBServiceAgrement);
            this.Controls.Add(this.tbTBNote);
            this.Controls.Add(this.tbTBEmail);
            this.Controls.Add(this.tbTBPhone);
            this.Controls.Add(this.tbTBCity);
            this.Controls.Add(this.tbTBStreet);
            this.Controls.Add(this.tbTBNIP);
            this.Controls.Add(this.tbTBClient);
            this.Controls.Add(this.tbClientListView1);
            this.Name = "CClientList";
            this.Size = new System.Drawing.Size(1039, 375);
            this.Resize += new System.EventHandler(this.CClientList_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ColumnHeader ClientName;
        private System.Windows.Forms.ColumnHeader ClientNip;
        private System.Windows.Forms.ColumnHeader ClientStreet;
        private System.Windows.Forms.ColumnHeader ClientCity;
        private System.Windows.Forms.ColumnHeader ClientPhone;
        private System.Windows.Forms.ColumnHeader ClientEmail;
        private System.Windows.Forms.ColumnHeader ClientNote;
        private TextBoxes.TBTextBox tbTBClient;
        private TextBoxes.TBTextBox tbTBNIP;
        private TextBoxes.TBTextBox tbTBStreet;
        private TextBoxes.TBTextBox tbTBCity;
        private TextBoxes.TBTextBox tbTBPhone;
        private TextBoxes.TBTextBox tbTBEmail;
        private TextBoxes.TBTextBox tbTBNote;
        public ListView.TBClientListView tbClientListView1;
        private System.Windows.Forms.ColumnHeader ClientServiceAgrement;
        private TextBoxes.TBTextBox tbTBServiceAgrement;
    }
}
