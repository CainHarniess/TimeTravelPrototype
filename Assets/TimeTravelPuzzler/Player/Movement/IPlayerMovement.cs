using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Player.Movement
{
    public interface IPlayerMovement
    {
        bool CanMove(Vector2 movementDirection);
        void Move(Vector2 movementDirection);
    }
}