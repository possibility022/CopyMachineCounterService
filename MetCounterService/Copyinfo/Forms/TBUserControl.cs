using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace Copyinfo.Forms
{
    public class TBUserControl : System.Windows.Forms.UserControl
    {
        public TBUserControl() : base()
        {

        }

        protected void setTextBoxWidith_EqualTo_ListView(int[] widith, TextBox[] textBoxes)
        {
            for (int i = 0; i < widith.Length; i++)
                textBoxes[i].Width = widith[i];
        }
    }
}
