namespace Copyinfo.Forms
{
    partial class FAddDevice
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
            this.cAddDevice1 = new Copyinfo.Forms.Controls.CAddDevice();
            this.SuspendLayout();
            // 
            // cAddDevice1
            // 
            this.cAddDevice1.Location = new System.Drawing.Point(12, 12);
            this.cAddDevice1.Name = "cAddDevice1";
            this.cAddDevice1.Size = new System.Drawing.Size(521, 272);
            this.cAddDevice1.TabIndex = 0;
            // 
            // FAddDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 364);
            this.Controls.Add(this.cAddDevice1);
            this.Name = "FAddDevice";
            this.Text = "Dodaj urządzenie";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CAddDevice cAddDevice1;
    }
}