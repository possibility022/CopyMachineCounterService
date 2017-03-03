using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Copyinfo.Forms.Controls
{
    public partial class CEmailView : UserControl
    {
        List<Other.EmailAttachment> attachments;

        public CEmailView()
        {
            InitializeComponent();
            attachments = new List<Other.EmailAttachment>();

            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            this.btnPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            this.btnSettings.Click += new System.EventHandler(this.btnPageSetup_Click);
        }

        public void setText(string text)
        {
            this.tbRichTextBoxPrint1.Text = text;
        }

        public void addAttachment(Other.EmailAttachment attachment)
        {
            attachments.Add(attachment);
            ListViewItem item = new ListViewItem(attachment.getAttachmentName());
            tbListView1.Items.Add(item);
        }

        public void addAttachments(List<Other.EmailAttachment> attachments)
        {
            foreach (Other.EmailAttachment att in attachments)
                addAttachment(att);
        }

        private void tbListView1_DoubleClick(object sender, EventArgs e)
        {
            ListView.TBListView listView = (ListView.TBListView)sender;
            string file_path = attachments[listView.SelectedItems[0].Index].getFile();
            System.IO.FileInfo file = new System.IO.FileInfo(file_path);
            Global.openEmailAttachment(file.FullName);
        }

        #region Printing
        private int checkPrint;
        private void btnPageSetup_Click(object sender, System.EventArgs e)
        {
            pageSetupDialog1.ShowDialog();
        }

        private void btnPrintPreview_Click(object sender, System.EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void btnPrint_Click(object sender, System.EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            checkPrint = 0;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Print the content of RichTextBox. Store the last character printed.
            checkPrint = tbRichTextBoxPrint1.Print(checkPrint, tbRichTextBoxPrint1.TextLength, e);

            // Check for more pages
            if (checkPrint < tbRichTextBoxPrint1.TextLength)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }
        #endregion
    }
}
