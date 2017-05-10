namespace Copyinfo.Forms
{
    partial class FEmailSend
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
            this.tbRichTextBoxPrint1 = new Copyinfo.Forms.Controls.RichTextBoxes.TBRichTextBoxPrint();
            this.tbButton1 = new Copyinfo.Forms.Controls.Buttons.TBButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tbRichTextBoxPrint1
            // 
            this.tbRichTextBoxPrint1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbRichTextBoxPrint1.Location = new System.Drawing.Point(12, 12);
            this.tbRichTextBoxPrint1.Name = "tbRichTextBoxPrint1";
            this.tbRichTextBoxPrint1.Size = new System.Drawing.Size(701, 482);
            this.tbRichTextBoxPrint1.TabIndex = 0;
            this.tbRichTextBoxPrint1.Text = "";
            // 
            // tbButton1
            // 
            this.tbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbButton1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tbButton1.Location = new System.Drawing.Point(543, 501);
            this.tbButton1.Name = "tbButton1";
            this.tbButton1.Size = new System.Drawing.Size(170, 34);
            this.tbButton1.TabIndex = 1;
            this.tbButton1.Text = "tbButton1";
            this.tbButton1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "***REMOVED***"});
            this.comboBox1.Location = new System.Drawing.Point(345, 511);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(192, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // FEmailSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(725, 547);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.tbButton1);
            this.Controls.Add(this.tbRichTextBoxPrint1);
            this.Name = "FEmailSend";
            this.Text = "FEmailSend";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.RichTextBoxes.TBRichTextBoxPrint tbRichTextBoxPrint1;
        private Controls.Buttons.TBButton tbButton1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}