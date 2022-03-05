using Osiris.TimeTravelPuzzler.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Osiris.TimeTravelPuzzler.Player
{
    public class PlayerRewind : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private InputAction _rewindAction;

        [SerializeField] private CloneInitialiser _cloneInitialiser;

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
        }

        private void OnRewindCancelled(InputAction.CallbackContext obj)
        {
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
