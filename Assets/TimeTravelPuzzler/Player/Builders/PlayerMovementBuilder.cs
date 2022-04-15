using Osiris.Testing;
using Osiris.Testing.Abstractions;
using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.Utilities.Events;
using Osiris.Utilities.ScriptableObjects;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Player
{
    [CreateAssetMenu(fileName = AssetMenu.MovementBuilderFileName, menuName = AssetMenu.MovementBuilderPath)]
    public class PlayerMovementBuilder : DescriptionSO
    {
        public ICollider2DProxy Collider2D { get; private set; }
        public PlayerMovementBuilder WithCollider2D(Collider2D collider)
        {
            Collider2D = new Collider2DProxy(collider);
            return this;
        }

        public float CastDistance { get; private set; }
        public PlayerMovementBuilder WithCastDistance(float castDistance)
        {
            CastDistance = castDistance;
            return this;
        }

        public ITransformProxy Transform { get; private set; }
        public PlayerMovementBuilder WithTransform(Transform transform)
        {
            Transform = new TransformProxy(transform);
            return this;
        }

        public ITransformProxy CloneTransform { get; private set; }
        public PlayerMovementBuilder WithCloneTransform(Transform transform)
        {
            CloneTransform = new TransformProxy(transform);
            return this;
        }

        public IEventChannelSO<IRewindableCommand> TimelineEventChannel { get; private set; }
        public PlayerMovementBuilder WithTimelineEventChannel(IEventChannelSO<IRewindableCommand> timelineEventChannel)
        {
            TimelineEventChannel = timelineEventChannel;
            return this;
        }

        public IPlayerMovement Build()
        {
            return new PlayerMovement(Collider2D, CastDistance, Transform, CloneTransform,
                                      TimelineEventChannel);
        }
    }
}
