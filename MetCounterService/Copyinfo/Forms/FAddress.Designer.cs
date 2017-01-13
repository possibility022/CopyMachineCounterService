namespace Copyinfo.Forms
{
    partial class FAddress
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
            this.btnOk = new System.Windows.Forms.Button();
            this.cAddress1 = new Copyinfo.Forms.Controls.CAddress();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.White;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnOk.Location = new System.Drawing.Point(101, 236);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(170, 34);
            this.btnOk.TabIndex = 14;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cAddress1
            // 
            this.cAddress1.BackColor = System.Drawing.Color.White;
            this.cAddress1.Location = new System.Drawing.Point(12, 12);
            this.cAddress1.Name = "cAddress1";
            this.cAddress1.Size = new System.Drawing.Size(369, 205);
            this.cAddress1.TabIndex = 0;
            // 
            // FAddress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(387, 282);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cAddress1);
            this.Name = "FAddress";
            this.Text = "FAddress";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnOk;
        public Controls.CAddress cAddress1;
    }
}