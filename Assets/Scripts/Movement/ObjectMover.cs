using Osiris.TimeTravelPuzzler.Commands;
using Osiris.TimeTravelPuzzler.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Extensions;
using Osiris.TimeTravelPuzzler.Timeline;
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
        [SerializeField] private TimelineEventChannelSO _timelineEventChannel;

        public ObjectMover(Transform transform,
                     BoxCollider2D collider,
                     float colliderCastDistance,
                     TimelineEventChannelSO timelineEventChannel)
        {
            _transform = transform;
            _collider = collider;
            _timelineEventChannel = timelineEventChannel;
            _colliderCastDistance = colliderCastDistance;
        }

        protected Transform Transform { get => _transform; }
        protected BoxCollider2D Collider { get => _collider; }
        protected TimelineEventChannelSO TimelineEventChannel { get => _timelineEventChannel; }
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
                //Debug.Log("Box Cast is to trigger collider.");
                continue;
            }

            return true;
        }

        public virtual void Move(Vector2 movementDirection)
        {
            var movementCommand = new MovementCommand(_transform,
                                                      movementDirection.ToVector3());
            movementCommand.Execute();
            _timelineEventChannel.RecordTimelineEvent(movementCommand);
        }
    }
}
