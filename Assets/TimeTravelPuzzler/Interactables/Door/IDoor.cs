namespace Osiris.TimeTravelPuzzler.Interactables.Doors
{
    public interface IDoor
    {
        bool IsOpen { get; }
        bool CanOpen();
        void Open();
        bool CanClose();
        void Close();
    }
}
