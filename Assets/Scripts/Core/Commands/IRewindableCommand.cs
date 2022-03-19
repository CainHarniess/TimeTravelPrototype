namespace Osiris.TimeTravelPuzzler.Core.Commands
{
    public interface IRewindableCommand : ICommand
    {
        ICommand Inverse { get; }
    }
}