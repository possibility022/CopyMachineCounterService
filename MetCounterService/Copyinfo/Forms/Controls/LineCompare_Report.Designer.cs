namespace Copyinfo.Forms.Controls
{
    partial class LineCompare_Report
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
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtScan = new System.Windows.Forms.TextBox();
            this.txtDiffrence = new System.Windows.Forms.TextBox();
            this.txtDiffrenceCounter = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(3, 3);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(100, 20);
            this.txtTotal.TabIndex = 0;
            // 
            // txtScan
            // 
            this.txtScan.Location = new System.Drawing.Point(109, 3);
            this.txtScan.Name = "txtScan";
            this.txtScan.Size = new System.Drawing.Size(100, 20);
            this.txtScan.TabIndex = 1;
            // 
            // txtDiffrence
            // 
            this.txtDiffrence.Location = new System.Drawing.Point(215, 3);
            this.txtDiffrence.Name = "txtDiffrence";
            this.txtDiffrence.Size = new System.Drawing.Size(100, 20);
            this.txtDiffrence.TabIndex = 2;
            // 
            // txtDiffrenceCounter
            // 
            this.txtDiffrenceCounter.Location = new System.Drawing.Point(321, 3);
            this.txtDiffrenceCounter.Name = "txtDiffrenceCounter";
            this.txtDiffrenceCounter.Size = new System.Drawing.Size(100, 20);
            this.txtDiffrenceCounter.TabIndex = 3;
            // 
            // LineCompare_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtDiffrenceCounter);
            this.Controls.Add(this.txtDiffrence);
            this.Controls.Add(this.txtScan);
            this.Controls.Add(this.txtTotal);
            this.Name = "LineCompare_Report";
            this.Size = new System.Drawing.Size(426, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtScan;
        private System.Windows.Forms.TextBox txtDiffrence;
        private System.Windows.Forms.TextBox txtDiffrenceCounter;
    }
}
