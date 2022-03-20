using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Timeline;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Osiris.TimeTravelPuzzler.Player
{
    public class PlayerRewind : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private InputAction _rewindAction;

        private IEnumerator _rewindTimer;

        [SerializeField] private CloneInitialiser _CloneInitialiser;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private float _MaximumRewindTime = 7f;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private RewindEventChannelSO _RewindEventChannel;
        [SerializeField] private RewindEventChannelSO _PlayerRewindRequested;
        [SerializeField] private RewindEventChannelSO _PlayerRewindCancelled;

        void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _rewindAction = _playerInput.actions["RewindTime"];
        }

        private void OnRewindStarted(InputAction.CallbackContext obj)
        {
            _PlayerRewindRequested.Raise();

            // TODO:    The TimelineManager should be responsible for activating the clone once
            //          the rewind request has been approved.
            _CloneInitialiser.Activate(transform.position);

            _rewindTimer = RewindTimer();
            StartCoroutine(_rewindTimer);
        }

        private IEnumerator RewindTimer()
        {
            yield return new WaitForSeconds(_MaximumRewindTime);
            _PlayerRewindCancelled.Raise();
        }

        private void OnRewindCancelled(InputAction.CallbackContext obj)
        {
            if (_rewindTimer != null)
            {
                StopCoroutine(_rewindTimer);
            }
            _PlayerRewindCancelled.Raise();
        }

        private void OnEnable()
        {
            _rewindAction.performed += OnRewindStarted;
            _rewindAction.canceled += OnRewindCancelled;
        }

        private void OnDisable()
        {
            _rewindAction.performed -= OnRewindStarted;
            _rewindAction.canceled -= OnRewindCancelled;
        }
    }
}
