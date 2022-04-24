using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Interactable;
using Osiris.Utilities;
using Osiris.Utilities.Extensions;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.Doors
{
    public class FlickerAnimationBehaviour : AnimationBehaviour
    {
        private Coroutine _timerCoroutine;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private float _MinFlickerDelay = 3f;
        [SerializeField] private float _MaxFlickerDelay = 8f;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private RandomCoroutineCallbackTimer _RandomTimer;

        private void Awake()
        {
            var validator = new RandomCoroutineTimerValidator();
            _RandomTimer = new RandomCoroutineCallbackTimer(Flicker, _MinFlickerDelay, _MaxFlickerDelay, validator);
        }

        public void Flicker()
        {
            Animator.SetTrigger(AnimationParameters.Flicker);
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
