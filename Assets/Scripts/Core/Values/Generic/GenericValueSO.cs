using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Core.Values.Generic
{
    public abstract class GenericValueSO<T> : ScriptableObject
        where T : struct
    {
        [SerializeField] private T _value;
        public T Value { get => _value; }
    }
}
