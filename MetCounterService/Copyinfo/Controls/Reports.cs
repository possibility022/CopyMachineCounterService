using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Copyinfo.Controls
{
    public partial class Reports : UserControl
    {
        public Reports()
        {
            InitializeComponent();
        }

        public void fillList(List<MachineRecord> machines)
        {
            this.listView1.Items.Clear();
            foreach(MachineRecord m in machines)
            {
                ListViewItem item = new ListViewItem(new string[] {
                    m.SerialNumber,
                    m.PrintCounterBlackAndWhite.ToString(),
                    m.PrintCounterColor.ToString(),
                    m.ScanCounter.ToString(),
                    m.DateTime.ToString()
                    });

                listView1.Items.Add(item);
            }
        }
    }
}
