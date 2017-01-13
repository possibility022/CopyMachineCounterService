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

        public AddClient()
        {
            init();
            client = new Database.Client();
        }
        
        public void setClient(Database.Client client)
        {
            this.client = client;
            fillControl(client);
            //init();
        }

        private void init()
        {
            InitializeComponent();
            this.lName.SetCopyOn();
            this.lNIP.SetCopyOn();
            this.tbCbMails.AddItemOnEnter(true);
            this.tbCbSites.AddItemOnEnter(true);
            this.tbCbFax.AddItemOnEnter(true);
            this.tbCbPhones.AddItemOnEnter(true);
        }

        private void fillControl(Database.Client client)
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
            this.lNIP.Text = client.id;
            this.tbNote.Text = client.notes;

            this.checkBox1.Checked = client.ser_agr;

            cDeviceList1.addToList(client.getDevices());

            fillAddress();
        }

        private void fillAddress()
        {
            Database.Address address = client.getAddress();
            if (address != null)
            {
                lStreet.Text = address.street;
                lCity.Text = address.city;
            }
        }

        private void showDeviceListForSelection()
        {
            FDeviceListSelect devicesForm = new FDeviceListSelect();
            devicesForm.ShowDialog();

            List<Database.Device> devices = devicesForm.getSelected();
            this.cDeviceList1.addToList(devices);
            foreach (Database.Device d in devices)
            {
                client.addDevice(d.id);
            }
        }

        private void tbButton_Small1_Click(object sender, EventArgs e)
        {
            showDeviceListForSelection();
        }

        private void btnEditAddress_Click(object sender, EventArgs e)
        {
            FAddress fAddres = new FAddress(client.getAddress());
            fAddres.ShowDialog();
            client.setAddress(fAddres.cAddress1.getAddress());
            fillAddress();
        }

        public Database.Client getClient()
        {
            client.id = lNIP.Text;
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
    }
}
