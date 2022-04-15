using Osiris.Utilities.Commands;
using System;

namespace Osiris.TimeTravelPuzzler.Core.Commands
{
    public abstract class CommandBase : ICommand
    {
        public abstract bool CanExecute(object parameter = null);
        public abstract void Execute(object parameter = null);
        public abstract string Description { get; }
    }

    public abstract class RewindableDelegateCommand : DelegateCommand, IRewindableCommand
    {
        private string _description;
        private ICommand _inverse;

        public RewindableDelegateCommand(Func<bool> canExecute, Action execute, string description)
            : base(canExecute, execute)
        {
            _description = description;
        }

        public RewindableDelegateCommand(Func<bool> canExecute, Action execute, string description, ICommand inverse)
            : this(canExecute, execute, description)
        {
            _inverse = inverse;
        }

        public string Description => _description;
        public ICommand Inverse => _inverse;
    }
}
