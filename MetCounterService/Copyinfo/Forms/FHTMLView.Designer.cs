namespace Copyinfo.Forms
{
    partial class FHTMLView
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
            this.chtmlView1 = new Copyinfo.Forms.Controls.CHTMLView();
            this.SuspendLayout();
            // 
            // chtmlView1
            // 
            this.chtmlView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chtmlView1.Location = new System.Drawing.Point(0, 0);
            this.chtmlView1.Name = "chtmlView1";
            this.chtmlView1.Size = new System.Drawing.Size(827, 537);
            this.chtmlView1.TabIndex = 0;
            // 
            // FHTMLView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 537);
            this.Controls.Add(this.chtmlView1);
            this.Name = "FHTMLView";
            this.Text = "FHTMLView";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CHTMLView chtmlView1;
    }
}