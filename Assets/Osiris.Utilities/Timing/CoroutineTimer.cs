using System;
using System.Collections;
using UnityEngine;

namespace Osiris.Utilities
{
    public abstract class Timer : ScriptableObject
    {
        public abstract void StartTimer();

        public abstract void StopTimer();

        public abstract void Reset();
    }

    public class CoroutineTimer
    {
        private float _duration;
        private Action _callback;

        public CoroutineTimer(float duration, Action callback)
        {
            _duration = duration;
            _callback = callback;
        }

        public IEnumerator StartTimer()
        {
            yield return new WaitForSeconds(_duration);
            Finish();
        }

        private void Finish()
        {
            _callback();
        }
    }
}
