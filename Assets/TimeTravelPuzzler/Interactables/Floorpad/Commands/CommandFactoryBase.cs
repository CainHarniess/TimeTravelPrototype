using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads.Commands
{
    public abstract class CommandFactoryBase : IFactory<IRewindableCommand, int>
    {
        private readonly IWeightedFloorPad _floorPad;

        protected CommandFactoryBase(IWeightedFloorPad floorPad)
        {
            _floorPad = floorPad;
        }

        protected IWeightedFloorPad FloorPad => _floorPad;

        public abstract IRewindableCommand Create(int candidateWeight);
    }
}