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

            btnDownload_Click(null, null);
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
            List<Database.Device> list;

            if (cDeviceList1.fastObjectListView1.SelectedObjects.Count > 0)
                list = cDeviceList1.fastObjectListView1.SelectedObjects.Cast<Database.Device>().ToList();
            else
                list = cDeviceList1.fastObjectListView1.Objects.Cast<Database.Device>().ToList();
            print(list);
        }

        private void FDevicesView_ResizeEnd(object sender, EventArgs e)
        {
            GUI.CalculateHeight(cDeviceList1, this, freeSpaceAtTop);
        }

        private void print(List<Database.Device> list)
        {
            List<Database.MachineRecord> toprint = new List<Database.MachineRecord>();
            foreach (Database.Device d in list)
            {
                Database.MachineRecord record = d.GetOneRecord(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
                if (record != null)
                    toprint.Add(record);
            }

            Other.Printing.Print(toprint);
        }
    }
}
