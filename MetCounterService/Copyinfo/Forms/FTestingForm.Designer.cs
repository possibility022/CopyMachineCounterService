using BrightIdeasSoftware;

namespace Copyinfo.Forms
{
    partial class FTestingForm
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
            this.cRecordMongoDetails1 = new Copyinfo.Forms.Controls.CRecordMongoDetails();
            this.SuspendLayout();
            // 
            // cRecordMongoDetails1
            // 
            this.cRecordMongoDetails1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cRecordMongoDetails1.Location = new System.Drawing.Point(0, 0);
            this.cRecordMongoDetails1.Name = "cRecordMongoDetails1";
            this.cRecordMongoDetails1.Size = new System.Drawing.Size(872, 541);
            this.cRecordMongoDetails1.TabIndex = 0;
            // 
            // FTestingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 541);
            this.Controls.Add(this.cRecordMongoDetails1);
            this.Name = "FTestingForm";
            this.Text = "FTestingForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CRecordMongoDetails cRecordMongoDetails1;
    }
}