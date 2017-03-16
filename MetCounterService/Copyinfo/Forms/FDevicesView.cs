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
    public partial class FDevicesView : Form
    {
        Database.Client client = null;

        int freeSpaceAtTop;

        public FDevicesView()
        {
            InitializeComponent();
            Init();
        }

        public FDevicesView(string clientID)
        {
            InitializeComponent();
            loadClient(clientID);
            Init();
        }

        private void Init()
        {
            freeSpaceAtTop = this.Height - cDeviceList1.Height;
        }

        private void loadClient(string clientID)
        {
            this.client = Database.DAO.getClient(clientID);
            this.cDeviceList1.loadList(client.getDevices());
            button1.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FAddDevice newDevice = new FAddDevice();
            newDevice.Show();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (client == null)
                //cDeviceList1.loadList(Global.database.getAllDevices());
                cDeviceList1.loadList(Database.DAO.getAllDevices());
            else
                cDeviceList1.loadList(client.getDevices());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cDeviceList1.DeleteSelectedDevices();
        }

        private void FDevicesView_Resize(object sender, EventArgs e)
        {
            GUI.calculateHeight(cDeviceList1, this, freeSpaceAtTop);
        }
    }
}
