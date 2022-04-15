using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.Utilities.Events;
using Osiris.Utilities.ScriptableObjects;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Player
{
    [CreateAssetMenu(fileName = AssetMenu.MovementBuildDirectorFileName, menuName = AssetMenu.MovementBuildDirectorPath)]
    public class PlayerMovementBuildDirector : DescriptionSO
    {
        [SerializeField] private PlayerMovementBuilder _Builder;
        public IPlayerMovement Construct(Collider2D collider, float castDistance, Transform transform,
                                         Transform cloneTransfrom,
                                         IEventChannelSO<IRewindableCommand> timelineEventChannel)
        {
            return _Builder.WithCollider2D(collider).WithCastDistance(castDistance).WithTransform(transform)
                           .WithCloneTransform(cloneTransfrom).WithTimelineEventChannel(timelineEventChannel).Build();
        }
    }
}
