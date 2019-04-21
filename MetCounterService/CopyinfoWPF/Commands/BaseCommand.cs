using System;
using System.Windows.Input;

namespace CopyinfoWPF.Commands
{
    class BaseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        Action _action;

        public BaseCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            _action.Invoke();
        }
    }
}
