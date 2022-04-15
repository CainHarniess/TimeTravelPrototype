using Osiris.EditorCustomisation;
using Osiris.Utilities.Logging;
using UnityEngine;
using Osiris.GameManagement;
using UnityEngine.InputSystem;

namespace Osiris.TimeTravelPuzzler.Player
{
    public class PlayerPauseControl : PlayerControl
    {
        private string _gameObjectName;
        private PlayerInput _playerInput;
        private InputAction _pauseAction;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private UnityConsoleLogger _Logger;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private PauseEventChannel _PauseButtonPressed;

        void Awake()
        {
            _gameObjectName = gameObject.name;
            _playerInput = GetComponent<PlayerInput>();
            _pauseAction = _playerInput.actions[ControlActions.Pause];
        }

        private void OnPausePerformed(InputAction.CallbackContext obj)
        {
            _Logger.Log("Pause press received", _gameObjectName);
            _PauseButtonPressed.Raise();
        }

        protected override void DeactivateControl()
        {

        }

        protected override void ActivateControl()
        {

        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _pauseAction.performed += OnPausePerformed;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _pauseAction.performed -= OnPausePerformed;
        }
    }
}