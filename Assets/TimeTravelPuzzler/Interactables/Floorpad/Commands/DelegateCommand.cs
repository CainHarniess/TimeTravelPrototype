using Osiris.TimeTravelPuzzler.Core.Commands;
using System;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads.Commands
{
    public class DelegateCommand : IRewindableCommand
    {
        private int _candidateWeight;
        private Func<bool> _canExecute;
        private Action _execute;
        private Action<int> _adjustWeight;
        private ICommand _inverse;
        private string _description;

        public DelegateCommand(int candidateWeight, Func<bool> canExecute, Action execute, Action<int> adjustWeight,
                               string description)
        {
            _candidateWeight = candidateWeight;
            _canExecute = canExecute;
            _execute = execute;
            _adjustWeight = adjustWeight;
            _description = description;
        }

        public DelegateCommand(int candidateWeight, Func<bool> canExecute, Action execute, Action<int> adjustWeight,
                               string description, ICommand inverse)
            : this(candidateWeight, canExecute, execute, adjustWeight, description)
        {
            _inverse = inverse;
        }

        public ICommand Inverse => _inverse;

        public string Description => _description;

        public bool CanExecute(object parameter = null)
        {
            _adjustWeight(_candidateWeight);
            return _canExecute();
        }

        public void Execute(object parameter = null)
        {
            _execute();
        }
    }
}