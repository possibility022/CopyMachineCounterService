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
    public partial class CAddress : UserControl
    {
        bool rewritePostCity = true;

        Database.Address address;

        public CAddress()
        {
            InitializeComponent();
            address = new Database.Address();
        }

        public CAddress(Database.Address a)
        {
            InitializeComponent();
            SetAddress(a);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            
        }

        private void Rewrite(TextBox source, TextBox target)
        {
            target.Text = source.Text;
        }

        public void SetAddress(Database.Address a)
        {
            this.address = a;

            tblCity.Text = address.city;
            tblPostCode.Text = address.postcode;
            tblPostCity.Text = address.post_city;
            tblStreet.Text = address.street;
            tblApartmentNumber.Text = address.apartment;
            tblHouseNumber.Text = address.house_number;
        }
    }
}
