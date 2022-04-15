using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.Utilities.Commands;
using System;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads.Commands
{
    public class FloorPadDelegateCommand : DelegateCommand, IRewindableCommand
    {
        private int _candidateWeight;
        private Action<int> _adjustWeight;
        private ICommand _inverse;
        private string _description;

        public FloorPadDelegateCommand(int candidateWeight, Func<bool> canExecute, Action execute,
                                       Action<int> adjustWeight, string description)
            : base(canExecute, execute)
        {
            _candidateWeight = candidateWeight;
            _adjustWeight = adjustWeight;
            _description = description;
        }

        public FloorPadDelegateCommand(int candidateWeight, Func<bool> canExecute, Action execute,
                                       Action<int> adjustWeight, string description, ICommand inverse)
            : this(candidateWeight, canExecute, execute, adjustWeight, description)
        {
            _inverse = inverse;
        }

        public ICommand Inverse => _inverse;

        public string Description => _description;

        public override bool CanExecute(object parameter = null)
        {
            _adjustWeight(_candidateWeight);
            return base.CanExecute();
        }
    }
}