using Osiris.Testing.Abstractions;
using Osiris.TimeTravelPuzzler.Core;
using Osiris.TimeTravelPuzzler.Movement;
using Osiris.Utilities.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Player.Movement
{
    public class PlayerMovement : IPlayerMovement
    {
        private ICollider2DProxy _collider;
        private float _castDistance;
        private ICollection<IMoveable> _currentMovables;
        private ITransformProxy _transformProxy;
        private ILogger _Logger;
        private string _gameObjectName;

        public PlayerMovement(ICollider2DProxy collider, float castDistance, ITransformProxy transform, ILogger logger,
                              string gameObjectName)
        {
            _collider = collider;
            _castDistance = castDistance;
            _currentMovables = new List<IMoveable>(8);
            _transformProxy = transform;
            _Logger = logger;
            _gameObjectName = gameObjectName;
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

            if (!castResults.Any(r => r.normal == -movementDirection))
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
