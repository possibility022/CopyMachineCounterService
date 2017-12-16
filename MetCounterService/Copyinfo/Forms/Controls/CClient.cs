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
    public partial class AddClient : UserControl
    {
        private Database.Client client;

        int freeSpaceAtTop;

        public AddClient()
        {
            Init();
            client = new Database.Client();
        }
        
        public void setClient(Database.Client client)
        {
            this.client = client;
            FillControl(client);
            Init();
        }

        private void Init()
        {
            InitializeComponent();
            this.lName.CopyOn = true;
            this.lNIP.CopyOn = true;
            this.tbCbMails.AddItemOnEnter(true);
            this.tbCbSites.AddItemOnEnter(true);
            this.tbCbFax.AddItemOnEnter(true);
            this.tbCbPhones.AddItemOnEnter(true);
            freeSpaceAtTop = this.Height - cDeviceList1.Height;
        }

        private void FillControl(Database.Client client)
        {
            foreach (string s in client.f_numbers)
                tbCbFax.Items.Add(s);

            foreach (string s in client.p_numbers)
                tbCbPhones.Items.Add(s);

            foreach (string s in client.wwwsites)
                tbCbSites.Items.Add(s);

            foreach (string s in client.emails)
                tbCbMails.Items.Add(s);

            if (tbCbFax.Items.Count > 0)
                tbCbFax.SelectedIndex = tbCbFax.Items.Count - 1;

            if (tbCbPhones.Items.Count > 0)
                tbCbPhones.SelectedIndex = tbCbPhones.Items.Count - 1;

            if (tbCbSites.Items.Count > 0)
                tbCbSites.SelectedIndex = tbCbSites.Items.Count - 1;

            if (tbCbMails.Items.Count > 0)
                tbCbMails.SelectedIndex = tbCbMails.Items.Count - 1;

            this.lName.Text = client.name;
            this.lNIP.Text = client.NIP;
            this.tbNote.Text = client.notes;

            this.checkBox1.Checked = client.ser_agr;

            cDeviceList1.fastObjectListView1.AddObjects(client.GetDevices());

            FillAddress();
        }

        private void FillAddress()
        {
            Database.Address address = client.GetAddress();

            if (address != null)
            {
                lStreet.Text = address.street;
                lCity.Text = address.city;
            }
            
        }

        private void ShowDeviceListForSelection()
        {
            MessageBox.Show("Ta opcja zostala usunieta");
            //FDeviceListSelect devicesForm = new FDeviceListSelect();
            //devicesForm.ShowDialog();

            //List<Database.Device> devices = devicesForm.getSelected();
            //this.cDeviceList1.addToList(devices);
            //foreach (Database.Device d in devices)
            //{
            //    client.addDevice(d.serial_number);
            //}
        }

        private void tbButton_Small1_Click(object sender, EventArgs e)
        {
            ShowDeviceListForSelection();
        }

        private void btnEditAddress_Click(object sender, EventArgs e)
        {
            new FAddress(client.GetAddress()).Show();
        }

        public Database.Client getClient()
        {
            client.NIP = lNIP.Text;
            client.name = lName.Text;

            client.notes = tbNote.Text;

            client.p_numbers = new string[tbCbPhones.Items.Count];
            tbCbPhones.Items.CopyTo(client.p_numbers, 0);

            client.wwwsites = new string[tbCbSites.Items.Count];
            tbCbSites.Items.CopyTo(client.wwwsites, 0);

            client.f_numbers = new string[tbCbFax.Items.Count];
            tbCbFax.Items.CopyTo(client.f_numbers, 0);

            client.emails = new string[tbCbMails.Items.Count];
            tbCbMails.Items.CopyTo(client.emails, 0);

            client.ser_agr = checkBox1.Checked;

            return client;
        }

        private void AddClient_Resize(object sender, EventArgs e)
        {
            GUI.CalculateHeight(cDeviceList1, this, freeSpaceAtTop);
        }
    }
}
