namespace Copyinfo.Forms
{
    partial class FCompareReports
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
            this.cLineCompare_Report1 = new Copyinfo.Forms.Controls.CLineCompare_Report();
            this.SuspendLayout();
            // 
            // cLineCompare_Report1
            // 
            this.cLineCompare_Report1.Location = new System.Drawing.Point(12, 12);
            this.cLineCompare_Report1.Name = "cLineCompare_Report1";
            this.cLineCompare_Report1.Size = new System.Drawing.Size(722, 509);
            this.cLineCompare_Report1.TabIndex = 7;
            // 
            // FCompareReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(736, 550);
            this.Controls.Add(this.cLineCompare_Report1);
            this.Name = "FCompareReports";
            this.Text = "FCompareReports";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CLineCompare_Report cLineCompare_Report1;
    }
}