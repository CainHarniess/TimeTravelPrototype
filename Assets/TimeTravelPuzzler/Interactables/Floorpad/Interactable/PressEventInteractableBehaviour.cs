using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using Osiris.Utilities;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class PressEventInteractableBehaviour : EventInteractableBehaviour, IFloorPadPressInteractable
    {
        protected override IFactory<IRewindableCommand, int> GetFactory(IFloorPad floorPad)
        {
            return new PressCommandFactory(FloorPad);
        }

        protected override IInteractable<int> GetInteractable()
        {
            return new PressEventInteractable(GameObjectName, GetFactory(FloorPad), Logger, Interacted,
                                                 RecordableActionOccurred);
        }
    }
}