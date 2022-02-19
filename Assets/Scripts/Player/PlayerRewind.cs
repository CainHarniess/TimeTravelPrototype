using Osiris.TimeTravelPuzzler.Editor;
using Osiris.TimeTravelPuzzler.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Osiris.TimeTravelPuzzler.Player
{
    public class PlayerRewind : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private InputAction _rewindAction;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private RewindEventChannelSO _rewindEventChannel;

        void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _rewindAction = _playerInput.actions["RewindTime"];
        }

        private void OnRewindPerformed(InputAction.CallbackContext obj)
        {
            _rewindEventChannel.Raise();
        }

        private void OnEnable()
        {
            _rewindAction.performed += OnRewindPerformed;
        }

        private void OnDisable()
        {
            _rewindAction.performed -= OnRewindPerformed;
        }
    }
}
