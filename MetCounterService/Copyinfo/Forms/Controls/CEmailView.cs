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
        }

        public void setText(string text)
        {
            this.richTextBox1.Text = text;
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
    }
}
