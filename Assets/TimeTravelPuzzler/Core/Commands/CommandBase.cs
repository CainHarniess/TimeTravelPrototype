namespace Osiris.TimeTravelPuzzler.Core.Commands
{
    public abstract class CommandBase : ICommand
    {
        public abstract bool CanExecute(object parameter = null);
        public abstract void Execute(object parameter = null);
        public abstract string Description { get; }
    }
}
