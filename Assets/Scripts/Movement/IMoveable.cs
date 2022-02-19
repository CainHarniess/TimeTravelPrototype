using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Movement
{
    public interface IMoveable
    {
        bool CanMove(Vector2 movementDirection);
        void Move(Vector2 movementDirection);
    }
}
