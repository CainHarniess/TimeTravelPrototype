using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads.Commands
{
    public class FloorPadReleaseCommandFactory : FloorPadCommandFactoryBase
    {
        public FloorPadReleaseCommandFactory(IWeightedFloorPad floorPad) : base(floorPad)
        {

        }

        public override IRewindableCommand Create(int candidateWeight)
        {
            var inverse = new DelegateCommand(candidateWeight, FloorPad.CanPress, FloorPad.Press, FloorPad.AddWeight,
                                               "Floor pad release inverse");
            return new DelegateCommand(candidateWeight, FloorPad.CanRelease, FloorPad.Release,
                                               FloorPad.RemoveWeight, "Floor pad release", inverse);
        }
    }
}