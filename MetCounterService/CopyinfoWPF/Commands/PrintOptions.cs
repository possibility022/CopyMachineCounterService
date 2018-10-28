using System;
using System.Windows.Input;

namespace CopyinfoWPF.Commands
{
    public class PrintOptions : ICommand
    {
        Action<string> _action;

        public PrintOptions(Action<string> action)
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
