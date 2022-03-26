using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public interface IFloorPadInteractor
    {
        void Interact(IFloorPad floorPad, int candidateWeight);
    }
}