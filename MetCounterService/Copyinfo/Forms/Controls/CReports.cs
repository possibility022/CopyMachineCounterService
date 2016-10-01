using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Copyinfo.Database;

namespace Copyinfo.Forms.Controls
{
    public partial class CReports : UserControl
    {
        public CReports()
        {
            InitializeComponent();
        }

        public void fillList(List<MachineRecord> machines)
        {
            this.listView1.Items.Clear();
            foreach(MachineRecord m in machines)
            {
                ListViewItem item = new ListViewItem(new string[] {
                    m.serial_number,
                    m.print_counter_black_and_white.ToString(),
                    m.print_counter_color.ToString(),
                    m.scan_counter.ToString(),
                    m.datetime.ToString()
                    });

                listView1.Items.Add(item);
            }
        }
    }
}
