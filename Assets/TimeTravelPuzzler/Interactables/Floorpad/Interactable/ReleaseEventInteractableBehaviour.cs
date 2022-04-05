using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class ReleaseEventInteractableBehaviour : EventInteractableBehaviour, IFloorPadReleaseInteractable
    {
        protected override IFactory<IRewindableCommand, int> GetFactory(IFloorPad floorPad)
        {
            return new ReleaseCommandFactory(FloorPad);
        }

        protected override IInteractable<int> GetInteractable()
        {
            return new ReleaseEventInteractable(GameObjectName, GetFactory(FloorPad), Logger, Interacted,
                                                   RecordableActionOccurred);
        }
    }
}