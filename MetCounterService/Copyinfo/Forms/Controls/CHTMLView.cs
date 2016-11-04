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
    public partial class CHTMLView : UserControl
    {
        public CHTMLView()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        public CHTMLView(string htmltext = "")
        {
            InitializeComponent();
            setHTML(htmltext);
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        public void setHTML(string html)
        {
            this.webBrowser1.DocumentText = html;
        }
    }
}
