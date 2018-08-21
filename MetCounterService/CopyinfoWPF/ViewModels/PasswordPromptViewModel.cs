using CopyinfoWPF.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyinfoWPF.ViewModels
{
    class PasswordPromptViewModel : BindableBase, INewElementProvider<string>
    {
        bool _result;

        string _password;

        public string Password { get => _password; set => SetProperty(ref _password, value); }

        public bool Accept()
        {
            _result = true;
            return _result;
        }

        public string GetElement()
        {
            return Password;
        }

        public bool GetResult()
        {
            return _result;
        }
    }
}
