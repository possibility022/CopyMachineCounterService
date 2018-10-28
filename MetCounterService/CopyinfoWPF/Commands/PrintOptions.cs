using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CopyinfoWPF.Commands
{
    public class PrintOptions : ICommand
    {
        Func<string, Task<bool>> _action;

        public PrintOptions(Func<string, Task<bool>> action)
        {
            _action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            _action(parameter as string);
        }
    }
}
