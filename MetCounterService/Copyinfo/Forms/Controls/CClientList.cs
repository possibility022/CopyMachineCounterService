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
    public partial class CClientList : UserControl
    {
        public CClientList()
        {
            InitializeComponent();
            GUI.SetTextBoxAndFastListView(tbTextBox1, fastObjectListView1, this);
            Style.InitFastObjectListView(fastObjectListView1, tbTextBox1);
            SetConverters();
        }

        protected void SetConverters()
        {
            olvPhones.AspectToStringConverter = delegate (object x) {
                string[] phones = (string[])x;
                string fullString = "";
                foreach (string phone in phones)
                    fullString += phone + " ";
                return fullString;
            };
        }
    }
}
