using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.Core;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class OneWayFloorPadReleaseCommandFactory : FloorPadReleaseCommandFactory
    {
        public OneWayFloorPadReleaseCommandFactory(IWeightedFloorPad floorPad) : base(floorPad)
        {

        }

        public override IRewindableCommand Create(int candidateWeight)
        {
            var inverse = new DelegateFloorPadCommand(candidateWeight, FloorPad.CanPress, FloorPad.Press, FloorPad.AddWeight,
                                               "One-way floor pad release inverse");
            return new DelegateFloorPadCommand(candidateWeight, FloorPad.CanRelease, () => { },
                                               FloorPad.RemoveWeight, "One-way floor pad release", inverse);
        }
    }
}