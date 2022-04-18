using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Player.Movement
{
    public abstract class SpriteFlipperUtility : ScriptableObject
    {
        public abstract void FlipSpriteIfRequired(SpriteRenderer sprite, Vector2 movementDirection);
    }
}
