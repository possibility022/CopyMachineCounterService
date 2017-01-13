using System;
using System.Windows.Forms;

namespace Copyinfo.Forms.Controls.Combobox
{
    class TBCombobox : ComboBox
    {
        bool addonenter = false;

        public TBCombobox() : base()
        {
            this.KeyUp += TBCombobox_KeyUp;
        }

        public void AddItemOnEnter(bool OnOff)
        {
            this.addonenter = OnOff;
        }

        private void TBCombobox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                addValue(this.Text);
        }

        async private void changeColors()
        {
            this.BackColor = Style.comboBoxBackColor;
            await System.Threading.Tasks.Task.Delay(300);
            this.BackColor = System.Drawing.Color.White;
        }

        private void addValue(string value)
        {
            if (this.addonenter)
            {
                if (Items.Contains(value) == false)
                {
                    Items.Add(value);
                    changeColors();
                }
            }
        }
    }
}
