using Osiris.EditorCustomisation;
using Osiris.Utilities.DependencyInjection;
using Osiris.Utilities.Extensions;
using UnityEngine;

namespace Osiris.Utilities.Animation
{
    [RequireComponent(typeof(AnimationCompletionEventDispatcherBehaviour))]
    public class RegularTriggerAnimationBehaviour : AnimationBehaviour
    {
        private Coroutine _timerCoroutine;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private RegularAnimationData _AnimationData;
        [SerializeField] private AnimationCompletionEventDispatcherBehaviour _EventDispatcher;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private RandomCoroutineCallbackTimer _RandomTimer;

        protected RegularAnimationData AnimationData => _AnimationData;

        protected override void Awake()
        {
            base.Awake();
            this.AddComponentInjectionIfNotPresent(ref _EventDispatcher, nameof(_EventDispatcher));
            _RandomTimer = new RandomCoroutineCallbackTimer(Animate, _AnimationData.MinimumInterval,
                                                            _AnimationData.MaximumInterval);
        }

        protected virtual void Start()
        {
            _AnimationData.IsValid(Animator, GameObjectName, Logger);
        }

        public void OnEnable()
        {
            _EventDispatcher.AnimationCompleted += RestartTimer;
            _timerCoroutine = StartCoroutine(_RandomTimer.StartTimer());
        }

        public virtual void Animate()
        {
            StopTimer();
            Animator.SetTrigger(_AnimationData.GetParameter());
        }

        private void StopTimer()
        {
            this.TryStopCoroutine(_timerCoroutine);
            Logger.Log("Coroutine stopped", GameObjectName);
        }

        private void RestartTimer()
        {
            _timerCoroutine = StartCoroutine(_RandomTimer.StartTimer());
            Logger.Log("Coroutine restarted", GameObjectName);
        }

        public void OnDisable()
        {
            StopTimer();
            _EventDispatcher.AnimationCompleted -= RestartTimer;
        }
    }
}
