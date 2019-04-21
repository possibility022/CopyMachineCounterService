using System.Threading.Tasks;
using System.Windows.Input;

namespace CopyinfoWPF.Commands
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}
