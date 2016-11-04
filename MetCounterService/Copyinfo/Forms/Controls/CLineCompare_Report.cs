using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;

namespace Copyinfo.Forms.Controls
{
    public partial class CLineCompare_Report : UserControl
    {

        private Database.MachineRecord record1, record2;

        public CLineCompare_Report()
        {
            InitializeComponent();
        }

        public CLineCompare_Report(Database.MachineRecord record1, Database.MachineRecord record2)
        {
            InitializeComponent();
            Compare(record1, record2);

        }

        public void Compare(Database.MachineRecord record1, Database.MachineRecord record2)
        {
            if (record1 < record2)
            {
                this.record1 = record1;
                this.record2 = record2;
            }
            else
            {
                this.record2 = record1;
                this.record1 = record2;
            }

            generate();
        }

        private void generate()
        {

            //TOTAL
            txtTotal1.Text = record1.getTotal().ToString();
            txtTotal2.Text = record2.getTotal().ToString();
            txtTotalDif.Text = (record2.getTotal() - record1.getTotal()).ToString();

            //SCAN
            txtScan1.Text = record1.scan_counter.ToString();
            txtScan2.Text = record2.scan_counter.ToString();
            txtScanDif.Text = (record2.scan_counter - record1.scan_counter).ToString();

            //Black and White
            txtBaW1.Text = record1.print_counter_black_and_white.ToString();
            txtBaW2.Text = record2.print_counter_black_and_white.ToString();
            txtBaWDiff.Text = (record2.print_counter_black_and_white - record1.print_counter_black_and_white).ToString();

            //Color
            txtColor1.Text = record1.print_counter_color.ToString();
            txtColor2.Text = record2.print_counter_color.ToString();
            txtColorDiff.Text = (record2.print_counter_color - record1.print_counter_color).ToString();

            //Set labels (text = datetime)
            lRecord1.Text = record1.datetime.ToString(Style.DateTimeFormat);
            lRecord2.Text = record2.datetime.ToString(Style.DateTimeFormat);

            //Bolding Dates
            monthCalendar1.AddBoldedDate(record1.datetime);
            monthCalendar1.AddBoldedDate(record2.datetime);

            //Chart Printer
            ChartBuilder builder = new ChartBuilder();
            Series s = builder.buildColumnSeries_total(record1, record2);
            chart1.Series.Add(s);

            //Chart Scan
            s = builder.buildColumnSeries_scan(record1, record2);
            chart2.Series.Add(s);
        }

    }
}
