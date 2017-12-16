using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace Copyinfo.Forms.Controls.DeviceControls
{
    public partial class DeviceModelAndReports : UserControl
    {
        Database.Device device;
        List<Database.ServiceReport> serviceReports = new List<Database.ServiceReport>();
        readonly string[] textToFilterServiceReports = new string[2] { "toner", "tonerów" };

        public DeviceModelAndReports()
        {
            InitializeComponent();

            objectListView1.SelectedIndexChanged += ObjectListView1_SelectedIndexChanged;
            objectListView1.Sort(olvDate, SortOrder.Descending);
            objectListView1.FormatRow += SetSpecialColorForRow;

            tblProvider.CopyOn = true;
            tblSerialNumber.CopyOn = true;

            tblScans.CopyOn = true;
            tblBlackAndWhite.CopyOn = true;
            tblColor.CopyOn = true;

            tbCombobox1.SelectedIndex = 0;
        }

        private void ObjectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillReport();
        }

        private void FillServiceReportsList()
        {
            objectListView1.Items.Clear();
            objectListView1.AddObjects(serviceReports);
        }

        public void FillReport()
        {
            richTextBox1.Text = "";
            Database.ServiceReport report = (Database.ServiceReport)objectListView1.SelectedObject;
            if (report != null)
            {
                string text = "Licznik: " + report.Counter.ToString() + "\r\n\r\n";
                text += "Wykonane czynności: " + "\r\n" + report.Description + "\r\n\r\n";
                text += "Opis usterki: \r\n" + report.ReportedProblem + "\r\n\r\n";
                text += "Zalecenia Serwisu: \r\n" + report.SeviceRecomendation + "\r\n";

                richTextBox1.Text = text;
            }
        }

        private void SetSpecialColorForRow(object sender, FormatRowEventArgs e)
        {
            if (StringContainsToner(serviceReports[e.RowIndex].Description))
                e.Item.BackColor = Style.yellowColor;
        }

        private bool StringContainsToner(string str)
        {
            foreach (string filter in textToFilterServiceReports)
                if (str.ToLower().Contains(filter))
                    return true;
            return false;
        }

        public void SetDevice(Database.Device device)
        {
            this.device = device;

            monthCalendar1.MaxSelectionCount = 1;
            monthCalendar1.SelectionStart = DateTime.Today;
            SetDateInText();

            SetDevice();
        }

        private void SetDevice()
        {
            if (device != null)
            {
                tblProvider.Text = device.provider;
                tblModel.Text = device.model;
                tblSerialNumber.Text = device.serial_number;
                monthCalendar1.SetDate(device.instalation_datetime);

                Database.Client client = device.GetClient();

                tblBlackAndWhite.Text = device.GetLastRecord().print_counter_black_and_white.ToString();
                tblColor.Text = device.GetLastRecord().print_counter_color.ToString();
                tblScans.Text = device.GetLastRecord().scan_counter.ToString();

                serviceReports = Database.DAO.GetServiceReport(device.id);

                FillServiceReportsList();
            }
        }

        private void SetDateInText()
        {
            txtInstallationDate.Text = monthCalendar1.SelectionStart.Date.Day.ToString() + " (" + monthCalendar1.SelectionStart.Date.DayOfWeek.ToString() + ") " + monthCalendar1.SelectionStart.Month.ToString();
        }

        private string GetEmailMessage()
        {
            string message = "";

            Database.Client client = device.GetClient();

            foreach (Database.ServiceReport report in serviceReports)
            {
                message +=
                    "Marka: " + device.provider + "\r\n" +
                    "Model: " + device.model + "\r\n" +
                    "Numer Seryjny: " + device.model + "\r\n";

                if (client != null)
                {
                    message += "Klient: " + client.name + "\r\n" +
                        "Addres instalacji urządzenia: " + device.address.ToString();
                }

                message +=
                    "\r\n---------------------------------------------------------------\r\n" +
                    "Serwis z dnia: " + report.DateOfServiceClosed.ToString(Style.DateTimeFormat) + "\r\n" +
                    "Serwisant: " + report.Technican + "\r\n" +
                    "Licznik: " + report.Counter.ToString() + "\r\n\r\n";

                message += "Wykonane czynności: \r\n" + report.Description + "\r\n\r\n";
                message += "Opis usterki: \r\n" + report.ReportedProblem + "\r\n\r\n";
                message += "Zalecenia Serwisu: \r\n" + report.SeviceRecomendation + "\r\n";
            }

            return message;
        }

        private string GetEmailHeader()
        {
            return "Historia serwisu z asystenta dla urządzenia: " + device.provider + " " + device.model + " " + device.serial_number;
        }

        private void SendServiceHistoryByEmail(object sender, EventArgs e)
        {
            tbButtonSendEmail.Hide();
            tbbtnCancelSending.Show();
            tbbtnSendEmail.Show();
            tbCombobox1.Show();
        }

        private void tbbtnSendEmail_Click(object sender, EventArgs e)
        {
            Other.Email.SendEmail(tbCombobox1.Text, GetEmailMessage(), GetEmailHeader());
            tbbtnSendEmail.Hide();

            tbCombobox1.Hide();
            tbbtnCancelSending.Hide();
            tbButtonSendEmail.Show();
        }

        private void tbbtnCancelSending_Click(object sender, EventArgs e)
        {
            tbCombobox1.Hide();
            tbbtnSendEmail.Hide();
            tbbtnCancelSending.Hide();
            tbButtonSendEmail.Show();
        }

        private void tbButtonSendEmail_Click(object sender, EventArgs e)
        {
            tbButtonSendEmail.Hide();
            tbbtnCancelSending.Show();
            tbbtnSendEmail.Show();
            tbCombobox1.Show();
        }
    }
}
