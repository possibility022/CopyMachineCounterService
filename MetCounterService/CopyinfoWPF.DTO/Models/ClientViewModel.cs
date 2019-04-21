using Prism.Mvvm;

namespace CopyinfoWPF.DTO.Models
{
    public class ClientViewModel : BindableBase
    {

        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _nip;
        public string NIP
        {
            get { return _nip; }
            set { SetProperty(ref _nip, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        private string _note;
        public string Note
        {
            get { return _note; }
            set { SetProperty(ref _note, value); }
        }

        private string _phones;
        public string Phones
        {
            get { return _phones; }
            set { SetProperty(ref _phones, value); }
        }
    }
}
