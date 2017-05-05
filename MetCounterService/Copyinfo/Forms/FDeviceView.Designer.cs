namespace Copyinfo.Forms
{
    partial class FDeviceView
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
            this.cDevice1 = new Copyinfo.Forms.Controls.CDevice();
            this.SuspendLayout();
            // 
            // cDevice1
            // 
            this.cDevice1.BackColor = System.Drawing.Color.White;
            this.cDevice1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cDevice1.Location = new System.Drawing.Point(0, 0);
            this.cDevice1.Name = "cDevice1";
            this.cDevice1.Size = new System.Drawing.Size(954, 599);
            this.cDevice1.TabIndex = 0;
            // 
            // FDeviceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(954, 599);
            this.Controls.Add(this.cDevice1);
            this.Name = "FDeviceView";
            this.Text = "FDeviceShow";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CDevice cDevice1;
    }
}