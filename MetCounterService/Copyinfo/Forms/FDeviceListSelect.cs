using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Copyinfo.Forms
{
    public partial class FDeviceListSelect : Form
    {
        public FDeviceListSelect()
        {
            InitializeComponent();
            cDeviceList1.loadList(Database.DAO.getAllDevices());
        }

        public List<Database.Device> getSelected()
        {
            List<Database.Device> devices = new List<Database.Device>();

            foreach (Controls.ListView.TBListViewItem el in this.cDeviceList1.tbListView1.SelectedItems)
                devices.Add((Database.Device)el.additionalItem);

            return devices;
        }

        private void tbButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbButton2_Click(object sender, EventArgs e)
        {
            FAddDevice addDeviceForm = new FAddDevice();
            addDeviceForm.ShowDialog();
        }
    }
}
