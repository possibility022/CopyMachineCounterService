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
    public partial class FClientList : Form
    {
        private int freeSpaceAtTop;

        public FClientList()
        {
            InitializeComponent();
            freeSpaceAtTop = this.Height - cClientList1.Height;
        }

        private void tbButton1_Click(object sender, EventArgs e)
        {
            cClientList1.fastObjectListView1.SetObjects(Database.DAO.GetAllClients());
        }

        private void FClientList_Resize(object sender, EventArgs e)
        {
            GUI.CalculateHeight(cClientList1, this, freeSpaceAtTop);
        }

        private void tbButton_Small1_Click(object sender, EventArgs e)
        {
            List<string> toPrint = new List<string>();

            foreach(Database.Client c in cClientList1.fastObjectListView1.SelectedObjects)
            {
                List<Database.Device> devices = c.GetDevices();
                foreach(Database.Device d in devices)
                {
                    Database.MachineRecord rec = d.GetOneRecord(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
                    if (rec != null)
                        toPrint.Add(rec.GetTextToPrint());
                }
            }

            Other.Printing.Print(toPrint);
        }

        private void FClientList_ResizeEnd(object sender, EventArgs e)
        {
            GUI.CalculateHeight(cClientList1, this, freeSpaceAtTop);
        }
    }
}
