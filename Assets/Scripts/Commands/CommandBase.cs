using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Commands
{
    public abstract class CommandBase : ICommand
    {
        public abstract bool CanExecute(object parameter = null);

        public abstract void Execute(object parameter = null);
    }
}
