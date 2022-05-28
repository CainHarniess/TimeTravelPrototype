using Osiris.EditorCustomisation;
using Osiris.Testing.Abstractions;
using Osiris.TimeTravelPuzzler.Core;
using Osiris.TimeTravelPuzzler.Movement;
using Osiris.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Player.Movement
{
    public class PlayerMovement : IPlayerMovement
    {
        private readonly ICollider2DProxy _collider;
        private readonly float _castDistance;
        private readonly ICollection<IMoveable> _currentMovables;
        private readonly ITransformProxy _transformProxy;
        private readonly ILogger _Logger;
        private readonly string _gameObjectName;
        private readonly float _vector2EqualsThreshold;

        public PlayerMovement(ICollider2DProxy collider, float castDistance, ITransformProxy transform, ILogger logger,
                              string gameObjectName, float vector2EqualsThreshold)
        {
            _collider = collider;
            _castDistance = castDistance;
            _currentMovables = new List<IMoveable>(8);
            _transformProxy = transform;
            _Logger = logger;
            _gameObjectName = gameObjectName;
            _vector2EqualsThreshold = vector2EqualsThreshold;
        }

        public bool CanMove(Vector2 movementDirection)
        {
            RaycastHit2D[] castResults = new RaycastHit2D[8];
            int resultCount = _collider.Cast(movementDirection, castResults, _castDistance);
            if (resultCount == 0)
            {
                _Logger.Log("Move request approved. Collider cast returned no results.", _gameObjectName);
                return true;
            }

            if (!castResults.Any(r => Vector2.Distance(-movementDirection, r.normal) < _vector2EqualsThreshold))
            {
                _Logger.Log("Move request approved. No collider cast results perpendicular to movement direction.", _gameObjectName);
                return true;
            }

            for (int i = 0; i < resultCount; i++)
            {
                RaycastHit2D result = castResults[i];

                if (result.collider.isTrigger)
                {
                    continue;
                }
                string otherTag = result.collider.gameObject.tag;
                if (otherTag == Tags.Player || otherTag == Tags.PlayerClone)
                {
                    _Logger.Log("Collider cast result is with player or clone.", _gameObjectName);
                    continue;
                }

                if (!(result.collider.GetComponent<IMoveable>() is IMoveable movable))
                {
                    _Logger.Log("Collider cast result does not implement IMoveable.", _gameObjectName);
                    return false;
                }

                if (!movable.CanMove(movementDirection))
                {
                    _Logger.Log("Movable cast result cannot be moved.", _gameObjectName);
                    return false;
                }

                _currentMovables.Add(movable);
            }
            _Logger.Log("Move request approved.", _gameObjectName);
            return true;
        }

        [Obsolete("Continuous player movement applied by PlayerMovementBehaviour class in favour of this method.")]
        public void Move(Vector2 movementDirection)
        {
            _transformProxy.Position += movementDirection.ToVector3();

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
    }
}
