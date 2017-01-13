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
    public partial class FCalendarWindow : Form
    {
        public bool dateSelected = false;
        public DateTime dateTimeSelectedSTART;
        public DateTime dateTimeSelectedEND;
        

        public FCalendarWindow()
        {
            InitializeComponent();
        }

        private void FCalendarWindow_MouseLeave(object sender, EventArgs e)
        {
            Close();
        }

        private void FCalendarWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            dateSelected = true;
            dateTimeSelectedSTART = monthCalendar1.SelectionStart;
            dateTimeSelectedEND = monthCalendar1.SelectionEnd;
            this.Close();
        }
    }
}
