using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class FloorPadPressEventChannelInteractableBehaviour : FloorPadInteractableBehaviour, IPressInteractable
    {
        protected override IFactory<IRewindableCommand, int> GetFactory(IFloorPad floorPad)
        {
            return new FloorPadPressCommandFactory(FloorPad);
        }

        protected override IInteractable<int> GetInteractable()
        {
            return new FloorPadPressInteractable(GameObjectName, GetFactory(FloorPad), Logger, Interacted,
                                                 RecordableActionOccurred);
        }
    }
}