namespace Osiris.Utilities.Commands
{
    public abstract class Command
    {
        public abstract bool CanExecute(object parameter = null);

        public abstract void Execute(object parameter = null);
    }
}
