namespace Copyinfo.Forms
{
    partial class FDevicesView
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
            this.btnDownload = new System.Windows.Forms.Button();
            this.tbButton_Small1 = new Copyinfo.Forms.Controls.Buttons.TBButton_Small();
            this.cDeviceList1 = new Copyinfo.Forms.Controls.CDeviceList();
            this.tbLabel1 = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.SuspendLayout();
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.Color.White;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDownload.Location = new System.Drawing.Point(139, 8);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(170, 34);
            this.btnDownload.TabIndex = 5;
            this.btnDownload.Text = "Odśwież";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // tbButton_Small1
            // 
            this.tbButton_Small1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbButton_Small1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbButton_Small1.Location = new System.Drawing.Point(645, 14);
            this.tbButton_Small1.Name = "tbButton_Small1";
            this.tbButton_Small1.Size = new System.Drawing.Size(152, 32);
            this.tbButton_Small1.TabIndex = 6;
            this.tbButton_Small1.Text = "Drukuj jeden record";
            this.tbButton_Small1.UseVisualStyleBackColor = true;
            this.tbButton_Small1.Click += new System.EventHandler(this.tbButton_Small1_Click);
            // 
            // cDeviceList1
            // 
            this.cDeviceList1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cDeviceList1.Location = new System.Drawing.Point(0, 52);
            this.cDeviceList1.Name = "cDeviceList1";
            this.cDeviceList1.Size = new System.Drawing.Size(809, 333);
            this.cDeviceList1.TabIndex = 4;
            // 
            // tbLabel1
            // 
            this.tbLabel1.AutoSize = true;
            this.tbLabel1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbLabel1.Location = new System.Drawing.Point(12, 12);
            this.tbLabel1.Name = "tbLabel1";
            this.tbLabel1.Size = new System.Drawing.Size(121, 30);
            this.tbLabel1.TabIndex = 10;
            this.tbLabel1.Text = "Urządzenia";
            // 
            // FDevicesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(809, 385);
            this.Controls.Add(this.tbLabel1);
            this.Controls.Add(this.tbButton_Small1);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.cDeviceList1);
            this.Name = "FDevicesView";
            this.Text = "MachinesView";
            this.ResizeEnd += new System.EventHandler(this.FDevicesView_ResizeEnd);
            this.Resize += new System.EventHandler(this.FDevicesView_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Controls.CDeviceList cDeviceList1;
        private System.Windows.Forms.Button btnDownload;
        private Controls.Buttons.TBButton_Small tbButton_Small1;
        private Controls.Labels.TBLabel tbLabel1;
    }
}