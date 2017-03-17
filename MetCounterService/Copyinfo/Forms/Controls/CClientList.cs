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
            tbTBClient.id = 0;
            tbTBNIP.id = 1;
            tbTBStreet.id = 2;
            tbTBCity.id = 3;
            tbTBPhone.id = 4;
            tbTBEmail.id = 5;
            tbTBNote.id = 6;
            tbTBServiceAgrement.id = 7;
        }

        private void tbButton1_Click(object sender, EventArgs e)
        {
            List<Database.Client> list = Database.DAO.GetAllClients();
            tbClientListView1.SetList(list);
        }

        private void tbButton2_Click(object sender, EventArgs e)
        {
            FClient client = new FClient();
            client.Show();
        }

        private void AlignTextBoxes()
        {
            GUI.AlignTextBoxes(
                this.tbClientListView1.GetColumnSizeHeaders(),
                new TextBox[] {
                    tbTBClient,
                    tbTBNIP,
                    tbTBStreet,
                    tbTBCity,
                    tbTBPhone,
                    tbTBEmail,
                    tbTBNote,
                    tbTBServiceAgrement}, 
                tbClientListView1.Location.Y - tbTBEmail.Size.Height, 0);
        }

        private void AlignListViewHeigh()
        {
            tbClientListView1.Height = this.Height - tbTBClient.Height - 5;
        }

        private void tbClientListView1_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            AlignTextBoxes();
        }

        private void CClientList_Resize(object sender, EventArgs e)
        {
            AlignTextBoxes();
            AlignListViewHeigh();
        }

        private void tbTBClient_TextChanged(object sender, EventArgs e)
        {
            TextBoxes.TBTextBox s = (TextBoxes.TBTextBox)sender;
            tbClientListView1.Filter(s.id, s.Text);
        }

        public List<Database.Client> GetSelectedClients()
        {
            List<Database.Client> clients = new List<Database.Client>();
            if (tbClientListView1.SelectedItems.Count > 0)
            {
                foreach(ListViewItem item in tbClientListView1.SelectedItems)
                {
                    Controls.ListView.TBListViewItem i = (Controls.ListView.TBListViewItem)item;
                    Database.Client client = (Database.Client)i.additionalItem;
                    clients.Add(client);
                }
            }
            return clients;
        }
    }
}
