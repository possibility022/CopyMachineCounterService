using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Copyinfo.Forms
{
    public partial class FEmailSend : Form
    {

        string subject = "";

        public FEmailSend()
        {
            InitializeComponent();
            tbButton1.Click += SendEmail;
            comboBox1.SelectedIndex = 0;
        }

        private void SendEmail(object sender, EventArgs e)
        {
            Other.Email.SendEmail(comboBox1.Text, tbRichTextBoxPrint1.Text, this.subject);
        }

        public void SetText(string text, string subject)
        {
            this.tbRichTextBoxPrint1.Text = text;
            this.subject = subject;
        }
    }
}
