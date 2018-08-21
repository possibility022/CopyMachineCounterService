namespace Copyinfo.Forms.Controls.DeviceControls
{
    partial class DeviceReports
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
            this.cReports1 = new Copyinfo.Forms.Controls.CReports();
            this.SuspendLayout();
            // 
            // cReports1
            // 
            this.cReports1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cReports1.Location = new System.Drawing.Point(0, 0);
            this.cReports1.Name = "cReports1";
            this.cReports1.Size = new System.Drawing.Size(856, 524);
            this.cReports1.TabIndex = 0;
            // 
            // DeviceReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cReports1);
            this.Name = "DeviceReports";
            this.Size = new System.Drawing.Size(856, 524);
            this.Tag = "Raporty";
            this.ResumeLayout(false);

        }

        #endregion

        public CReports cReports1;
    }
}
