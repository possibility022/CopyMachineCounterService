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
    public partial class FPasswordPrompt : Form
    {
        public FPasswordPrompt()
        {
            InitializeComponent();
        }

        private void tbButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                this.Close();
        }
    }
}
