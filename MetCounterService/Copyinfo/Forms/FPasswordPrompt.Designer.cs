namespace Copyinfo.Forms
{
    partial class FPasswordPrompt
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
            this.tbTextBox1 = new Copyinfo.Forms.Controls.TextBoxes.TBTextBox();
            this.tbButton1 = new Copyinfo.Forms.Controls.Buttons.TBButton();
            this.SuspendLayout();
            // 
            // tbTextBox1
            // 
            this.tbTextBox1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbTextBox1.id = 0;
            this.tbTextBox1.Location = new System.Drawing.Point(12, 12);
            this.tbTextBox1.Name = "tbTextBox1";
            this.tbTextBox1.PasswordChar = '-';
            this.tbTextBox1.Size = new System.Drawing.Size(284, 25);
            this.tbTextBox1.TabIndex = 1;
            this.tbTextBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbTextBox1_KeyUp);
            // 
            // tbButton1
            // 
            this.tbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbButton1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tbButton1.Location = new System.Drawing.Point(12, 43);
            this.tbButton1.Name = "tbButton1";
            this.tbButton1.Size = new System.Drawing.Size(284, 34);
            this.tbButton1.TabIndex = 2;
            this.tbButton1.Text = "ok";
            this.tbButton1.UseVisualStyleBackColor = true;
            this.tbButton1.Click += new System.EventHandler(this.tbButton1_Click);
            // 
            // FPasswordPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 92);
            this.Controls.Add(this.tbButton1);
            this.Controls.Add(this.tbTextBox1);
            this.Name = "FPasswordPrompt";
            this.Text = "FPasswordPrompt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Controls.Buttons.TBButton tbButton1;
        public Controls.TextBoxes.TBTextBox tbTextBox1;
    }
}