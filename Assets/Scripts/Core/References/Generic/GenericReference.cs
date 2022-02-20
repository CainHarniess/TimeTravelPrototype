using Osiris.TimeTravelPuzzler.Core.Values.Generic;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Core.References.Generic
{
    public abstract class GenericReference<T>
        where T : struct
    {
        [SerializeField] private bool _UseConstant = true;
        [SerializeField] private T _VariableValue;
        [SerializeField] private GenericValueSO<T> _Constant;
        public T Value => _UseConstant ? _Constant.Value : _VariableValue;
    }
}
