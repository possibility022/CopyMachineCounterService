using Prism.Mvvm;
using System.Threading;
using System.Threading.Tasks;

namespace CopyinfoWPF.ViewModels
{
    public class ClientOverviewViewModel : BindableBase
    {

        private string _address;
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        public ClientOverviewViewModel()
        {
            Task.Factory.StartNew(() => { Thread.Sleep(4000); Address = "akjshdakjuwhuda"; });
        }
    }
}
