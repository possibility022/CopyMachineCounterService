using System;
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
    public partial class FClientList : Form
    {
        public FClientList()
        {
            InitializeComponent();
        }

        private void tbButton1_Click(object sender, EventArgs e)
        {
            List<Database.Client> list = Global.database.getAllClients();
            cClientList1.tbClientListView1.setList(list);
        }

        private void tbButton2_Click(object sender, EventArgs e)
        {
            FClient client = new FClient();
            client.Show();
        }
    }
}
