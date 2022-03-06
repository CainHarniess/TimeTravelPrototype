namespace Osiris.TimeTravelPuzzler.Commands
{
    public interface IRewindableCommand : ICommand
    {
        ICommand Inverse { get; }
    }
}