using Osiris.TimeTravelPuzzler.Interactables.Core;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class FloorPadPressInteractorBehaviour : FloorPadInteractorBehaviourBase
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!(other.GetComponent<IFloorPadPressInteractable>() is IFloorPadPressInteractable floorPad))
            {
                Logger.Log("Component implementing IFloorPadPressInteractable not found on candidate.", gameObject.name);
                return;
            }

            floorPad.Interact(Weight);
        }
    }
}