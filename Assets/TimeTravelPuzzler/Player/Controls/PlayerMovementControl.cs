using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Timeline;
using Osiris.Utilities.Audio;
using Osiris.Utilities.DependencyInjection;
using Osiris.Utilities.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Osiris.TimeTravelPuzzler.Player.Movement
{
    public class PlayerMovementControl : PlayerControl
    {
        private InputAction _movementAction;


        [Header(InspectorHeaders.Injections)]
        [SerializeField] private AudioSource _AudioSource;
        [SerializeField] private Animator _Animator;
        [SerializeField] private AudioClipData _MovementErrorClip;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private PlayerMovementChannel _PlayerMoveButtonPressed;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private RewindEventChannelSO _RewindStarted;
        [SerializeField] private RewindEventChannelSO _RewindCompleted;
        [SerializeField] private RewindEventChannelSO _PlayerRewindCancelled;

        [Header(InspectorHeaders.ReadsFrom)]
        [SerializeField] private BoolVariableSO _IsRewinding;

        protected override void Awake()
        {
            base.Awake();
            _movementAction = PlayerInput.actions[ControlActions.Movement];
            this.AddComponentInjectionIfNotPresent(ref _AudioSource, nameof(_AudioSource));
            this.AddComponentInjectionIfNotPresent(ref _Animator, nameof(_Animator));
            this.IsInjectionPresent(_MovementErrorClip, nameof(_MovementErrorClip));
        }

        private void OnMovementPerformed(InputAction.CallbackContext obj)
        {
            if (!IsControlActive)
            {
                if (_IsRewinding.Value)
                {
                    _AudioSource.PlayOneShot(_MovementErrorClip.Clip);
                    _Animator.SetTrigger(AnimationParameters.RejectMovement);
                }
                return;
            }

            Vector2 movementDirection = obj.ReadValue<Vector2>();
            _PlayerMoveButtonPressed.Raise(movementDirection);
        }

        private void OnRewindStarted()
        {
            Deactivate();
        }

        private void OnRewindCompleted()
        {
            Activate();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _movementAction.performed += OnMovementPerformed;
            _RewindStarted.Event += OnRewindStarted;
            _RewindCompleted.Event += OnRewindCompleted;
            _PlayerRewindCancelled.Event += OnRewindCompleted;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _movementAction.performed -= OnMovementPerformed;
            _RewindStarted.Event -= OnRewindStarted;
            _RewindCompleted.Event -= OnRewindCompleted;
            _PlayerRewindCancelled.Event -= OnRewindCompleted;
        }
    }
}
