using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Timeline;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Movement
{

    public class MoveableObject : MonoBehaviour, IMoveable
    {
        private ObjectMover _objectMover;
        private Transform _transform;
        private BoxCollider2D _collider;
        private float _ColliderCastDistance = 1;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private TimelineActionChannel _RecordableActionOccurred;

        private void Awake()
        {
            _transform = transform;
            _collider = GetComponent<BoxCollider2D>();
            _objectMover = new ObjectMover(_transform, _collider, _ColliderCastDistance, _RecordableActionOccurred);
        }

        public bool CanMove(Vector2 movementDirection)
        {
            return _objectMover.CanMove(movementDirection);
        }

        public void Move(Vector2 movementDirection)
        {
            _objectMover.Move(movementDirection);
        }
    }
}
