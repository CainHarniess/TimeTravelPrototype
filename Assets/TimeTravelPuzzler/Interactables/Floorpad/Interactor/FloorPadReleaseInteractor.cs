using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class FloorPadReleaseInteractor : FloorPadInteractorBase
    {
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!(other.GetComponent<IReleaseInteractable>() is IReleaseInteractable floorPad))
            {
                Logger.Log("Component implementing IFloorPadReleaseInteractable not found on candidate.", GameObjectName);
                return;
            }

            floorPad.Interact(Weight);
        }
    }
}