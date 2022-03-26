namespace Osiris.TimeTravelPuzzler.Interactables.Core
{
    public interface IFloorPad
    {
        int CurrentPressWeight { get; }
        bool IsPressed { get; }
        int RequiredPressWeight { get; }
        bool CanPress(int additionalWeight);
        bool CanRelease(int weightRemoved);
        void Press();
        void Release();
    }
}