using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Timeline;
using Osiris.Utilities.Events;
using Osiris.Utilities.Logging;
using Osiris.Utilities.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Osiris.TimeTravelPuzzler.Player
{
    public class PlayerRewindControl : PlayerControl, ILoggableBehaviour
    {
        private InputAction _rewindAction;

        [Header(InspectorHeaders.ReadsFrom)]
        [SerializeField] private BoolVariableSO _IsRewinding;

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

        private void OnGameResumed()
        {
            if (_rewindAction.ReadValue<float>() == 1)
            {
                Logger.Log("Rewind button pressed at resume.", GameObjectName);
                return;
            }
            Logger.Log("Rewind button not pressed at resume.", GameObjectName);

            if (!_IsRewinding.Value)
            {
                Logger.Log("Not rewinding at resume.", GameObjectName);
                return;
            }
            _PlayerRewindCancelled.Raise();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _rewindAction.performed += OnRewindStarted;
            _rewindAction.canceled += OnRewindCancelled;
            GameUnpaused.Event += OnGameResumed;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _rewindAction.performed -= OnRewindStarted;
            _rewindAction.canceled -= OnRewindCancelled;
            GameUnpaused.Event -= OnGameResumed;
        }
    }
}
