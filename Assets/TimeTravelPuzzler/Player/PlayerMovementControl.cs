using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Commands;
using Osiris.TimeTravelPuzzler.Movement;
using Osiris.TimeTravelPuzzler.Timeline;
using Osiris.Utilities.Extensions;
using Osiris.Utilities.References;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Osiris.TimeTravelPuzzler.Player
{

    public class PlayerMovementControl : PlayerControl
    {
        private PlayerInput _playerInput;
        private InputAction _movementAction;
        private BoxCollider2D _collider;
        private Transform _transform;
        private List<IMoveable> _currentMovables;
        
        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private FloatReference _colliderCastDistance;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private Transform _cloneTransfrom;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private TimelineActionChannel _RecordableActionOccurred;

        void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _collider = GetComponent<BoxCollider2D>();
            _movementAction = _playerInput.actions[ControlActions.Movement];
            _transform = transform;
            _currentMovables = new List<IMoveable>(8);
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
            RaycastHit2D[] castResults = new RaycastHit2D[8];
            int resultCount = _collider.Cast(movementDirection, castResults, _colliderCastDistance.Value);
            if (resultCount == 0)
            {
                return true;
            }

            if (!castResults.Any(r => r.normal == -movementDirection))
            {
                return true;
            }

            for (int i = 0; i < resultCount; i++)
            {
                RaycastHit2D result = castResults[i];

                if (result.collider.isTrigger)
                {
                    continue;
                }

                if (!(result.collider.GetComponent<IMoveable>() is IMoveable movable))
                {
                    return false;
                }

                if (!movable.CanMove(movementDirection))
                {
                    return false;
                }

                _currentMovables.Add(movable);
            }
            return true;
        }

        public void Move(Vector2 movementDirection)
        {
            var movementCommand = new PlayerMovementCommand(_transform,
                                                            movementDirection.ToVector3(),
                                                            _cloneTransfrom);

            var recordedMovement = new MovementCommand(_cloneTransfrom,
                                                       movementDirection.ToVector3());
            recordedMovement.UpdateInverse();

            movementCommand.Execute();
            _RecordableActionOccurred.Raise(recordedMovement);

            if (_currentMovables.Count == 0)
            {
                return;
            }
            foreach (IMoveable movable in _currentMovables)
            {
                movable.Move(movementDirection);
            }
            _currentMovables.Clear();
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
