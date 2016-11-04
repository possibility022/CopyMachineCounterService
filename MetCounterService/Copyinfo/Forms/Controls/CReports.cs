using System;
using System.Collections;
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
        private ListViewSorter lvwColumnSorter;

        public CReports()
        {
            InitializeComponent();
            lvwColumnSorter = new ListViewSorter();
            lvwColumnSorter.setIntegers(new int[] { 1, 2, 3 });
            this.listView1.ListViewItemSorter = lvwColumnSorter;
        }

        public void fillList(List<MachineRecord> machines)
        {
            this.listView1.Items.Clear();
            foreach(MachineRecord m in machines)
            {
                TBListViewItem_MachineRecord item = new TBListViewItem_MachineRecord(new string[] {
                    m.serial_number,
                    m.print_counter_black_and_white.ToString(),
                    m.print_counter_color.ToString(),
                    m.scan_counter.ToString(),
                    m.datetime.ToString()
                    },
                    m
                    );

                listView1.Items.Add(item);
            }
        }

        private void hTMLLicznikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TBListViewItem_MachineRecord item = (TBListViewItem_MachineRecord)listView1.SelectedItems[0];
            MachineRecord record = item.record;
            string html = record.getCounter().full_counter;

            new FHTMLView(html).ShowDialog();
        }

        private void hTMLNumerSeryjnyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TBListViewItem_MachineRecord item = (TBListViewItem_MachineRecord)listView1.SelectedItems[0];
            MachineRecord record = item.record;
            string html = record.getSerial().full_serialnumber;

            new FHTMLView(html).ShowDialog();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView1.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    TBListViewItem_MachineRecord item = (TBListViewItem_MachineRecord)listView1.SelectedItems[0];
                    if (item.record.isParsedEmail())
                        contextMenuStrip1.Items[0].Enabled = true;
                    else
                        contextMenuStrip1.Items[0].Enabled = false;
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void dodajUrzadzenieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TBListViewItem_MachineRecord item = (TBListViewItem_MachineRecord)listView1.SelectedItems[0];
            MachineRecord record = item.record;
            FAddDevice form_add = new FAddDevice(record.serial_number);
            form_add.Show();
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listView1.Sort();
        }

        private void porownajWybraneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Database.MachineRecord[] records = new Database.MachineRecord[listView1.SelectedItems.Count];
            
            for (int i = 0; i < listView1.SelectedItems.Count; i++)
            {
                TBListViewItem_MachineRecord item = (TBListViewItem_MachineRecord) listView1.SelectedItems[i];
                records[i] = item.record;
            }

            FCompareReports compareWindow = new FCompareReports(records);
            compareWindow.Show();
        }

        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            //e.Item.BackColor = e.Item.Index % 2 == 0 ? Color.LightCyan : Color.LightBlue;
        }

        private void usunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes ==
                MessageBox.Show("Czy aby na pewno? Hmmm???", "WARNING", MessageBoxButtons.YesNo))
            {
                foreach (TBListViewItem_MachineRecord record in listView1.SelectedItems)
                    Global.database.DeleteMachineRecord(record.record);
            }

        }

        private void emailMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TBListViewItem_MachineRecord item = (TBListViewItem_MachineRecord)listView1.SelectedItems[0];
            MachineRecord record = item.record;
            string html = record.getEmail().getEmail();

            new FHTMLView(html).ShowDialog();
        }
    }
}
