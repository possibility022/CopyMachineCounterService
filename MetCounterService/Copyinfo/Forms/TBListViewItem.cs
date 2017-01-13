using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copyinfo.Forms
{
    //class TBListViewItem_MachineRecord : System.Windows.Forms.ListViewItem
    //{
    //    public Database.MachineRecord record { get; }

    //    public TBListViewItem_MachineRecord() { }

    //    public TBListViewItem_MachineRecord(string[] items, Database.MachineRecord record) : base(items)
    //    {
    //        this.record = record;
    //    }
    //}

    //class TBListViewItem_Device : System.Windows.Forms.ListViewItem
    //{
    //    public Database.Device device { get; }

    //    public TBListViewItem_Device() { }

    //    public TBListViewItem_Device(string[] items, Database.Device device) : base(items)
    //    {
    //        this.device = device;
    //    }
    //}

    public class TBListViewItem : System.Windows.Forms.ListViewItem
    {
        public enum AdditionalItemClassType
        {
            None,
            Device,
            MachineRecord
        }

        public object additionalItem { get; set; }

        public TBListViewItem() { }

        public TBListViewItem(string[] items, object additionalItem) : base(items)
        {
            this.additionalItem = additionalItem;
        }

        public override object Clone()
        {
            object clone = base.Clone();
            TBListViewItem item = (TBListViewItem)clone;
            item.additionalItem = this.additionalItem;
            return item;
        }
    }
}
