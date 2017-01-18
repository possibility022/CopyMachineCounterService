namespace Copyinfo.Forms
{
    partial class FEmailView
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
            this.cEmailView1 = new Copyinfo.Forms.Controls.CEmailView();
            this.SuspendLayout();
            // 
            // cEmailView1
            // 
            this.cEmailView1.Location = new System.Drawing.Point(12, 12);
            this.cEmailView1.Name = "cEmailView1";
            this.cEmailView1.Size = new System.Drawing.Size(810, 517);
            this.cEmailView1.TabIndex = 0;
            // 
            // FEmailView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 564);
            this.Controls.Add(this.cEmailView1);
            this.Name = "FEmailView";
            this.Text = "FEmailView";
            this.ResumeLayout(false);

        }

        #endregion

        public Controls.CEmailView cEmailView1;
    }
}