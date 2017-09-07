using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z80NavBarControl.Z80NavBar;
using Z80NavBarControl.Z80NavBar.Themes;

namespace Copyinfo.Forms
{
    public partial class FDeviceNavibar : Form
    {
        public FDeviceNavibar()
        {
            InitializeComponent();
            List<NavBarItem> demoItems = new List<NavBarItem>
                {
                new NavBarItem {ID = 1, Text = "Item1"},
                new NavBarItem {ID = 2, Text = "Item2"},
                new NavBarItem {ID = 3, Text = "Item3"}
                };
            z80_Navigation1.Initialize(demoItems, new ThemeSelector(Theme.RoyalBlue).CurrentTheme);
            z80_Navigation1.SelectedItem += Z80_Navigation1_SelectedItem;
        }

        private void Z80_Navigation1_SelectedItem(NavBarItem item)
        {
            throw new NotImplementedException();
        }
    }
}
