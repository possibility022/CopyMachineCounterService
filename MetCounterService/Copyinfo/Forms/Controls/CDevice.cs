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
            if (checkFields() == false)
                return null;
            Database.Device device = new Database.Device();
            device.id = txtSerialNumber.Text;
            device.model = txtModel.Text;
            device.provider = txtProvider.Text;
            device.instalation_datetime = monthCalendar1.SelectionStart;
            device.setAddress(cAddress1.getAddress());
            return device;
        }

        public void setSerialnumber(string serial_number)
        {
            this.txtSerialNumber.Text = serial_number;
        }

        private void setDevice()
        {
            txtProvider.Text = device.provider;
            txtModel.Text = device.model;
            txtSerialNumber.Text = device.id;
            monthCalendar1.SetDate(device.instalation_datetime);
            cAddress1.setAddress(device.getAddress());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>Zwraca true jesli wszystkie pola sa wypełnione poprawnie</returns>
        private bool checkFields()
        {
            if (checkTxtControl(txtInstallationDate) &&
                checkTxtControl(txtModel) &&
                checkTxtControl(txtProvider) &&
                checkTxtControl(txtSerialNumber) && 
                checkAddress())
                return true;

            return false;
        }

        private bool checkAddress()
        {
            Database.Address ad = cAddress1.getAddress();
            if (ad.city != null)
                if (ad.city.Length > 0)
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

        private void txtInstallationPlace_DoubleClick(object sender, EventArgs e)
        {

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
