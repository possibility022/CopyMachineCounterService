namespace Copyinfo.Forms
{
    partial class FDeviceNavibar
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
            this.z80_Navigation1 = new Z80NavBarControl.Z80_Navigation();
            this.SuspendLayout();
            // 
            // z80_Navigation1
            // 
            this.z80_Navigation1.AutoVerticalScrollBar = false;
            this.z80_Navigation1.Dock = System.Windows.Forms.DockStyle.Left;
            this.z80_Navigation1.IconLocation = new System.Drawing.Point(8, 8);
            this.z80_Navigation1.Location = new System.Drawing.Point(0, 0);
            this.z80_Navigation1.Name = "z80_Navigation1";
            this.z80_Navigation1.ShowItemsBorder = false;
            this.z80_Navigation1.ShowSelectedGlyph = true;
            this.z80_Navigation1.Size = new System.Drawing.Size(169, 511);
            this.z80_Navigation1.TabIndex = 0;
            // 
            // FDeviceNavibar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 511);
            this.Controls.Add(this.z80_Navigation1);
            this.Name = "FDeviceNavibar";
            this.Text = "FDeviceNavibar";
            this.ResumeLayout(false);

        }

        #endregion

        private Z80NavBarControl.Z80_Navigation z80_Navigation1;
    }
}