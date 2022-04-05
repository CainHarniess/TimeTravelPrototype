using Osiris.TimeTravelPuzzler.Core.Commands;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public interface IFloorPadCommandFactory
    {
        IRewindableCommand Create(int candidateWeight);
    }
}