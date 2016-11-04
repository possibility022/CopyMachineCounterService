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

        public FAddDevice(string device_serial_number)
        {
            InitializeComponent();
            this.cAddDevice1.setSerialnumber(device_serial_number);
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
                if (Global.database.SaveDevice(d) == new MongoDB.Bson.ObjectId())
                    MessageBox.Show("Operacja zapisywania nie powiodła się. Sprawdz czy urządzenie nie jest już dodane");
                else
                    this.Close();
            }
        }
    }
}
