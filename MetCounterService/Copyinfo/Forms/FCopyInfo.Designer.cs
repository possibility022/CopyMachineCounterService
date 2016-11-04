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
            this.btnDownloadAll = new System.Windows.Forms.Button();
            this.cReports1 = new Copyinfo.Forms.Controls.CReports();
            this.btnDevices = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDownloadAll
            // 
            this.btnDownloadAll.BackColor = System.Drawing.Color.White;
            this.btnDownloadAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDownloadAll.Location = new System.Drawing.Point(12, 12);
            this.btnDownloadAll.Name = "btnDownloadAll";
            this.btnDownloadAll.Size = new System.Drawing.Size(98, 34);
            this.btnDownloadAll.TabIndex = 1;
            this.btnDownloadAll.Text = "Pobierz";
            this.btnDownloadAll.UseVisualStyleBackColor = false;
            this.btnDownloadAll.Click += new System.EventHandler(this.button1_Click);
            // 
            // cReports1
            // 
            this.cReports1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cReports1.Location = new System.Drawing.Point(0, 52);
            this.cReports1.Name = "cReports1";
            this.cReports1.Size = new System.Drawing.Size(1128, 523);
            this.cReports1.TabIndex = 0;
            // 
            // btnDevices
            // 
            this.btnDevices.BackColor = System.Drawing.Color.White;
            this.btnDevices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDevices.Location = new System.Drawing.Point(116, 12);
            this.btnDevices.Name = "btnDevices";
            this.btnDevices.Size = new System.Drawing.Size(170, 34);
            this.btnDevices.TabIndex = 2;
            this.btnDevices.Text = "Urządzenia";
            this.btnDevices.UseVisualStyleBackColor = false;
            this.btnDevices.Click += new System.EventHandler(this.btnDevices_Click);
            // 
            // FCopyInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1128, 575);
            this.Controls.Add(this.btnDevices);
            this.Controls.Add(this.btnDownloadAll);
            this.Controls.Add(this.cReports1);
            this.Name = "FCopyInfo";
            this.Text = "Copy Info";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CReports cReports1;
        private System.Windows.Forms.Button btnDownloadAll;
        private System.Windows.Forms.Button btnDevices;
    }
}