using System;
using System.Threading.Tasks;

namespace CopyinfoWPF.Commands
{
    public class AsyncCommand : AsyncCommandBase
    {
        private readonly Func<Task> _command;
        private readonly Func<object, bool> _canExecute;

        public AsyncCommand(Func<Task> command, Func<object, bool> canExecute)
        {
            _command = command;
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public override Task ExecuteAsync(object parameter)
        {
            return _command();
        }
    }
}
