using Osiris.Testing;
using Osiris.Testing.Abstractions;
using Osiris.TimeTravelPuzzler.Player.Movement;
using Osiris.Utilities.ScriptableObjects;
using System;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

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

        public float Vector2EqualsThreshold { get; private set; }
        public PlayerMovementBuilder WithVector2EqualsThreshold(float vector2EqualsThreshold)
        {
            Vector2EqualsThreshold = vector2EqualsThreshold;
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

        public ILogger Logger { get; private set; }
        public PlayerMovementBuilder WithLogger(ILogger logger)
        {
            Logger = logger;
            return this;
        }

        public string GameObjectName { get; private set; }
        public PlayerMovementBuilder WithGameObjectName(string gameObjectName)
        {
            GameObjectName = gameObjectName;
            return this;
        }

        public IPlayerMovement Build()
        {
            return new PlayerMovement(Collider2D, CastDistance, Transform, Logger, GameObjectName,
                                      Vector2EqualsThreshold);
        }
    }
}
