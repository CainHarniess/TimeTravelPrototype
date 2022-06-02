using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Timeline;
using Osiris.Utilities.Logging;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Osiris.TimeTravelPuzzler.Player
{
    public class PlayerRewindControl : PlayerControl, ILoggableBehaviour
    {
        private InputAction _rewindAction;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private RewindEventChannelSO _PlayerRewindRequested;
        [SerializeField] private RewindEventChannelSO _PlayerRewindCancelled;

        protected override void Awake()
        {
            base.Awake();
            _rewindAction = PlayerInput.actions[ControlActions.RewindTime];
        }

        private void OnRewindStarted(InputAction.CallbackContext obj)
        {
            if (!IsControlActive)
            {
                return;
            }
            _PlayerRewindRequested.Raise();
        }

        private void OnRewindCancelled(InputAction.CallbackContext obj)
        {
            if (!IsControlActive)
            {
                return;
            }
            _PlayerRewindCancelled.Raise();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _rewindAction.performed += OnRewindStarted;
            _rewindAction.canceled += OnRewindCancelled;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _rewindAction.performed -= OnRewindStarted;
            _rewindAction.canceled -= OnRewindCancelled;
        }
    }
}
