using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.Core;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class FloorPadPressCommandFactory : FloorPadCommandFactoryBase
    {
        public FloorPadPressCommandFactory(IWeightedFloorPad floorPad) : base(floorPad)
        {

        }

        public override IRewindableCommand Create(int candidateWeight)
        {
            var inverse = new DelegateFloorPadCommand(candidateWeight, FloorPad.CanRelease, FloorPad.Release,
                                                      FloorPad.RemoveWeight, "Floor pad press inverse");
            return new DelegateFloorPadCommand(candidateWeight, FloorPad.CanPress, FloorPad.Press, FloorPad.AddWeight,
                                               "Floor pad press", inverse);
        }
    }
}