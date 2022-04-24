using Osiris.EditorCustomisation;
using System;
using UnityEngine;

namespace Osiris.Utilities
{
    [Serializable]
    public class FixedCoroutineCallbackTimer : CoroutineCallbackTimer
    {
        [Header(InspectorHeaders.ControlVariables)]
        [ReadOnly] [SerializeField] private float _Duration;
        
        protected override float Duration { get => _Duration; }

        public FixedCoroutineCallbackTimer(Action callback) : base(callback)
        {

        }

        public FixedCoroutineCallbackTimer(float duration, Action callback) : this(callback)
        {
            _Duration = duration;
        }
    }
}
