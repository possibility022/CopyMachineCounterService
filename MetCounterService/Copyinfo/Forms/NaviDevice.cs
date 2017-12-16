using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z80NavBarControl.Z80NavBar;
using Z80NavBarControl.Z80NavBar.Themes;
using Copyinfo.Database;

namespace Copyinfo.Forms
{
    public partial class NaviDevice : NaviBar
    {
        Controls.DeviceControls.DeviceDetailsInfo ucInfo = new Forms.Controls.DeviceControls.DeviceDetailsInfo();
        Controls.DeviceControls.DeviceModelAndReports ucModel = new Forms.Controls.DeviceControls.DeviceModelAndReports();
        Controls.DeviceControls.DeviceReports ucReports = new Forms.Controls.DeviceControls.DeviceReports();

        Device device;

        public NaviDevice()
        {
            InitializeComponent();
        }

        public NaviDevice(Device device) : this()
        {
            this.device = device;
            AdjustControl(ucModel);
            AdjustControl(ucInfo);
            AdjustControl(ucReports);

            ucModel.VisibleChanged += UcModel_VisibleChanged;
            ucInfo.VisibleChanged += UcInfo_VisibleChanged;
            ucReports.VisibleChanged += UcReports_VisibleChanged;

            this.Resize += NaviDevice_Resize;

            InitNavibarItems();

            ucModel.Show();
        }

        private void NaviDevice_Resize(object sender, EventArgs e)
        {
            foreach(UserControl control in pagesToView)
            {
                if (control.Visible)
                {
                    control.Size = new Size(Width - NaviBarControl.Width - 20, Height);
                }
            }
        }

        private void UcReports_VisibleChanged(object sender, EventArgs e)
        {
            if (ucReports.Visible)
            {
                if (ucReports.cReports1.fastObjectListView1.Items.Count == 0)
                {
                    ucReports.cReports1.fastObjectListView1.Objects = device.GetRecords();
                    //ucReports.cReports1.fastObjectListView1.
                }
            }
        }

        private void UcInfo_VisibleChanged(object sender, EventArgs e)
        {
            if (ucInfo.Visible)
                ucInfo.SetDevice(device);
        }

        private void UcModel_VisibleChanged(object sender, EventArgs e)
        {
            if (ucModel.Visible == true)
            {
                ucModel.SetDevice(device);
            }
        }
    }
}
