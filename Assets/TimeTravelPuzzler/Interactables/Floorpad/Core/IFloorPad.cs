namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core
{
    public interface IFloorPad
    {
        int CurrentPressWeight { get; }
        bool IsPressed { get; }
        int RequiredPressWeight { get; }
        bool CanPress();
        bool CanRelease();
        void Press();
        void Release();
    }
}