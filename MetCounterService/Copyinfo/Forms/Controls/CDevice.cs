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

namespace Copyinfo.Forms.Controls
{
    public partial class CDevice : UserControl
    {

        Database.Device device;
        List<Database.ServiceReport> serviceReports = new List<Database.ServiceReport>();
        readonly string[] textToFilterServiceReports = new string[2] {"toner","tonerów"};

        public CDevice()
        {
            InitializeComponent();

            objectListView1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
            objectListView1.Sort(olvDate, SortOrder.Descending);
            objectListView1.FormatRow += SetSpecialColorForRow;
            
        }

        private void SetSpecialColorForRow(object sender, FormatRowEventArgs e)
        {
            if (StringContainsToner(serviceReports[e.RowIndex].Description))
                e.Item.BackColor = Style.yellowColor;
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillReport();
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
                serviceReports = Database.DAO.GetServiceReport(device.id);
                FillServiceReportsList();
            }

            SetDevice();
        }

        private void FillServiceReportsList()
        {
            objectListView1.Items.Clear();
            objectListView1.AddObjects(serviceReports);
        }

        private bool StringContainsToner(string str)
        {
            foreach (string filter in textToFilterServiceReports)
                if (str.ToLower().Contains(filter))
                    return true;
            return false;
        }

        public void FillReport()
        {
            richTextBox1.Text = "";
            Database.ServiceReport report = (Database.ServiceReport)objectListView1.SelectedObject;
            if (report != null)
            {
                string text = "Licznik: " + report.Counter.ToString() + "\r\n\r\n";
                text += "Wykonane czynności: " + "\r\n" + report.Description + "\r\n\r\n";
                text += "Opis usterki: " + report.ReportedProblem + "\r\n";

                richTextBox1.Text = text;
            }
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
