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
    public partial class CDevice : UserControl
    {

        Database.Device device;

        public CDevice()
        {
            Init();
        }

        private void Init()
        {
            InitializeComponent();
        }

        public void SetDevice(Database.Device device)
        {
            this.device = device;

            monthCalendar1.MaxSelectionCount = 1;
            monthCalendar1.SelectionStart = DateTime.Today;
            SetDateInText();
            if (device == null)
            {
                cReports1.Visible = false;
            }
            else
            {
                cReports1.SwitchViewMode();
                cReports1.FillList(device.GetRecords());
            }

            SetDevice();
        }

        public void FillReports()
        {
            
        }

        private void SetDevice()
        {
            tblProvider.Text = device.provider;
            tblModel.Text = device.model;
            tblSerialNumber.Text = device.serial_number;
            monthCalendar1.SetDate(device.instalation_datetime);
            cAddress1.SetAddress(device.address);

            Database.Client client = device.GetClient();
            tblClientName.Text = client.name;
            tblNipName.Text = client.NIP;
            tblAddress.Text = client.address;
        }

        private void txtInstallationPlace_DoubleClick(object sender, EventArgs e)
        {

        }

        private void SetDateInText()
        {
            txtInstallationDate.Text = monthCalendar1.SelectionStart.Date.Day.ToString() + " (" + monthCalendar1.SelectionStart.Date.DayOfWeek.ToString() + ") " + monthCalendar1.SelectionStart.Month.ToString();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            SetDateInText();
        }

        private void tbButton1_Click(object sender, EventArgs e)
        {
            new FClient(device.GetClient()).Show();
        }
    }
}
