using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Commands;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class PressFloorPadInteractableBehaviour : FloorPadInteractableBehaviour, IFloorPadPressInteractable
    {
        protected override IInteractable<int> GetInteractable()
        {
            return new FloorPadInteractable(GameObjectName, new PressCommandFactory(FloorPad), Logger, RecordableActionOccurred);
        }
    }
}