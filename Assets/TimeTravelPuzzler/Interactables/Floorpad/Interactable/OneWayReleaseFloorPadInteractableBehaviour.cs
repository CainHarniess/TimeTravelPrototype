using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Commands;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class OneWayReleaseFloorPadInteractableBehaviour : ReleaseFloorPadInteractableBehaviour
    {
        protected override IInteractable<int> GetInteractable()
        {
            return new FloorPadInteractable(GameObjectName, new OneWayFloorPadReleaseCommandFactory(FloorPad), Logger, RecordableActionOccurred);
        }
    }
}