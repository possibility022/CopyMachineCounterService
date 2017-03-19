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
            this.objectListView1 = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tbButton1 = new Copyinfo.Forms.Controls.Buttons.TBButton();
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn9 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // objectListView1
            // 
            this.objectListView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.objectListView1.AllColumns.Add(this.olvColumn1);
            this.objectListView1.AllColumns.Add(this.olvColumn2);
            this.objectListView1.AllColumns.Add(this.olvColumn3);
            this.objectListView1.AllColumns.Add(this.olvColumn4);
            this.objectListView1.AllColumns.Add(this.olvColumn5);
            this.objectListView1.AllColumns.Add(this.olvColumn6);
            this.objectListView1.AllColumns.Add(this.olvColumn7);
            this.objectListView1.AllColumns.Add(this.olvColumn8);
            this.objectListView1.AllColumns.Add(this.olvColumn9);
            this.objectListView1.CellEditUseWholeCell = false;
            this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn5,
            this.olvColumn6,
            this.olvColumn7,
            this.olvColumn8,
            this.olvColumn9});
            this.objectListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListView1.Location = new System.Drawing.Point(44, 53);
            this.objectListView1.Name = "objectListView1";
            this.objectListView1.Size = new System.Drawing.Size(747, 386);
            this.objectListView1.TabIndex = 0;
            this.objectListView1.UseCompatibleStateImageBehavior = false;
            this.objectListView1.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "serial_number";
            this.olvColumn1.Text = "Numer Seryjny";
            this.olvColumn1.Width = 116;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "print_counter_black_and_white";
            this.olvColumn2.Text = "B&W";
            this.olvColumn2.Width = 96;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "print_counter_color ";
            this.olvColumn3.Text = "Kolor";
            this.olvColumn3.Width = 88;
            // 
            // tbButton1
            // 
            this.tbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbButton1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tbButton1.Location = new System.Drawing.Point(12, 12);
            this.tbButton1.Name = "tbButton1";
            this.tbButton1.Size = new System.Drawing.Size(170, 34);
            this.tbButton1.TabIndex = 1;
            this.tbButton1.Text = "Załaduj";
            this.tbButton1.UseVisualStyleBackColor = true;
            this.tbButton1.Click += new System.EventHandler(this.tbButton1_Click);
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "scan_counter";
            this.olvColumn4.Text = "Skany";
            this.olvColumn4.Width = 89;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "datetime";
            this.olvColumn5.Text = "Data Odczytu";
            this.olvColumn5.Width = 135;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "tonerlevel_k";
            this.olvColumn6.Text = "Czarny Toner";
            this.olvColumn6.Width = 99;
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "tonerlevel_c";
            this.olvColumn7.Text = "Cyjan Toner";
            this.olvColumn7.Width = 94;
            // 
            // olvColumn8
            // 
            this.olvColumn8.AspectName = "tonerlevel_m";
            this.olvColumn8.Text = "Toner Magenta";
            this.olvColumn8.Width = 103;
            // 
            // olvColumn9
            // 
            this.olvColumn9.AspectName = "tonerlevel_y";
            this.olvColumn9.Text = "Toner Żółty";
            this.olvColumn9.Width = 86;
            // 
            // FTestingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 541);
            this.Controls.Add(this.tbButton1);
            this.Controls.Add(this.objectListView1);
            this.Name = "FTestingForm";
            this.Text = "FTestingForm";
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ObjectListView objectListView1;
        private Controls.Buttons.TBButton tbButton1;
        private OLVColumn olvColumn1;
        private OLVColumn olvColumn2;
        private OLVColumn olvColumn3;
        private OLVColumn olvColumn4;
        private OLVColumn olvColumn5;
        private OLVColumn olvColumn6;
        private OLVColumn olvColumn7;
        private OLVColumn olvColumn8;
        private OLVColumn olvColumn9;
    }
}