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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CEmailView));
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.btnPreview = new Copyinfo.Forms.Controls.Buttons.TBButton_Small();
            this.btnSettings = new Copyinfo.Forms.Controls.Buttons.TBButton_Small();
            this.btnPrint = new Copyinfo.Forms.Controls.Buttons.TBButton_Small();
            this.tbRichTextBoxPrint1 = new Copyinfo.Forms.Controls.RichTextBoxes.TBRichTextBoxPrint();
            this.tbListView1 = new Copyinfo.Forms.Controls.ListView.TBListView();
            this.SuspendLayout();
            // 
            // printDialog1
            // 
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // pageSetupDialog1
            // 
            this.pageSetupDialog1.Document = this.printDocument1;
            // 
            // btnPreview
            // 
            this.btnPreview.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPreview.Location = new System.Drawing.Point(710, 485);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(90, 32);
            this.btnPreview.TabIndex = 5;
            this.btnPreview.Text = "Podgląd";
            this.btnPreview.UseVisualStyleBackColor = true;
            // 
            // btnSettings
            // 
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSettings.Location = new System.Drawing.Point(806, 485);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(90, 32);
            this.btnSettings.TabIndex = 4;
            this.btnSettings.Text = "Ustawienia";
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPrint.Location = new System.Drawing.Point(614, 485);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(90, 32);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Drukuj";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // tbRichTextBoxPrint1
            // 
            this.tbRichTextBoxPrint1.Location = new System.Drawing.Point(3, 3);
            this.tbRichTextBoxPrint1.Name = "tbRichTextBoxPrint1";
            this.tbRichTextBoxPrint1.Size = new System.Drawing.Size(589, 511);
            this.tbRichTextBoxPrint1.TabIndex = 2;
            this.tbRichTextBoxPrint1.Text = "";
            // 
            // tbListView1
            // 
            this.tbListView1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbListView1.FullRowSelect = true;
            this.tbListView1.Location = new System.Drawing.Point(598, 3);
            this.tbListView1.Name = "tbListView1";
            this.tbListView1.Size = new System.Drawing.Size(298, 511);
            this.tbListView1.TabIndex = 1;
            this.tbListView1.UseCompatibleStateImageBehavior = false;
            this.tbListView1.View = System.Windows.Forms.View.List;
            this.tbListView1.DoubleClick += new System.EventHandler(this.tbListView1_DoubleClick);
            // 
            // CEmailView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.tbRichTextBoxPrint1);
            this.Controls.Add(this.tbListView1);
            this.Name = "CEmailView";
            this.Size = new System.Drawing.Size(896, 517);
            this.ResumeLayout(false);

        }

        #endregion
        private ListView.TBListView tbListView1;
        private RichTextBoxes.TBRichTextBoxPrint tbRichTextBoxPrint1;
        private Buttons.TBButton_Small btnPrint;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private Buttons.TBButton_Small btnSettings;
        private Buttons.TBButton_Small btnPreview;
    }
}
