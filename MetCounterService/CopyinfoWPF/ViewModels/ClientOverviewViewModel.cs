using AutoMapper;
using CopyinfoWPF.DTO.Models;
using CopyinfoWPF.Model;
using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using Prism.Mvvm;

namespace CopyinfoWPF.ViewModels
{
    public class ClientOverviewViewModel : BindableBase
    {

        #region Address

        private string _street;
        public string Street
        {
            get { return _street; }
            set { SetProperty(ref _street, value); }
        }

        private string _houseNumber;
        public string HouseNumber
        {
            get { return _houseNumber; }
            set { SetProperty(ref _houseNumber, value); }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }

        private string _postalCode;
        public string PostalCode
        {
            get { return _postalCode; }
            set { SetProperty(ref _postalCode, value); }
        }

        private string _postalCity;
        public string PostalCity
        {
            get { return _postalCity; }
            set { SetProperty(ref _postalCity, value); }
        }

        #endregion

        #region Client

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _nip;
        public string NIP
        {
            get { return _nip; }
            set { SetProperty(ref _nip, value); }
        }

        private bool _serviceAgreement;
        public bool ServiceAgreement
        {
            get { return _serviceAgreement; }
            set { SetProperty(ref _serviceAgreement, value); }
        }

        private string _note;
        public string Note
        {
            get { return _note; }
            set { SetProperty(ref _note, value); }
        }

        #endregion

        public ClientOverviewViewModel()
        {

        }

        public ClientOverviewViewModel(Klient client)
        {
            if (client != null)
                Mapper.Map(client, this);
        }
    }
}
