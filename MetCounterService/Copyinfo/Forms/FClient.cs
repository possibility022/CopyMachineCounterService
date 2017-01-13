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
    public partial class FClient : Form
    {
        public FClient()
        {
            InitializeComponent();
        }

        public FClient(Database.Client client)
        {
            InitializeComponent();
            this.addClient1.setClient(client);
        }

        private void SaveClient()
        {
            Global.database.SaveClient(this.addClient1.getClient());
        }

        private void tbButton1_Click(object sender, EventArgs e)
        {
            SaveClient();
            this.Close();
        }

        private void tbButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
