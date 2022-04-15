using Osiris.EditorCustomisation;
using Osiris.GameManagement;
using Osiris.TimeTravelPuzzler.Timeline;
using Osiris.Utilities.References;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Osiris.TimeTravelPuzzler.Player
{
    public class PlayerRewindControl : PlayerControl
    {
        private PlayerInput _playerInput;
        private InputAction _rewindAction;
        private IEnumerator _rewindTimer;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private FloatReference _MaximumRewindTimeRef;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private CloneInitialiser _CloneInitialiser;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private RewindEventChannelSO _PlayerRewindRequested;
        [SerializeField] private RewindEventChannelSO _PlayerRewindCancelled;

        void Awake()
        {
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

            // TODO:    The TimelineManager should be responsible for activating the clone once
            //          the rewind request has been approved.
            _CloneInitialiser.Activate(transform.position);

            _rewindTimer = RewindTimer();
            StartCoroutine(_rewindTimer);
        }

        private IEnumerator RewindTimer()
        {
            yield return new WaitForSeconds(_MaximumRewindTimeRef.Value);
            _PlayerRewindCancelled.Raise();
        }

        private void OnRewindCancelled(InputAction.CallbackContext obj)
        {
            if (!IsControlActive)
            {
                return;
            }

            if (_rewindTimer != null)
            {
                StopCoroutine(_rewindTimer);
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
