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

        public FCopyInfo()
        {
            InitializeComponent();
            Global.Initialize();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cReports1.fillList(Global.database.getAllReports());
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
            printSelected();
        }

        private void printSelected()
        {
            List<MachineRecord> records = cReports1.getSelected();
            List<string> toPrint = new List<string>();

            for (int i = 0; i < records.Count; i ++)
            {
                if (records[i].isParsedEmail() == true)
                {
                    toPrint.Add(records[i].getEmail().getEmail());
                }
            }

            Other.Printing.print(toPrint);

            if (toPrint.Count > 0)
                MessageBox.Show("Zlecono do wydruku: " + records.Count.ToString() + " dokumentów.");
            else
                MessageBox.Show("Brak zaznaczonych rekordów.");
        }
    }
}
