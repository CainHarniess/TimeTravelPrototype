using Osiris.EditorCustomisation;
using Osiris.Utilities.Validation;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Osiris.Utilities
{
    [Serializable]
    public class RandomCoroutineCallbackTimer : CoroutineCallbackTimer
    {
        private float _MinDuration;
        private float _MaxDuration;

        [Header(InspectorHeaders.DebugVariables)]
        [ReadOnly] [SerializeField] private float _Duration;

        protected override float Duration { get => _Duration; }

        internal float MinDuration => _MinDuration;
        internal float MaxDuration => _MaxDuration;

        public RandomCoroutineCallbackTimer(Action callback, float minDuration, float maxDuration, IValidator<float, float> validator)
            : base(callback)
        {
            if (!validator.IsValid(minDuration, maxDuration))
            {
                return;
            }

            _MinDuration = minDuration;
            _MaxDuration = maxDuration;
        }

        public RandomCoroutineCallbackTimer(Action callback, float minDuration, float maxDuration)
            : base(callback)
        {
            _MinDuration = minDuration;
            _MaxDuration = maxDuration;
        }

        public override IEnumerator StartTimer()
        {
            GenerateDuration();
            yield return base.StartTimer();
        }

        private void GenerateDuration()
        {
            _Duration = Random.Range(_MinDuration, _MaxDuration);
        }

    }
}
