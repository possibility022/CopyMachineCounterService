namespace Copyinfo.Forms
{
    partial class FEditBox
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
            this.tbButton1 = new Copyinfo.Forms.Controls.Buttons.TBButton();
            this.tbTextBox1 = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.SuspendLayout();
            // 
            // tbButton1
            // 
            this.tbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbButton1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tbButton1.Location = new System.Drawing.Point(12, 43);
            this.tbButton1.Name = "tbButton1";
            this.tbButton1.Size = new System.Drawing.Size(260, 32);
            this.tbButton1.TabIndex = 1;
            this.tbButton1.Text = "OK";
            this.tbButton1.UseVisualStyleBackColor = true;
            this.tbButton1.Click += new System.EventHandler(this.tbButton1_Click);
            // 
            // tbTextBox1
            // 
            this.tbTextBox1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTextBox1.Location = new System.Drawing.Point(12, 12);
            this.tbTextBox1.Name = "tbTextBox1";
            this.tbTextBox1.Size = new System.Drawing.Size(260, 25);
            this.tbTextBox1.TabIndex = 2;
            // 
            // FEditBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 87);
            this.Controls.Add(this.tbTextBox1);
            this.Controls.Add(this.tbButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FEditBox";
            this.Text = "FEditField";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Controls.Buttons.TBButton tbButton1;
        private Controls.TextBoxes.TBTextBox tbTextBox1;
    }
}