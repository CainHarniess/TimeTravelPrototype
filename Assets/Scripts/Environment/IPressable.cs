namespace Osiris.TimeTravelPuzzler.Environment
{
    public interface IPressable
    {
        bool IsPressed { get; }

        bool CanPress(int additionalWeight);
        void Press();
        bool CanRelease(int weightToRemove);
        void Release();
    }
}

