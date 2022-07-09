using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Timeline;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Osiris.TimeTravelPuzzler.Player.Movement
{
    public class PlayerMovementControl : PlayerControl
    {
        private InputAction _movementAction;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private PlayerMovementChannel _PlayerMoveButtonPressed;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private RewindEventChannelSO _RewindStarted;
        [SerializeField] private RewindEventChannelSO _RewindCompleted;
        [SerializeField] private RewindEventChannelSO _PlayerRewindCancelled;

        protected override void Awake()
        {
            base.Awake();
            _movementAction = PlayerInput.actions[ControlActions.Movement];
        }

        private void OnMovementPerformed(InputAction.CallbackContext obj)
        {
            if (!IsControlActive)
            {
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
