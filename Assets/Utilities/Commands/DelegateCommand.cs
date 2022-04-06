using System;

namespace Osiris.Utilities.Commands
{

    public class DelegateCommand : Command
    {
        private Func<bool> _canExecute;
        private Action _execute;

        public DelegateCommand(Func<bool> canExecute, Action execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public override bool CanExecute(object parameter = null)
        {
            return _canExecute();
        }

        public override void Execute(object parameter = null)
        {
            _execute();
        }
    }
}
