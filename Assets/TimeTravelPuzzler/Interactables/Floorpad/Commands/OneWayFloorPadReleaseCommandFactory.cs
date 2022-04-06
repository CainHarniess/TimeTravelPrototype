using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads.Commands
{
    public class OneWayFloorPadReleaseCommandFactory : ReleaseCommandFactory
    {
        public OneWayFloorPadReleaseCommandFactory(IWeightedFloorPad floorPad)
            : base(floorPad)
        {

        }

        public override IRewindableCommand Create(int candidateWeight)
        {
            var inverse = new FloorPadDelegateCommand(candidateWeight, FloorPad.CanPress, FloorPad.Press, FloorPad.AddWeight,
                                              "One-way floor pad release inverse");
            return new FloorPadDelegateCommand(candidateWeight, FloorPad.CanRelease, () => { },
                                       FloorPad.RemoveWeight, "One-way floor pad release", inverse);
        }
    }
}