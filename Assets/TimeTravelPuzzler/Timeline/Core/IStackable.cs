namespace Osiris.TimeTravelPuzzler.Timeline.Core
{
    public interface IStackable<T>
    {
        int Count { get; }
        void Push(T item);
        T Peek();
        T Pop();
        void Clear();
    }

}
