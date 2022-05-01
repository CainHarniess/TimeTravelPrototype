using Osiris.EditorCustomisation;
using Osiris.Utilities.Extensions;
using Osiris.Utilities.Validation;
using UnityEngine;

namespace Osiris.Utilities.Animation
{
    public class RegularTriggerAnimationBehaviour : AnimationBehaviour
    {
        private Coroutine _timerCoroutine;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private RegularAnimationData _AnimationData;
        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private RandomCoroutineCallbackTimer _RandomTimer;

        protected override void Awake()
        {
            base.Awake();
            var validator = new RandomCoroutineTimerValidator();
            _RandomTimer = new RandomCoroutineCallbackTimer(Animate, _AnimationData.MinFlickerDelay,
                                                            _AnimationData.MaxFlickerDelay, validator);
        }

        private void Start()
        {
            IValidator validator = new AnimationBehaviourValidator(_AnimationData.ParameterName, Animator, GameObjectName, Logger);
            validator.IsValid();
        }

        public virtual void Animate()
        {
            Animator.SetTrigger(_AnimationData.ParameterName);
            RestartTimer();
        }

        private void RestartTimer()
        {
            this.TryStopCoroutine(_timerCoroutine);
            _timerCoroutine = StartCoroutine(_RandomTimer.StartTimer());
        }


        public void OnEnable()
        {
            _timerCoroutine = StartCoroutine(_RandomTimer.StartTimer());
        }

        public void OnDisable()
        {
            this.TryStopCoroutine(_timerCoroutine);
        }
    }
}
