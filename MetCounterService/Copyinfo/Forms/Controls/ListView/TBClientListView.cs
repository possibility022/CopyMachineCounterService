using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Copyinfo.Forms.Controls.ListView
{
    public class TBClientListView : TBListView
    {

        MenuStrip.TBClientRightClickMenu rightClickMenu;

        public TBClientListView() : base()
        {
            this.MouseDoubleClick += TBClientListView_MouseDoubleClick;
            this.MouseClick += TBClientListView_MouseClick;
        }

        private void TBClientListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                rightClickMenu = new MenuStrip.TBClientRightClickMenu(OnContextMenuClosed);
                rightClickMenu.Show(Cursor.Position);
            }
        }

        private void OnContextMenuClosed()
        {
            if (rightClickMenu.GetSelectedOption() == MenuStrip.TBClientRightClickMenu.ListOfOptions.Edit)
            {
                new FClient(Database.DAO.GetClient(SelectedItems[0].SubItems[1].Text)).ShowDialog();
            }
        }

        private void TBClientListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem item in SelectedItems)
                new FDevicesView(item.SubItems[1].Text).Show();
        }

        private void AddToList(Database.Client c)
        {
            Database.Address ad = c.GetAddress();

            string phone = "";
            string mail = "";

            if (c.p_numbers.Length > 0)
                phone = c.p_numbers[0];

            if (c.emails.Length > 0)
                mail = c.emails[0];

            TBListViewItem item = new TBListViewItem(new string[] { c.name, c.NIP, ad.street, ad.city, phone, mail, c.notes, c.ser_agr ? "tak" : "nie"}, c);

            this.Items.Add(item);
        }

        public void SetList(List<Database.Client> clients)
        {
            this.Items.Clear();
            foreach (Database.Client c in clients)
            {
                AddToList(c);
            }
        }
    }
}
