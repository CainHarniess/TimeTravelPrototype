using Osiris.EditorCustomisation;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Osiris.TimeTravelPuzzler.Player.Movement
{
    public class PlayerMovementControl : PlayerControl
    {
        private InputAction _movementAction;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private PlayerMovementChannel _PlayerMoveButtonPressed;

        protected override void Awake()
        {
            base.Awake();
            _movementAction = PlayerInput.actions[ControlActions.Movement];
        }

        private void OnMovementPerformed(InputAction.CallbackContext obj)
        {
            if (!IsControlActive)
            {
                return;
            }

            Vector2 movementDirection = obj.ReadValue<Vector2>();
            _PlayerMoveButtonPressed.Raise(movementDirection);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _movementAction.performed += OnMovementPerformed;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _movementAction.performed -= OnMovementPerformed;
        }
    }
}
