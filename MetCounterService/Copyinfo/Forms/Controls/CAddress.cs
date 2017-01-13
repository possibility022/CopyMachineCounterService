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
            setAddress(a);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            
        }

        private void rewrite(TextBox source, TextBox target)
        {
            target.Text = source.Text;
        }

        private void txtCity_TextChanged(object sender, EventArgs e)
        {
            if (rewritePostCity)
                rewrite(txtCity, txtPost);
        }

        private void txtPost_TextChanged(object sender, EventArgs e)
        {
            
        }

        public Database.Address getAddress()
        {
            address.city = txtCity.Text;
            address.postcode = txtPostCode.Text;
            address.post_city = txtPost.Text;
            address.street = txtStreet.Text;
            address.apartment = txtApartment.Text;
            address.house_number = txtHouseNumber.Text;

            return address;
        }

        public void setAddress(Database.Address a)
        {
            this.address = a;

            txtCity.Text = address.city;
            txtPostCode.Text = address.postcode;
            txtPost.Text = address.post_city;
            txtStreet.Text = address.street;
            txtApartment.Text = address.apartment;
            txtHouseNumber.Text = address.house_number;
        }
    }
}
