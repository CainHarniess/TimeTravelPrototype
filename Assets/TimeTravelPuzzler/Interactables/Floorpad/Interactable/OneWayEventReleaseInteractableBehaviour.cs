using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class OneWayEventReleaseInteractableBehaviour : EventReleaseInteractableBehaviour
    {
        protected override IFactory<IRewindableCommand, int> GetFactory(IFloorPad floorPad)
        {
            return new OneWayFloorPadReleaseCommandFactory(FloorPad);
        }
    }
}