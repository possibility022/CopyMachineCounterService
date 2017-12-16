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
    public partial class FDatabaseInfo : Form
    {
        public FDatabaseInfo()
        {
            InitializeComponent();
        }

        public void LoadDetails(object rec)
        {
            cRecordMongoDetails1.GenerateText(rec);
        }
    }
}
