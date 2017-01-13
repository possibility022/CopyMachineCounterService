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
    public partial class FRichTextView : Form
    {
        public FRichTextView()
        {
            InitializeComponent();
        }

        public FRichTextView(string text)
        {
            InitializeComponent();
            this.richTextBox1.Text = text;
        }
    }
}
