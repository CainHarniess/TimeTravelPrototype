using Osiris.Utilities.Commands;

namespace Osiris.TimeTravelPuzzler.Core.Commands
{
    public interface IRewindableCommand : ICommand
    {
        string Description { get; }
        ICommand Inverse { get; }
    }
}