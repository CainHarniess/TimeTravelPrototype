using Osiris.Testing.Abstractions;
using Osiris.TimeTravelPuzzler.Commands;
using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Movement;
using Osiris.Utilities.Events;
using Osiris.Utilities.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Player
{
    public class PlayerMovement : IPlayerMovement
    {
        private ICollider2DProxy _collider;
        private float _castDistance;
        private ICollection<IMoveable> _currentMovables;
        private ITransformProxy _transformProxy;
        private ITransformProxy _cloneTransfromProxy;
        private IEventChannelSO<IRewindableCommand> _RecordableActionOccurred;


        public PlayerMovement(ICollider2DProxy collider, float castDistance, ITransformProxy transform,
                              ITransformProxy cloneTransfrom,
                              IEventChannelSO<IRewindableCommand> recordableActionOccurred)
        {
            _collider = collider;
            _castDistance = castDistance;
            _currentMovables = new List<IMoveable>(8);
            _transformProxy = transform;
            _cloneTransfromProxy = cloneTransfrom;
            _RecordableActionOccurred = recordableActionOccurred;
        }

        public bool CanMove(Vector2 movementDirection)
        {
            RaycastHit2D[] castResults = new RaycastHit2D[8];
            int resultCount = _collider.Cast(movementDirection, castResults, _castDistance);
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

                if (result.collider.isTrigger)
                {
                    continue;
                }

                if (!(result.collider.GetComponent<IMoveable>() is IMoveable movable))
                {
                    return false;
                }

                if (!movable.CanMove(movementDirection))
                {
                    return false;
                }

                _currentMovables.Add(movable);
            }
            return true;
        }

        public void Move(Vector2 movementDirection)
        {
            var movementCommand = new PlayerMovementCommand(_transformProxy.Component,
                                                            movementDirection.ToVector3(),
                                                            _cloneTransfromProxy.Component);

            var recordedMovement = new MovementCommand(_cloneTransfromProxy.Component,
                                                       movementDirection.ToVector3());
            recordedMovement.UpdateInverse();

            movementCommand.Execute();
            _RecordableActionOccurred.Raise(recordedMovement);

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
