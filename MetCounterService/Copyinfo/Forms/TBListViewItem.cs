using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copyinfo.Forms
{
    class TBListViewItem_MachineRecord : System.Windows.Forms.ListViewItem
    {
        public Database.MachineRecord record { get; }

        public TBListViewItem_MachineRecord(string[] items, Database.MachineRecord record) : base(items)
        {
            this.record = record;
        }
    }

    class TBListViewItem_Device : System.Windows.Forms.ListViewItem
    {
        public Database.Device device { get; }

        public TBListViewItem_Device(string[] items, Database.Device device) : base(items)
        {
            this.device = device;
        }
    }
}
