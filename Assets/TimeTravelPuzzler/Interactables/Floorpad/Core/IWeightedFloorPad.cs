namespace Osiris.TimeTravelPuzzler.Interactables.Core
{
    public interface IWeightedFloorPad : IFloorPad
    {
        public void AddWeight(int weightToAdd);
        public void RemoveWeight(int weightToRemove);
    }
}