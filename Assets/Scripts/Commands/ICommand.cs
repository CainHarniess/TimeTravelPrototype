namespace Osiris.TimeTravelPuzzler.Commands
{
    public interface ICommand
    {
        string Description { get; }
        bool CanExecute(object parameter = null);
        void Execute(object parameter = null);
    }
}