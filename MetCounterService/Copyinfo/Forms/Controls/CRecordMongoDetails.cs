using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Copyinfo.Database;
using System.Reflection;
using Copyinfo.Forms.Controls.Labels;

namespace Copyinfo.Forms.Controls
{
    public partial class CRecordMongoDetails : UserControl
    {
        public CRecordMongoDetails()
        {
            InitializeComponent();
        }

        public void GenerateText(object obj)
        {
            MachineRecord rec = obj as MachineRecord;

            if (rec != null)
            {
                PropertyInfo[] properties = rec.GetType().GetProperties();
                int index = 0;
                int lineSpace = 30;
                int horizontalSpace = 300;
                foreach (PropertyInfo info in properties)
                {
                    Controls.Add(
                        new TBLabel {
                            CopyOn = true,
                            Text = info.Name,
                            Location = new Point(10, (index * lineSpace) + 10),
                            Size = new Size(horizontalSpace, lineSpace)
                        });

                    string text = "null";
                    string type = "";
                    if (info.GetValue(rec) != null)
                    {
                        text = info.GetValue(rec).ToString();
                        type = info.GetValue(rec).GetType().ToString();
                    }

                    Controls.Add(
                        new TBLabel {
                            CopyOn = true,
                            Text = text,
                            Location = new Point(horizontalSpace + 10, (index * lineSpace) + 10),
                            Size = new Size(horizontalSpace, lineSpace)
                        });


                    Controls.Add(
                        new TBLabel
                        {
                            CopyOn = true,
                            Text = type,
                            Location = new Point(horizontalSpace * 2 + 10, (index * lineSpace) + 10),
                            Size = new Size(horizontalSpace, lineSpace)
                        });

                    index += 1;
                }
            }
        }
    }
}
