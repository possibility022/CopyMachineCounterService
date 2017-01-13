using System.Windows.Forms;

namespace Copyinfo.Forms.Controls.Buttons
{
    class TBButton : Button
    {
        public TBButton() : base()
        {
            this.Font = Style.btnFont;
            this.Size = Style.btnSize;
            this.FlatStyle = Style.btnFlatStyle;
        }
    }
}
