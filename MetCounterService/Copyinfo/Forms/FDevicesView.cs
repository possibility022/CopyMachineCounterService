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
            this.client = Database.DAO.GetClient(clientID);
            this.cDeviceList1.fastObjectListView1.SetObjects(client.GetDevices());
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (client == null)
                //cDeviceList1.loadList(Global.database.getAllDevices());
                cDeviceList1.fastObjectListView1.SetObjects(Database.DAO.GetAllDevices());
            else
                cDeviceList1.fastObjectListView1.SetObjects(client.GetDevices());
        }

        private void FDevicesView_Resize(object sender, EventArgs e)
        {
            GUI.CalculateHeight(cDeviceList1, this, freeSpaceAtTop);
        }

        private void tbButton_Small1_Click(object sender, EventArgs e)
        {
            List<Database.Device> list = new List<Database.Device>();
            foreach (Database.Device dev in cDeviceList1.fastObjectListView1.SelectedObjects)
                list.Add(dev);
            print(list);
        }

        private void FDevicesView_ResizeEnd(object sender, EventArgs e)
        {
            GUI.CalculateHeight(cDeviceList1, this, freeSpaceAtTop);
        }

        private void print(List<Database.Device> list)
        {
            List<string> toprint = new List<string>();
            foreach (Database.Device d in list)
            {
                Database.MachineRecord record = d.GetOneRecord(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
                if (record != null)
                    toprint.Add(record.GetTextToPrint());
            }

            Other.Printing.Print(toprint);
        }
    }
}
