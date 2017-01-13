namespace Copyinfo.Forms
{
    partial class FClientList
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbButton2 = new Copyinfo.Forms.Controls.Buttons.TBButton();
            this.tbButton1 = new Copyinfo.Forms.Controls.Buttons.TBButton();
            this.cClientList1 = new Copyinfo.Forms.Controls.CClientList();
            this.SuspendLayout();
            // 
            // tbButton2
            // 
            this.tbButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbButton2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tbButton2.Location = new System.Drawing.Point(188, 12);
            this.tbButton2.Name = "tbButton2";
            this.tbButton2.Size = new System.Drawing.Size(170, 34);
            this.tbButton2.TabIndex = 2;
            this.tbButton2.Text = "Dodaj Klienta";
            this.tbButton2.UseVisualStyleBackColor = true;
            this.tbButton2.Click += new System.EventHandler(this.tbButton2_Click);
            // 
            // tbButton1
            // 
            this.tbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbButton1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tbButton1.Location = new System.Drawing.Point(12, 12);
            this.tbButton1.Name = "tbButton1";
            this.tbButton1.Size = new System.Drawing.Size(170, 34);
            this.tbButton1.TabIndex = 1;
            this.tbButton1.Text = "Pobierz";
            this.tbButton1.UseVisualStyleBackColor = true;
            this.tbButton1.Click += new System.EventHandler(this.tbButton1_Click);
            // 
            // cClientList1
            // 
            this.cClientList1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cClientList1.Location = new System.Drawing.Point(0, 52);
            this.cClientList1.Name = "cClientList1";
            this.cClientList1.Size = new System.Drawing.Size(941, 291);
            this.cClientList1.TabIndex = 0;
            // 
            // FClientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 343);
            this.Controls.Add(this.tbButton2);
            this.Controls.Add(this.tbButton1);
            this.Controls.Add(this.cClientList1);
            this.Name = "FClientList";
            this.Text = "FClientList";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CClientList cClientList1;
        private Controls.Buttons.TBButton tbButton1;
        private Controls.Buttons.TBButton tbButton2;
    }
}