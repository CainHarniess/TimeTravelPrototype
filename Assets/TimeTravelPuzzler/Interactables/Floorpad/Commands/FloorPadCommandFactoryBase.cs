using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads.Commands
{
    public abstract class FloorPadCommandFactoryBase : IFactory<IRewindableCommand, int>
    {
        private readonly IWeightedFloorPad _floorPad;

        protected FloorPadCommandFactoryBase(IWeightedFloorPad floorPad)
        {
            _floorPad = floorPad;
        }

        protected IWeightedFloorPad FloorPad => _floorPad;

        public abstract IRewindableCommand Create(int candidateWeight);
    }
}