namespace Osiris.TimeTravelPuzzler.Interactables
{
    public interface IInteractable
    {
        void Interact();
    }

    public interface IInteractable<T>
    {
        void Interact(T parameter);
    }
}
