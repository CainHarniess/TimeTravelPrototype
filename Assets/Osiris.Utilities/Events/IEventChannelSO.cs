using UnityEngine.Events;

namespace Osiris.Utilities.Events
{
    public interface IEventChannelSO
    {
        event UnityAction Event;

        void Raise();
    }

    public interface IEventChannelSO<T>
    {
        event UnityAction<T> Event;

        void Raise(T parameter);
    }
}