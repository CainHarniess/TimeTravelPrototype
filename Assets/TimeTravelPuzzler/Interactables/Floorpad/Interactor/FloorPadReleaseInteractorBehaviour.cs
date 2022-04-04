using Osiris.TimeTravelPuzzler.Interactables.Core;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class FloorPadReleaseInteractorBehaviour : FloorPadInteractorBehaviourBase
    {
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!(other.GetComponent<IFloorPadReleaseInteractable>() is IFloorPadReleaseInteractable floorPad))
            {
                Logger.Log("Component implementing IFloorPadReleaseInteractable not found on candidate.", gameObject.name);
                return;
            }

            floorPad.Interact(Weight);
        }
    }
}