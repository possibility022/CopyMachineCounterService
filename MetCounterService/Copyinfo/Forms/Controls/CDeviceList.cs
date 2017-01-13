﻿using System;
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
        public CDeviceList()
        {
            InitializeComponent();
            tbTBProvider.id = 0;
            tbTBModel.id = 1;
            tbTBSerialNumber.id = 2;
            tbTBAddress.id = 3;
            tbTBData.id = 4;
//            tbListView1.setColumnsWithDate(new int[] { 4 });
        }

        public void loadList(List<Database.Device> list)
        {
            tbListView1.Items.Clear();
            addToList(list);
        }

        public void addToList(List<Database.Device> list)
        {
            List<Database.Device> devices = new List<Database.Device>(list);
            ListViewItem item;
            string[] values = { };
            for (int i = 0; i < devices.Count; i++)
            {
                //foreach (Database.Device d in list)
                Database.Device d = devices[i];
                values = new string[]{ d.provider, d.model, d.id, d.getAddress().street, d.instalation_datetime.ToShortDateString()};
                item = new TBListViewItem(values, d);
                tbListView1.Items.Add(item);
            }
        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (tbListView1.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void showReportsForThisDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TBListViewItem item = (TBListViewItem)tbListView1.SelectedItems[0];
            Database.Device device = (Database.Device)item.additionalItem;
            new FReports(Global.database.getReports(device.id)).Show();
        }

        public void DeleteSelectedDevices()
        {
            List<ListViewItem> toRemove = new List<ListViewItem>();
            DialogResult r = MessageBox.Show("Czy napewno chcesz usunąć te urządzenie / urządzenia?", "Uwaga!", MessageBoxButtons.YesNoCancel);
            if (r == DialogResult.Yes)
            {
                for (int i = 0; i < tbListView1.SelectedItems.Count; i++)
                {
                    TBListViewItem item = (TBListViewItem)tbListView1.SelectedItems[i];
                    bool result = Global.database.DeleteDevice((Database.Device) item.additionalItem);
                    if(result)
                    {
                        toRemove.Add(item);
                    }
                }
            }

            foreach(ListViewItem i in toRemove)
            {
                tbListView1.Items.Remove(i);
            }
        }

        private void alignTextBoxesAndListView()
        {
            tbListView1.Height = this.Height - tbTBAddress.Height - 2;
            GUI.AlignTextBoxes(
                this.tbListView1.getColumnSizeHeaders(),
                new TextBox[] {
                    tbTBProvider,
                    tbTBModel,
                    tbTBSerialNumber,
                    tbTBAddress,
                    tbTBData},
                tbListView1.Location.Y - tbTBProvider.Size.Height, 0);
        }

        private void listView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            alignTextBoxesAndListView();
        }

        private void tbTBProvider_TextChanged(object sender, EventArgs e)
        {
            TextBoxes.TBTextBox s = (TextBoxes.TBTextBox)sender;
            tbListView1.filter(s.id, s.Text);
        }

        private void CDeviceList_Resize(object sender, EventArgs e)
        {
            alignTextBoxesAndListView();
        }
    }
}