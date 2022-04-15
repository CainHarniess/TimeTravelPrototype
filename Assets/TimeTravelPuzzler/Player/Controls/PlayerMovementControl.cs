using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Commands;
using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Timeline;
using Osiris.Utilities.References;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Osiris.TimeTravelPuzzler.Player
{

    public class PlayerMovementControl : PlayerControl, IPlayerMovement
    {
        private PlayerInput _playerInput;
        private InputAction _movementAction;

        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private FloatReference _colliderCastDistance;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeReference] private IPlayerMovement _playerMovement;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private Transform _cloneTransfrom;
        [SerializeField] private PlayerMovementBuildDirector _movementBuildDirector;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private TimelineActionChannel _TimelineEventChannel;

        void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _movementAction = _playerInput.actions[ControlActions.Movement];
            
            var collider = GetComponent<BoxCollider2D>();
            _playerMovement = _movementBuildDirector.Construct(collider, _colliderCastDistance.Value, transform,
                                                               _cloneTransfrom, _TimelineEventChannel);
        }

        private void OnMovementPerformed(InputAction.CallbackContext obj)
        {
            if (!IsControlActive)
            {
                return;
            }

            Vector2 movementDirection = obj.ReadValue<Vector2>();

            if (!CanMove(movementDirection))
            {
                return;
            }
            Move(movementDirection);
        }

        public bool CanMove(Vector2 movementDirection)
        {
            return _playerMovement.CanMove(movementDirection);
        }

        public void Move(Vector2 movementDirection)
        {
            _playerMovement.Move(movementDirection);
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
