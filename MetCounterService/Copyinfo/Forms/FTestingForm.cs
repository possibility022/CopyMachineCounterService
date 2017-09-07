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
            List<Music> music = new List<Music>();
            for(int i = 0; i < 20; i++)
            {
                music.Add(new Music { Name = "ABC" + i.ToString() , name_local = "abc"});
            }
            
            objectListView1.SetObjects(DAO.GetAllReports(MongoTB.RecordsCollection.Normal));
        }
    }

    public class Music
    {
        public string Name { get; set; }
        public string name_local { get; set; }
    }

}
