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
        enum ViewMode
        {
            Simple,
            WithClientsName
        }

        ViewMode viewMode = ViewMode.WithClientsName;

        public CReports()
        {
            InitializeComponent();
            //this.tbListView1.ListViewItemSorter = lvwColumnSorter;
            this.fastObjectListView1.MouseClick += listView1_MouseClick;
            //this.tbListView1.ColumnClick += listView1_ColumnClick;

            GUI.SetTextBoxAndFastListView(tbTextBox1, fastObjectListView1, this);
            Style.InitFastObjectListView(fastObjectListView1, tbTextBox1);

            this.olvDateTime.AspectToStringConverter = delegate (object x) {
                DateTime date = (DateTime)x;
                return date.ToString(Style.DateTimeFormat);
            };
        }

        public void FillList(List<MachineRecord> records)
        {
            fastObjectListView1.SetObjects(records);
            fastObjectListView1.Sort(olvDateTime, SortOrder.Descending);
        }

        private void HtmlLicznikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MachineRecord record = (MachineRecord)fastObjectListView1.SelectedObjects[0];
            string html = record.GetCounter().full_counter;

            new FHTMLView(html).ShowDialog();
        }

        private void HtmlNumerSeryjnyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MachineRecord record = (MachineRecord)fastObjectListView1.SelectedObjects[0];
            string html = record.GetSerial().full_serialnumber;

            new FHTMLView(html).ShowDialog();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (fastObjectListView1.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    MachineRecord record = (MachineRecord)fastObjectListView1.SelectedObjects[0];
                    if (record.IsParsedEmail())
                    {
                        contextMenuStrip1.Items["emailMessageToolStripMenuItem"].Enabled = true;
                        contextMenuStrip1.Items["hTMLLicznikToolStripMenuItem"].Enabled = false;
                        contextMenuStrip1.Items["hTMLNumerSeryjnyToolStripMenuItem"].Enabled = false;
                    }
                    else
                    {
                        contextMenuStrip1.Items["emailMessageToolStripMenuItem"].Enabled = false;
                        contextMenuStrip1.Items["hTMLLicznikToolStripMenuItem"].Enabled = true;
                        contextMenuStrip1.Items["hTMLNumerSeryjnyToolStripMenuItem"].Enabled = true;
                    }
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void dodajUrzadzenieToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
            MachineRecord record = (MachineRecord)fastObjectListView1.SelectedObjects[0];
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

        private void showDevice_Click(object sender, EventArgs e)
        {
            MachineRecord record = (MachineRecord)fastObjectListView1.SelectedObjects[0];
            record.ShowClient();
        }

        private void showDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MachineRecord record = (MachineRecord)fastObjectListView1.SelectedObjects[0];
            new FDeviceView(record.GetDevice()).Show() ;
        }

        public void SwitchViewMode()
        {
            if (viewMode == ViewMode.Simple)
                viewMode = ViewMode.WithClientsName;
            else if (viewMode == ViewMode.WithClientsName)
                viewMode = ViewMode.Simple;

            ApplyViewMode();
        }

        private void ApplyViewMode()
        {
            if (viewMode == ViewMode.Simple)
            {
                olvClientName.IsVisible = false;
                olvAddress.IsVisible = false;
                olvDeviceModel.IsVisible = false;
                olvSerialNumber.IsVisible = false;
                fastObjectListView1.RebuildColumns();
            }
            else if(viewMode == ViewMode.WithClientsName)
            {
                olvAddress.IsVisible = true;
                olvClientName.IsVisible = true;
                olvDeviceModel.IsVisible = true;
                olvSerialNumber.IsVisible = true;
                fastObjectListView1.RebuildColumns();
            }
        }
    }
}
