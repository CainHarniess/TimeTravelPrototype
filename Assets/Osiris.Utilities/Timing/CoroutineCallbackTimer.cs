using Osiris.EditorCustomisation;
using System;
using System.Collections;
using UnityEngine;

namespace Osiris.Utilities
{
    [Serializable]
    public abstract class CoroutineCallbackTimer
    {
        private Action _callback;

        [Header(InspectorHeaders.DebugVariables)]
        [ReadOnly] [SerializeField] private float _TimePassed;

        public CoroutineCallbackTimer(Action callback)
        {
            _callback = callback;
            _TimePassed = 0;
        }

        protected abstract float Duration { get; }
        protected float TimePassed { get => _TimePassed; set => _TimePassed = value; }
        protected Action CallBack => _callback;

        public virtual IEnumerator StartTimer()
        {
            while (_TimePassed <= Duration)
            {
                _TimePassed += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            Finish();
        }

        protected void Finish()
        {
            _TimePassed = 0;
            _callback();
        }
    }
}
