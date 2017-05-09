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
    public partial class CClientList : UserControl
    {
        public CClientList()
        {
            InitializeComponent();
            GUI.SetTextBoxAndFastListView(tbTextBox1, fastObjectListView1, this);
            Style.InitFastObjectListView(fastObjectListView1, tbTextBox1);
            SetConverters();
            SetContextMenu();
        }

        protected void SetConverters()
        {
            olvPhones.AspectToStringConverter = delegate (object x) {
                string[] phones = (string[])x;
                string fullString = "";
                foreach (string phone in phones)
                    fullString += phone + " ";
                return fullString;
            };
        }

        protected void SetContextMenu()
        {
            var item = contextMenuStrip1.Items.Add("Szczegóły");
            item.Name = "Details";
            item.Click += ShowClientDetails;

            item = contextMenuStrip1.Items.Add("Pokaż Raporty");
            item.Name = "Show Reports";
            item.Click += ShowReports;

            item = contextMenuStrip1.Items.Add("Wyszukaj w mapach Google");
            item.Click += SearchInGoogleMaps;    

            fastObjectListView1.MouseClick += FastObjectListView1_MouseClick;
        }

        private void SearchInGoogleMaps(object sender, EventArgs e)
        {
            if (fastObjectListView1.SelectedObjects.Count > 0)
            {
                Database.Client client = (Database.Client)fastObjectListView1.SelectedObjects[0];
                client.GetAddress().SearchInGoogleMaps();
            }
        }

        private void ShowClientDetails(object sender, EventArgs e)
        {
            new FClient((Database.Client)fastObjectListView1.SelectedObjects[0]).Show();
        }

        private void FastObjectListView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void ShowReports(object sender, EventArgs e)
        {
            System.Collections.ArrayList list = (System.Collections.ArrayList)fastObjectListView1.SelectedObjects;
            new FReports(fastObjectListView1.SelectedObjects).Show();
        }
    }
}
