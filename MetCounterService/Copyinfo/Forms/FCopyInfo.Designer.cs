﻿namespace Copyinfo.Forms
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
            this.tbButton2 = new Copyinfo.Forms.Controls.Buttons.TBButton();
            this.tbButton1 = new Copyinfo.Forms.Controls.Buttons.TBButton();
            this.cReports1 = new Copyinfo.Forms.Controls.CReports();
            this.tbButton3 = new Copyinfo.Forms.Controls.Buttons.TBButton();
            this.tbButton4 = new Copyinfo.Forms.Controls.Buttons.TBButton();
            this.SuspendLayout();
            // 
            // tbButton2
            // 
            this.tbButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbButton2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tbButton2.Location = new System.Drawing.Point(188, 11);
            this.tbButton2.Name = "tbButton2";
            this.tbButton2.Size = new System.Drawing.Size(170, 34);
            this.tbButton2.TabIndex = 5;
            this.tbButton2.Text = "Urządzenia";
            this.tbButton2.UseVisualStyleBackColor = true;
            this.tbButton2.Click += new System.EventHandler(this.btnDevices_Click);
            // 
            // tbButton1
            // 
            this.tbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbButton1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tbButton1.Location = new System.Drawing.Point(12, 12);
            this.tbButton1.Name = "tbButton1";
            this.tbButton1.Size = new System.Drawing.Size(170, 34);
            this.tbButton1.TabIndex = 4;
            this.tbButton1.Text = "Pobierz";
            this.tbButton1.UseVisualStyleBackColor = true;
            this.tbButton1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cReports1
            // 
            this.cReports1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cReports1.Location = new System.Drawing.Point(0, 51);
            this.cReports1.Name = "cReports1";
            this.cReports1.Size = new System.Drawing.Size(1128, 524);
            this.cReports1.TabIndex = 0;
            // 
            // tbButton3
            // 
            this.tbButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbButton3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tbButton3.Location = new System.Drawing.Point(364, 11);
            this.tbButton3.Name = "tbButton3";
            this.tbButton3.Size = new System.Drawing.Size(170, 34);
            this.tbButton3.TabIndex = 6;
            this.tbButton3.Text = "Klienci";
            this.tbButton3.UseVisualStyleBackColor = true;
            this.tbButton3.Click += new System.EventHandler(this.tbButton3_Click);
            // 
            // tbButton4
            // 
            this.tbButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbButton4.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tbButton4.Location = new System.Drawing.Point(680, 11);
            this.tbButton4.Name = "tbButton4";
            this.tbButton4.Size = new System.Drawing.Size(170, 34);
            this.tbButton4.TabIndex = 7;
            this.tbButton4.Text = "tbButton4";
            this.tbButton4.UseVisualStyleBackColor = true;
            // 
            // FCopyInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1128, 575);
            this.Controls.Add(this.tbButton4);
            this.Controls.Add(this.tbButton3);
            this.Controls.Add(this.tbButton2);
            this.Controls.Add(this.tbButton1);
            this.Controls.Add(this.cReports1);
            this.Name = "FCopyInfo";
            this.Text = "Copy Info";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CReports cReports1;
        private Controls.Buttons.TBButton tbButton1;
        private Controls.Buttons.TBButton tbButton2;
        private Controls.Buttons.TBButton tbButton3;
        private Controls.Buttons.TBButton tbButton4;
    }
}