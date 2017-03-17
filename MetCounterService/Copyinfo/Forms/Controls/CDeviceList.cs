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
            tbTBProvider.id = 0;
            tbTBModel.id = 1;
            tbTBSerialNumber.id = 2;
            tbTBAddress.id = 3;
            tbTBData.id = 4;
            tbTBServiceAgreement.id = 5;
//            tbListView1.setColumnsWithDate(new int[] { 4 });
        }

        public void LoadList(List<Database.Device> list)
        {
            tbListView1.Items.Clear();
            AddToList(list);
        }

        public void AddToList(List<Database.Device> list)
        {
            List<Database.Device> devices = new List<Database.Device>(list);
            ListViewItem item;
            string[] values = { };
            for (int i = 0; i < devices.Count; i++)
            {
                Database.Device d = devices[i];
                values = new string[] { d.provider, d.model, d.serial_number, d.GetAddress().street + " " + d.GetAddress().city, d.instalation_datetime.ToString(Style.DateTimeFormat), d.service_agreement ? "tak" : "nie" };
                item = new Controls.ListView.TBListViewItem(values, d);
                tbListView1.Items.Add(item);
            }
        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (tbListView1.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void showReportsForThisDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Database.Device device = GetSelectedDevice();
            new FReports(Database.DAO.GetReports(device.serial_number)).Show();
        }

        public void DeleteSelectedDevices()
        {
            List<ListViewItem> toRemove = new List<ListViewItem>();
            DialogResult r = MessageBox.Show("Czy napewno chcesz usunąć te urządzenie / urządzenia?", "Uwaga!", MessageBoxButtons.YesNoCancel);
            try
            {
                if (r == DialogResult.Yes)
                {
                    for (int i = 0; i < tbListView1.SelectedItems.Count; i++)
                    {
                        Controls.ListView.TBListViewItem item = (Controls.ListView.TBListViewItem)tbListView1.SelectedItems[i];
                        bool result = Database.DAO.DeleteDevice((Database.Device)item.additionalItem);
                        if (result)
                        {
                            toRemove.Add(item);
                        }
                    }
                }

                foreach (ListViewItem i in toRemove)
                {
                    tbListView1.Items.Remove(i);
                }
            }catch(NotImplementedException notImplemented)
            {
                MessageBox.Show("Ta funkcja nie została jescze zaimplementowana. " + notImplemented.Message);
            }
        } // NOT IMPLEMENTED! and DONT DO THIS!

        private void AlignTextBoxesAndListView()
        {
            tbListView1.Height = this.Height - tbTBAddress.Height - 2;
            GUI.AlignTextBoxes(
                this.tbListView1.GetColumnSizeHeaders(),
                new TextBox[] {
                    tbTBProvider,
                    tbTBModel,
                    tbTBSerialNumber,
                    tbTBAddress,
                    tbTBData,
                    tbTBServiceAgreement},
                tbListView1.Location.Y - tbTBProvider.Size.Height, 0);
        }

        private void listView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            AlignTextBoxesAndListView();
        }

        private void tbTBProvider_TextChanged(object sender, EventArgs e)
        {
            TextBoxes.TBTextBox s = (TextBoxes.TBTextBox)sender;
            tbListView1.Filter(s.id, s.Text);
        }

        private void CDeviceList_Resize(object sender, EventArgs e)
        {
            AlignTextBoxesAndListView();
        }

        private void ShowAddressForSelectedDevice(object sender, EventArgs e)
        {
            if (this.tbListView1.SelectedItems.Count > 0)
            {
                Database.Device device = GetSelectedDevice();
                FAddress fAdress = new FAddress(device.GetAddress());
                fAdress.Show();
            }
        }

        private void ShowRecordFromThisMonth(object sender, EventArgs e)
        {
            Database.Device device = GetSelectedDevice();
            DateTime datetime = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);

            Database.MachineRecord record = Database.DAO.GetFirstInMonth(device.serial_number, datetime);
        }

        private Database.Device GetSelectedDevice()
        {
            if (this.tbListView1.SelectedItems.Count > 0)
            {
                Controls.ListView.TBListViewItem item = (Controls.ListView.TBListViewItem)tbListView1.SelectedItems[0];
                Database.Device device = (Database.Device)item.additionalItem;
                return device;
            }

            return null;
        }

        public List<Database.Device> GetSelectedDevices()
        {
            List<Database.Device> devices = new List<Database.Device>();

            if(tbListView1.SelectedItems.Count > 0)
            {
                foreach(ListViewItem item in tbListView1.SelectedItems)
                {
                    Controls.ListView.TBListViewItem i = (Controls.ListView.TBListViewItem)item;
                    Database.Device device = (Database.Device)i.additionalItem;
                    devices.Add(device);
                }
            }

            return devices;
        }
    }
}
