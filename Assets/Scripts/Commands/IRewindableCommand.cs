namespace Osiris.TimeTravelPuzzler.Commands
{
    public interface IRewindableCommand
    {
        ICommand Inverse { get; }
    }
}