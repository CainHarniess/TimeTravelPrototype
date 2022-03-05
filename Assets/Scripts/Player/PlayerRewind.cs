using Osiris.TimeTravelPuzzler.EditorCustomisation;
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

        [SerializeField] private CloneInitialiser _cloneInitialiser;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private float _maximumRewindTime = 7f;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private RewindEventChannelSO _rewindEventChannel;

        void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _rewindAction = _playerInput.actions["RewindTime"];
        }

        private void OnRewindStarted(InputAction.CallbackContext obj)
        {
            _rewindEventChannel.RaiseRewindRequest();

            _cloneInitialiser.Activate(transform.position);

            _rewindTimer = RewindTimer();
            StartCoroutine(_rewindTimer);
        }

        private IEnumerator RewindTimer()
        {
            yield return new WaitForSeconds(_maximumRewindTime);
            _rewindEventChannel.RaiseRewindCancellation();
        }

        private void OnRewindCancelled(InputAction.CallbackContext obj)
        {
            if (_rewindTimer != null)
            {
                StopCoroutine(_rewindTimer);
            }
            _rewindEventChannel.RaiseRewindCancellation();
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
