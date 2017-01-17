namespace Copyinfo.Forms.Controls
{
    partial class CEmailView
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tbListView1 = new Copyinfo.Forms.Controls.ListView.TBListView();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(589, 511);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // tbListView1
            // 
            this.tbListView1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbListView1.FullRowSelect = true;
            this.tbListView1.Location = new System.Drawing.Point(598, 3);
            this.tbListView1.Name = "tbListView1";
            this.tbListView1.Size = new System.Drawing.Size(68, 511);
            this.tbListView1.TabIndex = 1;
            this.tbListView1.UseCompatibleStateImageBehavior = false;
            // 
            // CEmailView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbListView1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "CEmailView";
            this.Size = new System.Drawing.Size(669, 517);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private ListView.TBListView tbListView1;
    }
}
