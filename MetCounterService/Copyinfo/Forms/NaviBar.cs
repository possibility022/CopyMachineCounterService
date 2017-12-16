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
using Z80NavBarControl;
using Z80NavBarControl.Z80NavBar.Themes;

namespace Copyinfo.Forms
{
    abstract public partial class NaviBar : Form
    {

        private Z80_Navigation z80_Navigation1;

        protected Z80_Navigation NaviBarControl { get { if (z80_Navigation1 == null) InitializeNavibar(); return z80_Navigation1; } }


        protected List<UserControl> pagesToView = new List<UserControl>();

        public NaviBar()
        {
            InitializeComponent();
        }

        private void InitializeNavibar()
        {
            z80_Navigation1 = new Z80_Navigation()
            {
                Dock = DockStyle.Left,
                Size = new Size(200, Width)
            };
            z80_Navigation1.Initialize(new List<NavBarItem>(), new ThemeSelector(Theme.RoyalBlue).CurrentTheme);
            Controls.Add(z80_Navigation1);

            NaviBarControl.SelectedItem += NaviBar_SelectedItem;
        }

        private void NaviBar_SelectedItem(NavBarItem item)
        {
            foreach (UserControl control in pagesToView)
                control.Hide();

            pagesToView[item.ID].Show();
        }

        protected void Z80_Navigation1_SelectedItem(NavBarItem item)
        {
            foreach (UserControl control in pagesToView)
                control.Hide();

            pagesToView[item.ID].Show();
        }

        protected void AdjustControl(UserControl control)
        {
            control.Size = new Size(this.Width - NaviBarControl.Width, Height);
            control.Dock = DockStyle.Right;
            control.Visible = false;
            control.VisibleChanged += Control_VisibleChanged;
            Controls.Add(control);
            pagesToView.Add(control);
        }

        private void Control_VisibleChanged(object sender, EventArgs e)
        {
            UserControl control = sender as UserControl;
            if (control != null)
            {
                control.Width = Width - 20 - NaviBarControl.Width;
            }
        }

        protected void InitNavibarItems()
        {
            List<NavBarItem> menuItems = new List<NavBarItem>();

            int i = 0;
            foreach (UserControl control in pagesToView)
            {
                menuItems.Add(new NavBarItem() { ID = i, Text = control.Tag.ToString() });
                i++;
            }
            z80_Navigation1.Initialize(menuItems, new ThemeSelector(Theme.RoyalBlue).CurrentTheme);
        }
    }
}
