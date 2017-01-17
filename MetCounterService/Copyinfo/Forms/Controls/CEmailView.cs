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
        public CEmailView()
        {
            InitializeComponent();
        }

        public void setText(string text)
        {
            this.richTextBox1.Text = text;
        }

        public void addAttachment()
        {

        }
    }
}
