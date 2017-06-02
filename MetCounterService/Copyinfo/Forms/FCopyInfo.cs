﻿using Copyinfo.Database;
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
        }

        private void FCopyInfo_Resize(object sender, EventArgs e)
        {
            GUI.CalculateHeight(cReports1, this, freeAtTop);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cReports1.FillList(Database.DAO.GetAllReports());
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
            PrintSelected();
        }

        private void PrintSelected()
        {
            //List<string> toPrint = new List<string>();

            //if (MessageBox.Show("Czy wydrukować tylko te dane, które nie były drukowane?", "Co drukujemy?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    foreach(MachineRecord rec in cReports1.fastObjectListView1.SelectedObjects)
            //    {
            //        toPrint.Add
            //    }

            //foreach(MachineRecord rec in cReports1.fastObjectListView1.SelectedObjects)
            //{
            //    toPrint.Add(rec.GetTextToPrint());
            //}

            //if (Other.Printing.Print(toPrint) != DialogResult.Cancel)
            //{
            //    if (toPrint.Count > 0)
            //        MessageBox.Show("Zlecono do wydruku: " + toPrint.Count.ToString() + " dokumentów.");
            //    else
            //        MessageBox.Show("Brak zaznaczonych rekordów.");
            //}
            List<MachineRecord> records = cReports1.fastObjectListView1.SelectedObjects.Cast<Database.MachineRecord>().ToList();
            Other.Printing.Print(records);
        }

        private void tbButton4_Click(object sender, EventArgs e)
        {
            new FTestingForm().Show();
        }

        private void FCopyInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz zamknąć główne okienko? Spowoduje to zamknięcie całego programu.", "Uwaga", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
