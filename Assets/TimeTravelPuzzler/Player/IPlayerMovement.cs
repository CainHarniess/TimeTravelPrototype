using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Player
{
    public interface IPlayerMovement
    {
        bool CanMove(Vector2 movementDirection);
        void Move(Vector2 movementDirection);
    }
}