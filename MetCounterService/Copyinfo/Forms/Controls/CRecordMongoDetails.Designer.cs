namespace Copyinfo.Forms.Controls
{
    partial class CRecordMongoDetails
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
            this.tbLabel1 = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.labelObjectId = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.tbLabel2 = new Copyinfo.Forms.Controls.Labels.TBLabel();
            this.SuspendLayout();
            // 
            // tbLabel1
            // 
            this.tbLabel1.AutoSize = true;
            this.tbLabel1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tbLabel1.Location = new System.Drawing.Point(3, 14);
            this.tbLabel1.Name = "tbLabel1";
            this.tbLabel1.Size = new System.Drawing.Size(68, 21);
            this.tbLabel1.TabIndex = 0;
            this.tbLabel1.Text = "ObjectId";
            // 
            // labelObjectId
            // 
            this.labelObjectId.AutoSize = true;
            this.labelObjectId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.labelObjectId.Location = new System.Drawing.Point(165, 14);
            this.labelObjectId.Name = "labelObjectId";
            this.labelObjectId.Size = new System.Drawing.Size(68, 21);
            this.labelObjectId.TabIndex = 1;
            this.labelObjectId.Text = "ObjectId";
            // 
            // tbLabel2
            // 
            this.tbLabel2.AutoSize = true;
            this.tbLabel2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbLabel2.Location = new System.Drawing.Point(3, 47);
            this.tbLabel2.Name = "tbLabel2";
            this.tbLabel2.Size = new System.Drawing.Size(62, 19);
            this.tbLabel2.TabIndex = 2;
            this.tbLabel2.Text = "tbLabel2";
            // 
            // CRecordMongoDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbLabel2);
            this.Controls.Add(this.labelObjectId);
            this.Controls.Add(this.tbLabel1);
            this.Name = "CRecordMongoDetails";
            this.Size = new System.Drawing.Size(669, 771);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Labels.TBLabel tbLabel1;
        private Labels.TBLabel labelObjectId;
        private Labels.TBLabel tbLabel2;
    }
}
