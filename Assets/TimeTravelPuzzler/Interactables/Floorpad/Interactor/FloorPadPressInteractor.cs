using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class FloorPadPressInteractor : FloorPadInteractorBase
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!(other.GetComponent<IFloorPadPressInteractable>() is IFloorPadPressInteractable pressInteractable))
            {
                Logger.Log("Component implementing IFloorPadPressInteractable not found on candidate.", GameObjectName);
                return;
            }

            pressInteractable.Interact(Weight);
        }
    }
}