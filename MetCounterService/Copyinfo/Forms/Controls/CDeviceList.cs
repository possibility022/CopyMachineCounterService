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
    public partial class CDeviceList : UserControl
    {
        public CDeviceList()
        {
            InitializeComponent();
            GUI.SetTextBoxAndFastListView(tbTextBox1, fastObjectListView1, this);
            Style.InitFastObjectListView(fastObjectListView1, tbTextBox1);
            SetConverters();
            SetContextMenu();
        }

        public void SetConverters()
        {
            olvInstallationDateTime.AspectToStringConverter = delegate (object x)
            {
                DateTime date = (DateTime)x;
                return date.ToShortDateString();
            };
        }

        public void SetContextMenu()
        {
            var item = contextMenuStrip1.Items.Add("Klient");
            item.Click += ShowClient;

            item = contextMenuStrip1.Items.Add("Raporty");
            item.Click += ShowReports;

            item = contextMenuStrip1.Items.Add("Szczegóły");
            item.Click += ShowDetails;

            fastObjectListView1.MouseClick += FastObjectListView1_MouseClick;
        }

        private void FastObjectListView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void ShowDetails(object sender, EventArgs e)
        {
            MessageBox.Show("Jeszcze nie zaimplementowano");
        }

        private void ShowReports(object sender, EventArgs e)
        {
            new FReports((System.Collections.ArrayList)fastObjectListView1.SelectedObjects).Show();
        }

        private void ShowClient(object sender, EventArgs e)
        {
            Database.Device device = (Database.Device)fastObjectListView1.SelectedObject;
            new FClient(device.GetClient()).ShowDialog();
        }
    }
}
