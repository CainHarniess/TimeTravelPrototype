namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core
{
    public interface IFloorPadInteractor
    {
        void Interact(IWeightedFloorPad floorPad, int candidateWeight);
    }
}