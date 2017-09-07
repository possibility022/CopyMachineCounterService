using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Copyinfo.Database;

namespace Copyinfo.Forms.Controls.DeviceControls
{
    public partial class DeviceDetailsInfo : UserControl
    {

        Device device;
        Client client;

        public DeviceDetailsInfo()
        {
            InitializeComponent();
        }

        public DeviceDetailsInfo(Device device)
        {
            this.device = device;
        }

        public void SetDevice(Device device)
        {
            this.device = device;

            if (device != null)
            {

                tblProvider.Text = device.provider;
                tblModel.Text = device.model;
                tblSerialNumber.Text = device.serial_number;

                cAddress1.SetAddress(device.address);
            }

            MachineRecordData rec = device.GetLastRecord();
            if (rec != null)
            {
                tblBlackAndWhite.Text = rec.print_counter_black_and_white.ToString();
                tblColor.Text = rec.print_counter_color.ToString();
                tblScans.Text = rec.scan_counter.ToString();
            }

            ServiceReport report = DAO.GetLastReport(device.id);
            if (report != null)
            {
                tblAsystentCounter.Text = report.Counter.ToString();
                tblLastService.Text = report.DateOfServiceClosed.ToShortDateString();
                tblTechnican.Text = report.Technican;
            }

            Client client = device.GetClient();
            if (client != null)
            {
                tblClientName.Text = client.name;
                tblNipName.Text = client.NIP;
                tblAddress.Text = client.address;
            }

            this.client = client;
        }

        private void btnFindClientAddres_Click(object sender, EventArgs e)
        {
            client.GetAddress().SearchInGoogleMaps();
        }

        private void tbButton_Small1_Click(object sender, EventArgs e)
        {
            device.address.SearchInGoogleMaps();
        }

        private void btnFindClientAddres_Click_1(object sender, EventArgs e)
        {
            client.GetAddress().SearchInGoogleMaps();
        }
    }
}
