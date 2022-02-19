namespace Osiris.TimeTravelPuzzler.Commands
{
    public interface ICommand
    {
        bool CanExecute(object parameter = null);
        void Execute(object parameter = null);
    }
}