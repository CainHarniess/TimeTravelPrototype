using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Commands
{
    public class PlayerMovementCommand : MovementCommand
    {
        private Transform _altTransform;

        public PlayerMovementCommand(Transform transformToMove, Vector3 movementDirection, Transform altTransform)
            : base(transformToMove, movementDirection)
        {
            _altTransform = altTransform;
        }

        protected override void UpdateInverse()
        {
            Inverse = new MovementCommand(_altTransform, -MovementDirection);
        }
    }
}
