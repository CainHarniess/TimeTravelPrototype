using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.Core;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public abstract class FloorPadCommandFactoryBase : IFloorPadCommandFactory
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