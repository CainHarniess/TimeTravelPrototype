using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class ReleaseFloorPadInteractableBehaviour : FloorPadInteractableBehaviour, IFloorPadReleaseInteractable
    {
        protected override IInteractable<int> GetInteractable()
        {
            return new FloorPadInteractable(GameObjectName, new ReleaseCommandFactory(FloorPad), Logger, RecordableActionOccurred);
        }
    }
}