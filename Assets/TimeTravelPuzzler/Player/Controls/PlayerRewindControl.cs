using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Timeline;
using Osiris.Utilities.Extensions;
using Osiris.Utilities.Logging;
using UnityEngine;
using UnityEngine.InputSystem;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Player
{
    public class PlayerRewindControl : PlayerControl, ILoggableBehaviour
    {
        private PlayerInput _playerInput;
        private InputAction _rewindAction;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private UnityConsoleLogger _Logger;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private RewindEventChannelSO _PlayerRewindRequested;
        [SerializeField] private RewindEventChannelSO _PlayerRewindCancelled;

        public ILogger Logger => _Logger;

        protected override void Awake()
        {
            base.Awake();
            this.IsInjectionPresent(_Logger, nameof(_Logger));
            
            _playerInput = GetComponent<PlayerInput>();
            _rewindAction = _playerInput.actions[ControlActions.RewindTime];
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
