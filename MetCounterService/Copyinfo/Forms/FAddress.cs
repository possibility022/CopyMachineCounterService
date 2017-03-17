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
    public partial class FAddress : Form
    {
        public FAddress()
        {
            InitializeComponent();
        }

        public FAddress(Database.Address address)
        {
            InitializeComponent();
            this.cAddress1.SetAddress(address);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
