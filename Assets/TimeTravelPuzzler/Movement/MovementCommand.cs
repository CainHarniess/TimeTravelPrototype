﻿using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.Utilities.Commands;
using Osiris.Utilities.Extensions;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Commands
{
    public class MovementCommand : CommandBase, IRewindableCommand
    {
        private Transform _transform;
        private Vector3 _movementDirection;

        public MovementCommand(Transform transformToMove, Vector3 movementDirection)
        {
            _transform = transformToMove;
            _movementDirection = movementDirection;
        }

        protected Transform Transform => _transform;
        protected Vector3 MovementDirection => _movementDirection;

        public ICommand Inverse { get; protected set; }
        public Vector2 Direction => _movementDirection.ToVector2();

        public override string Description => $"Move - {Direction}";

        public override bool CanExecute(object parameter = null)
        {
            return true;
        }

        public override void Execute(object parameter = null)
        {
            _transform.position += _movementDirection;
            UpdateInverse();
        }

        public virtual void UpdateInverse()
        {
            Inverse = new MovementCommand(_transform, -_movementDirection);
        }
    }
}
