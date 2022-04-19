using Osiris.Utilities.Values;
using UnityEngine;

namespace Osiris.Utilities.References
{
    public abstract class GenericReference<T> where T : struct
    {
        [SerializeField] private bool _UseConstant = true;
        [SerializeField] private T _VariableValue;
        [SerializeField] private GenericValueSO<T> _Constant;
        public T Value => UseConstant ? _Constant.Value : _VariableValue;
        public bool UseConstant => _UseConstant;
        public GenericValueSO<T> Constant { get => _Constant; }
    }
}
