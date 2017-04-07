using System;
using System.Collections;
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
    public partial class FReports : Form
    {
        public FReports()
        {
            InitializeComponent();
        }

        public FReports(Database.Device device)
        {
            InitializeComponent();
            cReports1.fastObjectListView1.SetObjects(Database.DAO.GetReports(device.serial_number));
        }

        public FReports(Database.Client client)
        {
            InitializeComponent();
            AddReportsFromClient(client);
        }

        public FReports(IList list)
        {
            InitializeComponent();

            if (list.Count > 0)
            {
                if (list[0].GetType() == typeof(Database.Device))
                {
                    AddReportsFromDeviceList(list);
                }
                else if (list[0].GetType() == typeof(Database.Client))
                {
                    AddReportsFromClientsList(list);
                }
                else
                {
                    throw new ArgumentException("Do parametru IList podano listę obiektów inną niż Device lub Client.");
                }
            }
        }

        protected void AddReportsFromDeviceList(IList listOfDevices)
        {
            foreach(Database.Device dev in listOfDevices)
            {
                cReports1.fastObjectListView1.AddObjects(Database.DAO.GetReports(dev.serial_number));
            }
        }

        protected void AddReportsFromClientsList(IList listOfClients)
        {
            foreach(Database.Client cli in listOfClients)
            {
                AddReportsFromClient(cli);
            }
        }

        protected void AddReportsFromClient(Database.Client client)
        {
            foreach (Database.Device dev in client.GetDevices())
            {
                cReports1.fastObjectListView1.AddObjects(dev.GetRecords());
            }
        }
    }
}
