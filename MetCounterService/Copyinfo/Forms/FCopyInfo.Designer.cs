namespace Copyinfo.Forms
{
    partial class FCopyInfo
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
            this.tbButtonDropMenu1 = new Copyinfo.Forms.Controls.Buttons.TBButtonDropMenu();
            this.tbLabel1 = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tbButton4 = new Copyinfo.Forms.Controls.Buttons.TBButton();
            this.tbButton3 = new Copyinfo.Forms.Controls.Buttons.TBButton();
            this.tbButton2 = new Copyinfo.Forms.Controls.Buttons.TBButton();
            this.tbBtnRefresh = new Copyinfo.Forms.Controls.Buttons.TBButton();
            this.cReports1 = new Copyinfo.Forms.Controls.CReports();
            this.SuspendLayout();
            // 
            // tbButtonDropMenu1
            // 
            this.tbButtonDropMenu1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbButtonDropMenu1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbButtonDropMenu1.Location = new System.Drawing.Point(901, 11);
            this.tbButtonDropMenu1.Name = "tbButtonDropMenu1";
            this.tbButtonDropMenu1.Size = new System.Drawing.Size(90, 32);
            this.tbButtonDropMenu1.TabIndex = 10;
            this.tbButtonDropMenu1.Text = "Drukuj";
            this.tbButtonDropMenu1.UseVisualStyleBackColor = true;
            // 
            // tbLabel1
            // 
            this.tbLabel1.AutoSize = true;
            this.tbLabel1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbLabel1.Location = new System.Drawing.Point(12, 9);
            this.tbLabel1.Name = "tbLabel1";
            this.tbLabel1.Size = new System.Drawing.Size(87, 30);
            this.tbLabel1.TabIndex = 9;
            this.tbLabel1.Text = "Liczniki";
            // 
            // tbButton4
            // 
            this.tbButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbButton4.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tbButton4.Location = new System.Drawing.Point(725, 9);
            this.tbButton4.Name = "tbButton4";
            this.tbButton4.Size = new System.Drawing.Size(170, 34);
            this.tbButton4.TabIndex = 7;
            this.tbButton4.Text = "tbButton4";
            this.tbButton4.UseVisualStyleBackColor = true;
            this.tbButton4.Click += new System.EventHandler(this.tbButton4_Click);
            // 
            // tbButton3
            // 
            this.tbButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbButton3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tbButton3.Location = new System.Drawing.Point(462, 9);
            this.tbButton3.Name = "tbButton3";
            this.tbButton3.Size = new System.Drawing.Size(170, 34);
            this.tbButton3.TabIndex = 6;
            this.tbButton3.Text = "Klienci";
            this.tbButton3.UseVisualStyleBackColor = true;
            this.tbButton3.Click += new System.EventHandler(this.tbButton3_Click);
            // 
            // tbButton2
            // 
            this.tbButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbButton2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tbButton2.Location = new System.Drawing.Point(286, 9);
            this.tbButton2.Name = "tbButton2";
            this.tbButton2.Size = new System.Drawing.Size(170, 34);
            this.tbButton2.TabIndex = 5;
            this.tbButton2.Text = "Urządzenia";
            this.tbButton2.UseVisualStyleBackColor = true;
            this.tbButton2.Click += new System.EventHandler(this.btnDevices_Click);
            // 
            // tbBtnRefresh
            // 
            this.tbBtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbBtnRefresh.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tbBtnRefresh.Location = new System.Drawing.Point(110, 9);
            this.tbBtnRefresh.Name = "tbBtnRefresh";
            this.tbBtnRefresh.Size = new System.Drawing.Size(170, 34);
            this.tbBtnRefresh.TabIndex = 4;
            this.tbBtnRefresh.Text = "Odśwież";
            this.tbBtnRefresh.UseVisualStyleBackColor = true;
            this.tbBtnRefresh.Click += new System.EventHandler(this.button1_Click);
            // 
            // cReports1
            // 
            this.cReports1.BackColor = System.Drawing.Color.Transparent;
            this.cReports1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cReports1.Location = new System.Drawing.Point(0, 52);
            this.cReports1.Name = "cReports1";
            this.cReports1.Size = new System.Drawing.Size(1061, 577);
            this.cReports1.TabIndex = 0;
            // 
            // FCopyInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1061, 629);
            this.Controls.Add(this.tbButtonDropMenu1);
            this.Controls.Add(this.tbLabel1);
            this.Controls.Add(this.tbButton4);
            this.Controls.Add(this.tbButton3);
            this.Controls.Add(this.tbButton2);
            this.Controls.Add(this.tbBtnRefresh);
            this.Controls.Add(this.cReports1);
            this.Name = "FCopyInfo";
            this.Text = "Copy Info";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FCopyInfo_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CReports cReports1;
        private Controls.Buttons.TBButton tbBtnRefresh;
        private Controls.Buttons.TBButton tbButton2;
        private Controls.Buttons.TBButton tbButton3;
        private Controls.Buttons.TBButton tbButton4;
        private Controls.Labels.TBLabel tbLabel1;
        private Controls.Buttons.TBButtonDropMenu tbButtonDropMenu1;
    }
}