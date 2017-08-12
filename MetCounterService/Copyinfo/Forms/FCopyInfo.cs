using Copyinfo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Copyinfo.Forms
{
    public partial class FCopyInfo : Form
    {
        static FDevicesView f_machines;
        static int freeAtTop;

        public FCopyInfo()
        {
            InitializeComponent();
            Init();
            Global.Initialize();
            button1_Click(null, null);
        }

        protected void Init()
        {
            this.Resize += FCopyInfo_Resize;
            freeAtTop = this.Height - cReports1.Height;
            cReports1.fastObjectListView1.UseCellFormatEvents = true;

            //Inicjalizacja przycisku Drukuj z drop down menu

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripItem item = contextMenu.Items.Add("Drukuj zestawienie dla tego miesiąca");
            item.Click += PrintMonthCounters;

            item = contextMenu.Items.Add("Drukuj wybrane recordy");
            item.Click += PrintSelectedItems;

            tbButtonDropMenu1.Menu = contextMenu;

        }

        private void PrintSelectedItems(object sender, EventArgs e)
        {
            List<MachineRecord> records = cReports1.fastObjectListView1.SelectedObjects.Cast<Database.MachineRecord>().ToList();
            Other.Printing.Print(records);
        }

        private void PrintMonthCounters(object sender, EventArgs e)
        {
            tbButtonDropMenu1.SetLoadingState();
            Other.Printing.PrintThisMonthReportBackground(new Action(() => tbButtonDropMenu1.SetNormalState() ));
        }

        private void FCopyInfo_Resize(object sender, EventArgs e)
        {
            GUI.CalculateHeight(cReports1, this, freeAtTop);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tbBtnRefresh.SetLoadingState();
            FillListAsync(new Action(() => tbBtnRefresh.SetNormalState()));
        }

        private void btnDevices_Click(object sender, EventArgs e)
        {
            f_machines = new FDevicesView();
            f_machines.Show();
        }

        private void tbButton3_Click(object sender, EventArgs e)
        {
            FClientList clientWindows = new FClientList();
            clientWindows.Show();
        }

        private void tbButton_Small1_Click(object sender, EventArgs e)
        {
            //PrintSelected();
        }

        private void tbButton4_Click(object sender, EventArgs e)
        {
            FTestingForm test = new FTestingForm();
            test.Show();
            MachineRecord record = (MachineRecord)cReports1.fastObjectListView1.SelectedObjects[0];
            test.test(record);
        }

        private void FCopyInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz zamknąć główne okienko? Spowoduje to zamknięcie całego programu.", "Uwaga", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private async void FillListAsync(Action doWhenFinish)
        {
            List<Database.MachineRecord> records = await DAO.GetAllReportsAsync();
            cReports1.FillList(records);

            doWhenFinish();
        }
    }
}
