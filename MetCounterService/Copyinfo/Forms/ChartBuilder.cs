using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms.DataVisualization.Charting;

namespace Copyinfo.Forms
{
    class ChartBuilder
    {
        private List<List<Database.MachineRecord>> devices = new List<List<Database.MachineRecord>>();
        private List<Series> series = new List<Series>();
        private List<string> inList = new List<string>();
        private Chart chart = new Chart();

        int max = 0;
        int min = 0;

        Database.MachineRecordComparer sorter = new Database.MachineRecordComparer();

        public ChartBuilder()
        {

        }

        public ChartBuilder(Database.MachineRecord[] records)
        {
            SortDevices(records);
            SortReports();
            BuildSeries();
        }

        public Chart GetChart()
        {
            return chart;
        }

        public List<Series> GetSeriesPrinterTotal()
        {
            return series;
        }

        public int getMax() { return max; }
        public int getMin() { return min; }

        private void SortDevices(Database.MachineRecord[] records)
        {
            foreach (Database.MachineRecord record in records)
            {
                if (inList.Contains(record.serialnumber))
                {
                    int listIndex = inList.IndexOf(record.serialnumber);
                    devices[listIndex].Add(record);
                }
                else
                {
                    inList.Add(record.serialnumber);
                    List<Database.MachineRecord> list = new List<Database.MachineRecord>();
                    list.Add(record);
                    devices.Add(list);
                }
            }
        }

        private void SortReports()
        {
            for(int i = 0; i < devices.Count; i++)
                devices[i].Sort(this.sorter);
        }

        public Series BuildColumnSeries_scan(Database.MachineRecord record1, Database.MachineRecord record2)
        {
            return BuildTwoColumns(record1.datetime, record1.scanCounter, record2.datetime, record2.scanCounter);
        }

        public Series BuildColumnSeries_total(Database.MachineRecord record1, Database.MachineRecord record2)
        {
            return BuildTwoColumns(record1.datetime, record1.GetTotal(), record2.datetime, record2.GetTotal());
        }

        private Series BuildTwoColumns(DateTime a, int value_a, DateTime b, int value_b)
        {
            Series s = new Series();
            s.XValueType = ChartValueType.Date;
            s.Points.AddXY(a.ToOADate(), value_a);
            s.Points.AddXY(b.ToOADate(), value_b);
            s.ChartType = Style.chartType_Duo;
            return s;
        }

        private void BuildSeries()
        {
            for (int i = 0; i < devices.Count; i++)
            {
                Series seria = new Series(inList[i]);
                int minimum = 99999999;
                int maximum = 0;
                for (int report = 0; report < devices[i].Count; report++)
                {
                    int total = devices[i][report].GetTotal();
                    if (minimum > total)
                        minimum = total;
                    if (maximum < total)
                        maximum = total;
                    seria.Points.AddXY(devices[i][report].datetime.ToOADate(), total);
                }

                //Obniżanie progu o 10% amplitudy
                minimum = minimum - ((maximum - minimum) * 10 / 100);

                max = maximum;
                min = minimum;

                if (seria.Points.Count > 2)
                    seria.ChartType = Style.chartType_Multi;
                else
                    seria.ChartType = Style.chartType_Duo;

                seria.XValueType = ChartValueType.Date;
                seria.BorderWidth = 2;
                series.Add(seria);
                //chart.Series.Add(seria);
                //chart.ChartAreas[0].AxisY.Minimum = minimum;
            }
        }
        
    }
}
