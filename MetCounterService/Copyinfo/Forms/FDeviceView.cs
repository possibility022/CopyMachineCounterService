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
    public partial class FDeviceView : Form
    {
        Database.Device device;

        public FDeviceView()
        {
            InitializeComponent();
        }

        public FDeviceView(Database.Device device)
        {
            this.device = device;
            InitializeComponent();
            cDevice1.SetDevice(device);
        }
    }
}
