using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Commands;
using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Timeline;
using Osiris.Utilities.Extensions;
using System.Linq;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Movement
{
    [System.Serializable]
    public class ObjectMover : IMoveable
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private float _colliderCastDistance = 1;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private TimelineActionChannel _RecordableActionOccurred;

        public ObjectMover(Transform transform,
                           BoxCollider2D collider,
                           float colliderCastDistance,
                           TimelineActionChannel recordableActionOccuredChannel)
        {
            _transform = transform;
            _collider = collider;
            _RecordableActionOccurred = recordableActionOccuredChannel;
            _colliderCastDistance = colliderCastDistance;
        }

        protected Transform Transform { get => _transform; }
        protected BoxCollider2D Collider { get => _collider; }
        protected TimelineActionChannel TimelineEventChannel { get => _RecordableActionOccurred; }
        protected float ColliderCastDistance { get => _colliderCastDistance; }

        public virtual bool CanMove(Vector2 movementDirection)
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

                if (!result.collider.isTrigger)
                {
                    return false;
                }
                continue;
            }

            return true;
        }

        public virtual void Move(Vector2 movementDirection)
        {
            IRewindableCommand movementCommand = new MovementCommand(_transform,
                                                      movementDirection.ToVector3());
            movementCommand.Execute();
            _RecordableActionOccurred.Raise(movementCommand);
        }
    }
}
