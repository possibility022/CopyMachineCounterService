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
    public partial class FEditBox : Form
    {
        public FEditBox()
        {
            InitializeComponent();
        }

        public FEditBox(string text)
        {
            InitializeComponent();
            tbTextBox1.Text = text;
        }

        public string GetValue()
        {
            return tbTextBox1.Text;
        }

        private void tbButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
