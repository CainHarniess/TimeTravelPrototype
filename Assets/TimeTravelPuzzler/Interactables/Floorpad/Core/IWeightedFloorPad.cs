namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core
{
    public interface IWeightedFloorPad : IFloorPad
    {
        public void AddWeight(int weightToAdd);
        public void RemoveWeight(int weightToRemove);
    }
}