using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;

namespace Copyinfo.Forms
{
    public partial class FCompareReports : Form
    {
        public FCompareReports()
        {
            InitializeComponent();
        }

        public FCompareReports(Database.MachineRecord[] records)
        {
            InitializeComponent();
            if (records.Length == 2)
                cLineCompare_Report1.Compare(records[0], records[1]);
        }

        private void setCompareLines(Database.MachineRecord[] records)
        {

        }
    }
}
