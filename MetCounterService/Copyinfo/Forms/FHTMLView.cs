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
    public partial class FHTMLView : Form
    {
        public FHTMLView()
        {
            InitializeComponent();
        }

        public FHTMLView(string html)
        {
            InitializeComponent();
            chtmlView1.SetHTML(html);
        }
    }
}
