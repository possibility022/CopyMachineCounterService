using System.Windows.Forms;
using Copyinfo.Forms.Controls.MenuStrip;

namespace Copyinfo.Forms.Controls.Labels
{
    class TBLabel : Label
    {
        private TBMenuStrip menu = null;

        public override string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                if (menu != null) menu.valueToCopy = value;
            }
        }

        private bool copyOn = false;
        public bool CopyOn
        {
            get { return copyOn; }
            set
            {
                if (copyOn == false && value == true)
                {
                    this.MouseClick += TBLabel_MouseClick;
                    menu = new TBMenuStrip(Text, value, SetValue);
                    copyOn = value;
                }
            }
        }

        public TBLabel() : base()
        {
            this.Font = Style.labelFont;
        }

        private void SetValue(string value)
        {
            this.Text = value;
        }

        private void TBLabel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                menu.Show(Cursor.Position);
            }
        }
    }
}
