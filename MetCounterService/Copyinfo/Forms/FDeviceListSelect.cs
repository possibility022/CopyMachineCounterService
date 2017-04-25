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
            cDeviceList1.fastObjectListView1.SetObjects(Database.DAO.GetAllDevices());
        }

        public List<Database.Device> GetSelected()
        {
            List<Database.Device> devices = new List<Database.Device>();

            foreach (Database.Device dev in cDeviceList1.fastObjectListView1.SelectedItems)
                devices.Add(dev);

            return devices;
        }

        private void tbButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
