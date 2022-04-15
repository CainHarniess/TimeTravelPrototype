using Osiris.Utilities.Commands;
using System;

namespace Osiris.TimeTravelPuzzler.Interactables.Doors.Commands
{

    public class DelegateCommand : ICommand
    {
        private Func<bool> _canExecute;
        private Action _execute;
        private string _description;

        public DelegateCommand(Func<bool> canExecute, Action execute, string description)
        {
            _canExecute = canExecute;
            _execute = execute;
            _description = description;
        }

        public string Description => _description;

        public bool CanExecute(object parameter = null)
        {
            return _canExecute();
        }

        public void Execute(object parameter = null)
        {
            _execute();
        }
    }
}
