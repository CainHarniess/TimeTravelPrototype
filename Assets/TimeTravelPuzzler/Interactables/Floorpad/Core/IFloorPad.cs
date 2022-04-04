namespace Osiris.TimeTravelPuzzler.Interactables.Core
{
    public interface IFloorPad
    {
        int CurrentPressWeight { get; set; }
        bool IsPressed { get; set; }
        int RequiredPressWeight { get; }
        bool CanPress();
        bool CanRelease();
        void Press();
        void Release();
    }
}