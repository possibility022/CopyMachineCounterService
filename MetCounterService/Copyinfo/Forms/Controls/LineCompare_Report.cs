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
    public partial class LineCompare_Report : UserControl
    {

        public LineCompare_Report()
        {
            InitializeComponent();
        }

        public LineCompare_Report(Database.MachineRecord record, Database.MachineRecord compared_to)
        {
            InitializeComponent();

            int total = record.print_counter_black_and_white + record.print_counter_color;
            this.txtTotal.Text = (record.print_counter_black_and_white + record.print_counter_color).ToString();
            this.txtScan.Text = record.scan_counter.ToString();

            int total_compared = (compared_to.print_counter_black_and_white + compared_to.print_counter_color) - total;

            this.txtDiffrence.Text = total_compared.ToString();

            int scanDiffrent = compared_to.scan_counter - record.scan_counter;
            this.txtDiffrenceCounter.Text = scanDiffrent.ToString();
        }
    }
}
