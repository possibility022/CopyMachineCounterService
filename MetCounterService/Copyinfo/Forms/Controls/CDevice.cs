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
    public partial class CDevice : UserControl
    {

        Database.Device device;
        CAddress cAddress;

        public CDevice()
        {
            init();
        }

        public CDevice(Database.Device device)
        {
            init();
            this.device = device;
        }

        private void init()
        {
            InitializeComponent();
            monthCalendar1.MaxSelectionCount = 1;
            monthCalendar1.SelectionStart = DateTime.Today;
            setDateInText();
        }

        public Database.Device getDevice()
        {
            Database.Device device = new Database.Device();
            device.serial_number = txtSerialNumber.Text;
            device.model = txtModel.Text;
            device.provider = txtProvider.Text;
            device.instalation_datetime = monthCalendar1.SelectionStart;

            return device;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>Zwraca true jesli wszystkie pola sa wypełnione poprawnie</returns>
        private bool checkFields()
        {
            if (checkTxtControl(txtInstallationDate) &&
                checkTxtControl(txtInstallationPlace) &&
                checkTxtControl(txtModel) &&
                checkTxtControl(txtProvider) &&
                checkTxtControl(txtSerialNumber))
                return true;

            return false;
        }



        /// <summary>
        /// Zwraca true jeśli pole jest prawidłowe
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private bool checkTxtControl(TextBox t)
        {
            if (t.Text.Length == 0)
            {
                txtProvider.BackColor = Style.txtErrorColor;
                return false;
            }

            return true;
        }

        private void hideAll()
        {
            foreach(Control c in Controls)
            {
                c.Hide();
            }
        }

        private void showAll()
        {
            foreach(Control c in Controls)
            {
                c.Show();
            }
        }

        private void hideAddressControl()
        {
            foreach(Control c in Controls)
            {
                if (c is CAddress)
                    c.Hide();
            }
        }

        private void txtInstallationPlace_DoubleClick(object sender, EventArgs e)
        {
            if (Controls.Contains(cAddress))
            {
                hideAll();
                foreach (Control c in Controls)
                {
                    if (c is CAddress)
                    {
                        c.Show();
                        break;
                    }
                }
            }
            else
            {
                cAddress = new CAddress();
                if (device != null)
                    cAddress.setAddress(device.getAddress());
                hideAll();
                Controls.Add(cAddress);

                foreach(Control c in Controls)
                {
                    if (c is CAddress)
                    {
                        c.Show();
                        break;
                    }
                }
            }
        }

        private void CAddDevice_Click(object sender, EventArgs e)
        {
            showAll();
            hideAddressControl();

            foreach(Control c in Controls)
            {
                if (c is CAddress)
                {
                    CAddress a = (CAddress)c;
                    txtInstallationPlace.Text = a.getAddress().street + " " + a.getAddress().house_number + "/" + a.getAddress().apartment;
                }
            }
        }

        private void setDateInText()
        {
            txtInstallationDate.Text = monthCalendar1.SelectionStart.Date.Day.ToString() + " (" + monthCalendar1.SelectionStart.Date.DayOfWeek.ToString() + ") " + monthCalendar1.SelectionStart.Month.ToString();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            setDateInText();
        }
    }
}
