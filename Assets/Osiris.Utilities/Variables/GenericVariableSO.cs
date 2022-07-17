using Osiris.EditorCustomisation;
using UnityEngine;

namespace Osiris.Utilities.Variables
{
    public abstract class GenericVariableSO<T> : ScriptableObject
         where T : struct
    {
        [ReadOnly] [SerializeField] private T _value;
        public T Value { get => _value; set => _value = value; }
    }
}
