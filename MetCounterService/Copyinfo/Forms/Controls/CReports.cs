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
        public CReports()
        {
            InitializeComponent();
            ListViewSorter lvwColumnSorter = new ListViewSorter();
            lvwColumnSorter = new ListViewSorter();
            lvwColumnSorter.setIntegers(new int[] { 1, 2, 3 });
            lvwColumnSorter.setDates(new int[] { 4 });
            lvwColumnSorter.setAdditionalItemClass(TBListViewItem.AdditionalItemClassType.MachineRecord);
            this.tbListView1.setSorter(lvwColumnSorter);
            //this.tbListView1.ListViewItemSorter = lvwColumnSorter;
            this.tbListView1.MouseClick += listView1_MouseClick;
            //this.tbListView1.ColumnClick += listView1_ColumnClick;

            tbListView1.setColumnsWithDate(new int[] { 4 });
            tbListView1.setAdditionalClass(TBListViewItem.AdditionalItemClassType.MachineRecord);

            tbTBSerialNumber.id = 0;
            tbTBBandW.id = 1;
            tbTBColor.id = 2;
            tbTBScan.id = 3;
            tbTBDate.id = 4;
            tbTBToner_k.id = 5;
            tbTBToner_c.id = 6;
            tbTBToner_m.id = 7;
            tbTBToner_y.id = 8;
        }

        public void fillList(List<MachineRecord> machines)
        {
            this.tbListView1.Items.Clear();
            foreach(MachineRecord m in machines)
            {
                TBListViewItem item = new TBListViewItem(new string[] {
                    m.serial_number,
                    m.print_counter_black_and_white.ToString(),
                    m.print_counter_color.ToString(),
                    m.scan_counter.ToString(),
                    m.datetime.ToString(),
                    m.tonerlevel_k,
                    m.tonerlevel_c,
                    m.tonerlevel_m,
                    m.tonerlevel_y
                    },
                    m
                    );

                tbListView1.Items.Add(item);
            }
        }

        private void hTMLLicznikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TBListViewItem item = (TBListViewItem)tbListView1.SelectedItems[0];
            MachineRecord record = (MachineRecord)item.additionalItem;
            string html = record.getCounter().full_counter;

            new FHTMLView(html).ShowDialog();
        }

        private void hTMLNumerSeryjnyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TBListViewItem item = (TBListViewItem)tbListView1.SelectedItems[0];
            MachineRecord record = (MachineRecord)item.additionalItem;
            string html = record.getSerial().full_serialnumber;

            new FHTMLView(html).ShowDialog();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (tbListView1.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    TBListViewItem item = (TBListViewItem)tbListView1.SelectedItems[0];
                    MachineRecord record = (MachineRecord)item.additionalItem;
                    if (record.isParsedEmail())
                        contextMenuStrip1.Items[0].Enabled = true;
                    else
                        contextMenuStrip1.Items[0].Enabled = false;
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void dodajUrzadzenieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TBListViewItem item = (TBListViewItem)tbListView1.SelectedItems[0];
            MachineRecord record = (MachineRecord)item.additionalItem;
            FAddDevice form_add = new FAddDevice(record.serial_number);
            form_add.Show();
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //if (e.Column == lvwColumnSorter.SortColumn)
            //{
            //    // Reverse the current sort direction for this column.
            //    if (lvwColumnSorter.Order == SortOrder.Ascending)
            //    {
            //        lvwColumnSorter.Order = SortOrder.Descending;
            //    }
            //    else
            //    {
            //        lvwColumnSorter.Order = SortOrder.Ascending;
            //    }
            //}
            //else
            //{
            //    // Set the column number that is to be sorted; default to ascending.
            //    lvwColumnSorter.SortColumn = e.Column;
            //    lvwColumnSorter.Order = SortOrder.Ascending;
            //}

            //// Perform the sort with these new sort options.
            //this.tbListView1.Sort();
        }

        private void porownajWybraneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Database.MachineRecord[] records = new Database.MachineRecord[tbListView1.SelectedItems.Count];
            
            for (int i = 0; i < tbListView1.SelectedItems.Count; i++)
            {
                TBListViewItem item = (TBListViewItem)tbListView1.SelectedItems[i];
                records[i] = (MachineRecord)item.additionalItem;
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
                foreach (TBListViewItem item in tbListView1.SelectedItems)
                    Global.database.DeleteMachineRecord((MachineRecord)item.additionalItem);
            }

        }

        private void alignTextBoxes()
        {
            GUI.AlignTextBoxes(
                this.tbListView1.getColumnSizeHeaders(),
                new TextBox[] {
                    tbTBSerialNumber,
                    tbTBBandW,
                    tbTBColor,
                    tbTBScan,
                    tbTBDate,
                    tbTBToner_k,
                    tbTBToner_c,
                    tbTBToner_m,
                    tbTBToner_y},
                tbListView1.Location.Y - tbTBSerialNumber.Size.Height, 0);
        }

        private void alignListViewHeight()
        {
            this.tbListView1.Height = this.Size.Height - tbTBBandW.Height;
        }

        private void emailMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TBListViewItem item = (TBListViewItem)tbListView1.SelectedItems[0];
            MachineRecord record = (MachineRecord)item.additionalItem;
            string emailText = record.getEmail().getEmail();

            //new FHTMLView(html).ShowDialog();
            //new FRichTextView(html).ShowDialog();
            FEmailView view = new FEmailView();
            view.cEmailView1.setText(emailText);
            view.cEmailView1.addAttachments(record.getEmail().getAttachments());
            view.Show();
            
        }

        private void tbTBDate_TextChanged(object sender, EventArgs e)
        {
            TextBoxes.TBTextBox s = (TextBoxes.TBTextBox)sender;
            tbListView1.filter(s.id, s.Text);
        }

        private void tbListView1_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            alignTextBoxes();
        }

        private void CReports_Resize(object sender, EventArgs e)
        {
            alignTextBoxes();
            alignListViewHeight();
        }

        private void tbTBDate_MouseClick(object sender, MouseEventArgs e)
        {
            FCalendarWindow calWindow = new FCalendarWindow();
            calWindow.Location = Cursor.Position;
            calWindow.StartPosition = FormStartPosition.Manual;
            calWindow.ShowDialog();

            if (calWindow.dateSelected)
            {
                tbTBDate.Text = calWindow.dateTimeSelectedSTART.ToString(Style.DateTimeFormat) + "-" + calWindow.dateTimeSelectedEND.ToString(Style.DateTimeFormat);
            }
        }
    }
}
