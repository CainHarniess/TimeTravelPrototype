using Osiris.EditorCustomisation;
using Osiris.Utilities.Logging;
using Osiris.Utilities.References;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Player.Movement
{
    public class PlayerMovementBehaviour : MonoBehaviour, IPlayerMovement
    {
        [Header(InspectorHeaders.ControlVariables)]
        [SerializeField] private FloatReference _colliderCastDistance;

        [Header(InspectorHeaders.DebugVariables)]
        [SerializeReference] private IPlayerMovement _playerMovement;

        [Header(InspectorHeaders.Injections)]
        [SerializeField] private PlayerMovementBuildDirector _movementBuildDirector;
        [SerializeField] private UnityConsoleLogger _Logger;

        void Awake()
        {
            if (_Logger == null)
            {
                ILoggerExtensions.MissingLoggerInjection(nameof(_Logger), gameObject.name);
            }
            var collider = GetComponent<BoxCollider2D>();
            _playerMovement = _movementBuildDirector.Construct(collider, _colliderCastDistance.Value, transform, _Logger,
                                                               gameObject.name);
        }

        public bool CanMove(Vector2 movementDirection)
        {
            return _playerMovement.CanMove(movementDirection);
        }

        public void Move(Vector2 movementDirection)
        {
            _playerMovement.Move(movementDirection);
        }
    }
}
