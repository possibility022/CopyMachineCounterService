using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace Copyinfo.Forms.Controls.Buttons
{
    class TBButton_Small : TBButtonBase
    {
        public TBButton_Small() : base()
        {
            this.Font = Style.btnFont_Small;
            this.Size = Style.btnSize_Small;
            this.FlatStyle = Style.btnFlatStyle;
        }
    }
}
