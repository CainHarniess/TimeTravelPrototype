using System.Linq;
using Osiris.TimeTravelPuzzler.Commands;
using Osiris.TimeTravelPuzzler.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Extensions;
using Osiris.TimeTravelPuzzler.Movement;
using Osiris.TimeTravelPuzzler.Timeline;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Osiris.TimeTravelPuzzler.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private InputAction _movementAction;
        private BoxCollider2D _collider;
        private Transform _transform;
        private float _colliderCastDistance = 1f;
        private List<IMoveable> _currentMovables;

        [SerializeField] private Transform _cloneTransfrom;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private TimelineEventChannelSO _TimelineEventChannel;

        void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _collider = GetComponent<BoxCollider2D>();
            _movementAction = _playerInput.actions["Movement"];
            _transform = transform;
            _currentMovables = new List<IMoveable>(8);
        }

        private void OnMovementPerformed(InputAction.CallbackContext obj)
        {
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
            int resultCount = _collider.Cast(movementDirection, castResults, _colliderCastDistance);
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
            movementCommand.Execute();
            _TimelineEventChannel.RecordTimelineEvent(movementCommand);

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

        private void OnEnable()
        {
            _movementAction.performed += OnMovementPerformed;
        }

        private void OnDisable()
        {
            _movementAction.performed -= OnMovementPerformed;
        }
    }
}
