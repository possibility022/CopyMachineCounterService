using System;
using System.Threading.Tasks;

namespace CopyinfoWPF.Commands
{
    public class AsyncCommand : AsyncCommandBase
    {
        private readonly Func<Task> _command;
        private readonly Func<bool> _canExecute;

        public AsyncCommand(Func<Task> command, Func<bool> canExecute)
        {
            _command = command;
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return _canExecute.Invoke();
        }

        public override Task ExecuteAsync(object parameter)
        {
            return _command();
        }
    }
}
