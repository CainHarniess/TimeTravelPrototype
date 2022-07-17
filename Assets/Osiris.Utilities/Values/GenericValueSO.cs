using UnityEngine;

namespace Osiris.Utilities.Values
{
    public abstract class GenericValueSO<T> : ScriptableObject
        where T : struct
    {
        [SerializeField] private T _value;
        public T Value { get => _value; protected set => _value = value; }
    }
}
