using System.Windows.Forms;

namespace Copyinfo.Forms.Controls.Buttons
{
    class TBButton : TBButtonBase
    {
        public TBButton() : base()
        {
            this.Font = Style.btnFont;
            this.Size = Style.btnSize;
            this.FlatStyle = Style.btnFlatStyle;
        }
    }
}
