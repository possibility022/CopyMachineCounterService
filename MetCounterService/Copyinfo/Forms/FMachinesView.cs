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
    public partial class FMachinesView : Form
    {
        public FMachinesView()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FAddDevice newDevice = new FAddDevice();
            newDevice.Show();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            cDeviceList1.loadList(Global.database.getAllDevices());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cDeviceList1.DeleteSelectedDevices();
        }
    }
}
