namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core
{
    public interface IFloorPadInteractor
    {
        void Interact(IFloorPad floorPad, int candidateWeight);
    }
}