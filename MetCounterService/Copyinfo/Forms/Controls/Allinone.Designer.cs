namespace Copyinfo.Forms.Controls
{
    partial class Allinone
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
            this.cClientList1 = new Copyinfo.Forms.Controls.CClientList();
            this.cDeviceList1 = new Copyinfo.Forms.Controls.CDeviceList();
            this.cReports1 = new Copyinfo.Forms.Controls.CReports();
            this.SuspendLayout();
            // 
            // cClientList1
            // 
            this.cClientList1.Location = new System.Drawing.Point(3, 3);
            this.cClientList1.Name = "cClientList1";
            this.cClientList1.Size = new System.Drawing.Size(1228, 224);
            this.cClientList1.TabIndex = 0;
            // 
            // cDeviceList1
            // 
            this.cDeviceList1.Location = new System.Drawing.Point(3, 233);
            this.cDeviceList1.Name = "cDeviceList1";
            this.cDeviceList1.Size = new System.Drawing.Size(1228, 200);
            this.cDeviceList1.TabIndex = 1;
            // 
            // cReports1
            // 
            this.cReports1.Location = new System.Drawing.Point(3, 439);
            this.cReports1.Name = "cReports1";
            this.cReports1.Size = new System.Drawing.Size(1228, 431);
            this.cReports1.TabIndex = 2;
            // 
            // Allinone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cReports1);
            this.Controls.Add(this.cDeviceList1);
            this.Controls.Add(this.cClientList1);
            this.Name = "Allinone";
            this.Size = new System.Drawing.Size(1234, 897);
            this.ResumeLayout(false);

        }

        #endregion

        private CClientList cClientList1;
        private CDeviceList cDeviceList1;
        private CReports cReports1;
    }
}
