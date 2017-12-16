using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware.Design;
using Copyinfo.Database;

namespace Copyinfo.Forms
{
    public partial class FTestingForm : Form
    {
        public FTestingForm()
        {
            InitializeComponent();
            
        }

        private void tbButton1_Click(object sender, EventArgs e)
        {

        }

        public void test(Database.MachineRecord rec)
        {
            cRecordMongoDetails1.GenerateText(rec);
        }
    }
}
