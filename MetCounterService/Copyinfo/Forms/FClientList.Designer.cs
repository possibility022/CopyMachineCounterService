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
            this.tbButton1 = new Copyinfo.Forms.Controls.Buttons.TBButton();
            this.cClientList1 = new Copyinfo.Forms.Controls.CClientList();
            this.tbButton_Small1 = new Copyinfo.Forms.Controls.Buttons.TBButton_Small();
            this.tbLabel1 = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.SuspendLayout();
            // 
            // tbButton1
            // 
            this.tbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbButton1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tbButton1.Location = new System.Drawing.Point(97, 9);
            this.tbButton1.Name = "tbButton1";
            this.tbButton1.Size = new System.Drawing.Size(170, 34);
            this.tbButton1.TabIndex = 1;
            this.tbButton1.Text = "Odśwież";
            this.tbButton1.UseVisualStyleBackColor = true;
            this.tbButton1.Click += new System.EventHandler(this.tbButton1_Click);
            // 
            // cClientList1
            // 
            this.cClientList1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cClientList1.Location = new System.Drawing.Point(0, 49);
            this.cClientList1.Name = "cClientList1";
            this.cClientList1.Size = new System.Drawing.Size(941, 294);
            this.cClientList1.TabIndex = 0;
            // 
            // tbButton_Small1
            // 
            this.tbButton_Small1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbButton_Small1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbButton_Small1.Location = new System.Drawing.Point(768, 11);
            this.tbButton_Small1.Name = "tbButton_Small1";
            this.tbButton_Small1.Size = new System.Drawing.Size(161, 32);
            this.tbButton_Small1.TabIndex = 2;
            this.tbButton_Small1.Text = "Drukuj raporty";
            this.tbButton_Small1.UseVisualStyleBackColor = true;
            this.tbButton_Small1.Click += new System.EventHandler(this.tbButton_Small1_Click);
            // 
            // tbLabel1
            // 
            this.tbLabel1.AutoSize = true;
            this.tbLabel1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbLabel1.Location = new System.Drawing.Point(12, 9);
            this.tbLabel1.Name = "tbLabel1";
            this.tbLabel1.Size = new System.Drawing.Size(79, 30);
            this.tbLabel1.TabIndex = 10;
            this.tbLabel1.Text = "Klienci";
            // 
            // FClientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(941, 343);
            this.Controls.Add(this.tbLabel1);
            this.Controls.Add(this.tbButton_Small1);
            this.Controls.Add(this.tbButton1);
            this.Controls.Add(this.cClientList1);
            this.Name = "FClientList";
            this.Text = "FClientList";
            this.ResizeEnd += new System.EventHandler(this.FClientList_ResizeEnd);
            this.Resize += new System.EventHandler(this.FClientList_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CClientList cClientList1;
        private Controls.Buttons.TBButton tbButton1;
        private Controls.Buttons.TBButton_Small tbButton_Small1;
        private Controls.Labels.TBLabel tbLabel1;
    }
}