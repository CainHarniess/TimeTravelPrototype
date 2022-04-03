namespace Osiris.TimeTravelPuzzler.Interactables.Core
{
    public interface IFloorPadInteractor
    {
        void Interact(IFloorPad floorPad, int candidateWeight);
    }
}