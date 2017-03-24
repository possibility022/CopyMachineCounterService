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
            lvwColumnSorter.SetIntegers(new int[] { 1, 2, 3 });
            lvwColumnSorter.SetDates(new int[] { 4 });
            lvwColumnSorter.SetAdditionalItemClass(Copyinfo.Forms.Controls.ListView.TBListViewItem.AdditionalItemClassType.MachineRecord);
            
            //this.tbListView1.ListViewItemSorter = lvwColumnSorter;
            this.fastObjectListView1.MouseClick += listView1_MouseClick;
            //this.tbListView1.ColumnClick += listView1_ColumnClick;

            GUI.SetTextBoxAndFastListView(tbTextBox1, fastObjectListView1, this);
            Style.InitFastObjectListView(fastObjectListView1, tbTextBox1);
        }

        public void FillList(List<MachineRecord> records)
        {
            fastObjectListView1.SetObjects(records);
        }

        private void hTMLLicznikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MachineRecord record = (MachineRecord)fastObjectListView1.SelectedObject;
            string html = record.GetCounter().full_counter;

            new FHTMLView(html).ShowDialog();
        }

        private void hTMLNumerSeryjnyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MachineRecord record = (MachineRecord)fastObjectListView1.SelectedObject;
            string html = record.GetSerial().full_serialnumber;

            new FHTMLView(html).ShowDialog();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (fastObjectListView1.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    MachineRecord record = (MachineRecord)fastObjectListView1.SelectedObject;
                    if (record.IsParsedEmail())
                        contextMenuStrip1.Items[0].Enabled = true;
                    else
                        contextMenuStrip1.Items[0].Enabled = false;
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void dodajUrzadzenieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MachineRecord record = (MachineRecord)fastObjectListView1.SelectedObject;
            FAddDevice form_add = new FAddDevice(record.serial_number);
            form_add.Show();
        }

        private void porownajWybraneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Database.MachineRecord[] records = new Database.MachineRecord[fastObjectListView1.SelectedObjects.Count];

            for (int i = 0; i < records.Length; i++)
                records[i] = (MachineRecord) fastObjectListView1.SelectedObjects[i];

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
                foreach (MachineRecord rec in fastObjectListView1.SelectedObjects)
                    Database.DAO.DeleteMachineRecord(rec);
            }

        }

        private void emailMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MachineRecord record = (MachineRecord)fastObjectListView1.SelectedObject;
            string emailText = record.GetEmail().GetEmail();

            //new FHTMLView(html).ShowDialog();
            //new FRichTextView(html).ShowDialog();
            FEmailView view = new FEmailView();
            view.cEmailView1.SetText(emailText);
            view.cEmailView1.AddAttachments(record.GetEmail().GetAttachments());
            view.Show();
            
        }

        private void tbTBDate_MouseClick(object sender, MouseEventArgs e)
        {
            //Ta metoda pokazywała okienko z kalendarzem dzięki czemu można było filtrować datą. TODO zaimplementować to w object list view.

            //FCalendarWindow calWindow = new FCalendarWindow();
            //calWindow.Location = Cursor.Position;
            //calWindow.StartPosition = FormStartPosition.Manual;
            //calWindow.ShowDialog();

            //if (calWindow.dateSelected)
            //{
            //    tbTBDate.Text = calWindow.dateTimeSelectedSTART.ToString(Style.DateTimeFormat) + "-" + calWindow.dateTimeSelectedEND.ToString(Style.DateTimeFormat);
            //}
        }
    }
}
