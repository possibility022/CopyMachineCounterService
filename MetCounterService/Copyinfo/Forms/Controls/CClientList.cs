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
            List<Database.Client> list = Database.DAO.getAllClients();
            tbClientListView1.setList(list);
        }

        private void tbButton2_Click(object sender, EventArgs e)
        {
            FClient client = new FClient();
            client.Show();
        }

        private void alignTextBoxes()
        {
            GUI.AlignTextBoxes(
                this.tbClientListView1.getColumnSizeHeaders(),
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

        private void alignListViewHeigh()
        {
            tbClientListView1.Height = this.Height - tbTBClient.Height - 5;
        }

        private void tbClientListView1_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            alignTextBoxes();
        }

        private void CClientList_Resize(object sender, EventArgs e)
        {
            alignTextBoxes();
            alignListViewHeigh();
        }

        private void tbTBClient_TextChanged(object sender, EventArgs e)
        {
            TextBoxes.TBTextBox s = (TextBoxes.TBTextBox)sender;
            tbClientListView1.filter(s.id, s.Text);
        }
    }
}
