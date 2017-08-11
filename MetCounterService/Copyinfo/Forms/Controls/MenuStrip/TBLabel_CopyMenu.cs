using System.Windows.Forms;

namespace Copyinfo.Forms.Controls.MenuStrip
{
    class TBMenuStrip : System.Windows.Forms.ContextMenuStrip
    {
        private ListOfOptions SelectedOption = ListOfOptions.None;
        private TBDelegates.SetValue setValue;

        public enum ListOfOptions
        {
            None,
            Copy,
            Edit
        }

        public string valueToCopy = string.Empty;

        public TBMenuStrip(string valueToCopy, bool edit = false, TBDelegates.SetValue setValue = null) : base()
        {
            this.valueToCopy = valueToCopy;
            this.Items.Add(ListOfOptions.Copy.ToString());
            if (edit)
            {
                this.Items.Add(ListOfOptions.Edit.ToString());
                this.setValue = setValue;
            }
            this.ItemClicked += TBLabel_CopyMenu_ItemClicked;
        }

        private void TBLabel_CopyMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == ListOfOptions.Copy.ToString())
            {
                Clipboard.SetText(valueToCopy);
            }
            else if (e.ClickedItem.Text == ListOfOptions.Edit.ToString())
            {
                FEditBox editBox = new FEditBox();
                editBox.ShowDialog();
                setValue(editBox.GetValue());
            }
        }

        public ListOfOptions getSelectedOption()
        {
            return SelectedOption;
        }
    }
}
