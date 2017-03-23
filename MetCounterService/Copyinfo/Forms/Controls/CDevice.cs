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
            Init();
        }

        public CDevice(Database.Device device)
        {
            Init();
            this.device = device;
        }

        private void Init()
        {
            InitializeComponent();
            monthCalendar1.MaxSelectionCount = 1;
            monthCalendar1.SelectionStart = DateTime.Today;
            SetDateInText();
        }

        public Database.Device GetDevice()
        {
            if (CheckFields() == false)
                return null;
            Database.Device device = new Database.Device();
            device.serial_number = txtSerialNumber.Text;
            device.model = txtModel.Text;
            device.provider = txtProvider.Text;
            device.instalation_datetime = monthCalendar1.SelectionStart;
            //device.setAddress(cAddress1.getAddress());
            return device;
        }

        public void SetSerialnumber(string serial_number)
        {
            this.txtSerialNumber.Text = serial_number;
        }

        private void SetDevice()
        {
            txtProvider.Text = device.provider;
            txtModel.Text = device.model;
            txtSerialNumber.Text = device.serial_number;
            monthCalendar1.SetDate(device.instalation_datetime);
            cAddress1.SetAddress(device.address);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>Zwraca true jesli wszystkie pola sa wypełnione poprawnie</returns>
        private bool CheckFields()
        {
            if (CheckTxtControl(txtInstallationDate) &&
                CheckTxtControl(txtModel) &&
                CheckTxtControl(txtProvider) &&
                CheckTxtControl(txtSerialNumber) && 
                CheckAddress())
                return true;

            return false;
        }

        private bool CheckAddress()
        {
            Database.Address ad = cAddress1.GetAddress();
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
        private bool CheckTxtControl(TextBox t)
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

        private void SetDateInText()
        {
            txtInstallationDate.Text = monthCalendar1.SelectionStart.Date.Day.ToString() + " (" + monthCalendar1.SelectionStart.Date.DayOfWeek.ToString() + ") " + monthCalendar1.SelectionStart.Month.ToString();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            SetDateInText();
        }
    }
}
