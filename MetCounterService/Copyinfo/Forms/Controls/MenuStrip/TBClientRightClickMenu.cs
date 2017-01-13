using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace Copyinfo.Forms.Controls.MenuStrip
{
    class TBClientRightClickMenu : System.Windows.Forms.ContextMenuStrip
    {
        private ListOfOptions SelectedOption = ListOfOptions.None;
        private TBDelegates.ContextMenuClosed menuClosed;

        public enum ListOfOptions
        {
            None,
            Delete,
            Edit
        }

        public TBClientRightClickMenu(TBDelegates.ContextMenuClosed onMenuClosed) : base()
        {
            Items.Add(ListOfOptions.Delete.ToString());
            Items.Add(ListOfOptions.Edit.ToString());

            this.menuClosed = onMenuClosed;

            ItemClicked += TBClientRightClickMenu_ItemClicked;
            Closed += TBClientRightClickMenu_Closed;
        }

        private void TBClientRightClickMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == ListOfOptions.Edit.ToString())
            {
                SelectedOption = ListOfOptions.Edit;
            }else if
                (e.ClickedItem.Text == ListOfOptions.Delete.ToString())
            {
                SelectedOption = ListOfOptions.Delete;
                MessageBox.Show("Nie zaimplementowano");
            }
               
        }

        private void TBClientRightClickMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            menuClosed();
        }

        public ListOfOptions getSelectedOption()
        {
            return SelectedOption;
        }

    }
}
