using Osiris.EditorCustomisation;
using Osiris.GameManagement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Osiris.TimeTravelPuzzler.Player
{
    public class PlayerPauseControl : PlayerControl
    {
        private InputAction _pauseAction;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private PauseEventChannel _PauseButtonPressed;

        protected override void Awake()
        {
            base.Awake();
            _pauseAction = PlayerInput.actions[ControlActions.Pause];
        }

        private void OnPausePerformed(InputAction.CallbackContext obj)
        {
            if (!IsControlActive)
            {
                return;
            }

            _PauseButtonPressed.Raise();
        }

        protected override void OnEnable()
        {
            _pauseAction.performed += OnPausePerformed;
            LevelCompleted.Event += Deactivate;
        }

        protected override void OnDisable()
        {
            _pauseAction.performed -= OnPausePerformed;
            LevelCompleted.Event -= Deactivate;
        }
    }
}
