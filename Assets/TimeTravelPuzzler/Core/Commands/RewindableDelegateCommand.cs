using Osiris.Utilities.Commands;
using System;

namespace Osiris.TimeTravelPuzzler.Core.Commands
{
    public abstract class RewindableDelegateCommand<T> : IRewindableCommand
    {
        private Func<T, bool> _canExecute;
        private Action<T> _execute;
        private T _parameter;
        private string _description;
        private ICommand _inverse;

        public RewindableDelegateCommand(Func<T, bool> canExecute, Action<T> execute, T parameter, string description)
        {
            _canExecute = canExecute;
            _execute = execute;
            _parameter = parameter;
            _description = description;
        }

        public RewindableDelegateCommand(Func<T, bool> canExecute, Action<T> execute, T parameter, string description,
                                         ICommand inverse)
            : this(canExecute, execute, parameter, description)
        {
            _inverse = inverse;
        }

        protected T Parameter => _parameter;

        public bool CanExecute(object parameter = null)
        {
            return _canExecute(_parameter);
        }

        public void Execute(object parameter = null)
        {
            _execute(_parameter);
        }

        public string Description => _description;

        public ICommand Inverse => _inverse;
    }
}
