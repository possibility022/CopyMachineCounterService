using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Copyinfo.Forms.Controls
{
    public partial class CDeviceList : UserControl
    {
        public CDeviceList()
        {
            InitializeComponent();
        }

        public void loadList(List<Database.Device> list)
        {
            listView2.Items.Clear();
            addToList(list);
        }

        public void addToList(List<Database.Device> list)
        {
            List<Database.Device> devices = new List<Database.Device>(list);
            ListViewItem item;
            string[] values = { };
            for (int i = 0; i < devices.Count; i++)
            {
                //foreach (Database.Device d in list)
                Database.Device d = devices[i];
                values = new string[]{ d.provider, d.model, d.serial_number, d.getAddress().street, d.instalation_datetime.ToShortDateString()};
                item = new TBListViewItem_Device(values, d);
                listView2.Items.Add(item);
            }
        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView2.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void showReportsForThisDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TBListViewItem_Device item = (TBListViewItem_Device)listView2.SelectedItems[0];
            new FReports(Global.database.getReports(item.device.serial_number)).Show();
        }

        public void DeleteSelectedDevices()
        {
            List<ListViewItem> toRemove = new List<ListViewItem>();
            DialogResult r = MessageBox.Show("Czy napewno chcesz usunąć te urządzenie / urządzenia?", "Uwaga!", MessageBoxButtons.YesNoCancel);
            if (r == DialogResult.Yes)
            {
                for (int i = 0; i < listView2.SelectedItems.Count; i++)
                {
                    TBListViewItem_Device item = (TBListViewItem_Device)listView2.SelectedItems[i];
                    bool result = Global.database.DeleteDevice(item.device);
                    if(result)
                    {
                        toRemove.Add(item);
                    }
                }
            }

            foreach(ListViewItem i in toRemove)
            {
                listView2.Items.Remove(i);
            }
        }
    }
}
