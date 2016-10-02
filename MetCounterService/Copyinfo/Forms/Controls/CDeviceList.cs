using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Copyinfo.Forms.Controls
{
    public partial class CDeviceList : UserControl
    {
        List<Database.Device> currentList;

        public CDeviceList()
        {
            InitializeComponent();
        }

        public void loadList(List<Database.Device> list)
        {
            currentList = list;
            listView2.Items.Clear();
            addToList(currentList);
        }

        public void addToList(List<Database.Device> list)
        {
            ListViewItem item;
            string[] values = { };
            foreach (Database.Device d in list)
            {
                values = new string[]{ d.provider, d.model, d.serial_number, d.getAddress().street, d.instalation_datetime.ToShortDateString()};
                item = new ListViewItem(values);
                listView2.Items.Add(item);
            }
        }
    }
}
