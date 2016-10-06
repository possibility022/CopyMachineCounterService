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
    public partial class FAddDevice : Form
    {
        public FAddDevice()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Database.Device d = cAddDevice1.getDevice();
            if (d == null)
            {
                MessageBox.Show("Źle wypełnione dane");
            }
            else
            {
                Global.database.SaveDevice(d);
                this.Close();
            }
        }
    }
}
