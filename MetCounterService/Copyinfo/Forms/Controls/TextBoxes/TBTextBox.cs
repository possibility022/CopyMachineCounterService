using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copyinfo.Forms.Controls.TextBoxes
{
    public class TBTextBox : System.Windows.Forms.TextBox
    {
        public int id { get; set; }

        public TBTextBox() : base()
        {
            this.Font = Style.textBoxFont;
        }
    }
}
