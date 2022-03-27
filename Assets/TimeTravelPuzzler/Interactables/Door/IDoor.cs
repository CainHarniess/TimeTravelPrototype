namespace Osiris.TimeTravelPuzzler.Interactables
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
