using Osiris.TimeTravelPuzzler.Player.Movement;
using Osiris.Utilities.ScriptableObjects;
using ILogger = Osiris.Utilities.Logging.ILogger;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Player
{
    [CreateAssetMenu(fileName = AssetMenu.MovementBuildDirectorFileName, menuName = AssetMenu.MovementBuildDirectorPath)]
    public class PlayerMovementBuildDirector : DescriptionSO
    {
        [SerializeField] private PlayerMovementBuilder _Builder;
        public IPlayerMovement Construct(Collider2D collider, float castDistance, Transform transform, ILogger logger, string gameObjectName)
        {
            return _Builder.WithCollider2D(collider).WithCastDistance(castDistance).WithTransform(transform)
                           .WithLogger(logger).WithGameObjectName(gameObjectName).Build();
        }
    }
}
