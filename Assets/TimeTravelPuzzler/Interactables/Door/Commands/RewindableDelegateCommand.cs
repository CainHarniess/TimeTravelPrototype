using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.Utilities.Commands;
using System;

namespace Osiris.TimeTravelPuzzler.Interactables.Doors.Commands
{
    public class RewindableDelegateCommand : DelegateCommand, IRewindableCommand
    {
        private ICommand _inverse;

        public RewindableDelegateCommand(Func<bool> canExecute, Action execute, string description)
            : base(canExecute, execute, description)
        {

        }

        public RewindableDelegateCommand(Func<bool> canExecute, Action execute, string description,
            ICommand inverse)
            : this(canExecute, execute, description)
        {
            _inverse = inverse;
        }

        public ICommand Inverse => _inverse;
    }
}
