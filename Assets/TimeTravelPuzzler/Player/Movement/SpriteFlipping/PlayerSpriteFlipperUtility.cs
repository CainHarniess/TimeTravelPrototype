using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Player.Movement
{
    [CreateAssetMenu(fileName = AssetMenu.PlayerSpriteFlipperFileName, menuName = AssetMenu.PlayerSpriteFlipperPath)]
    public class PlayerSpriteFlipperUtility : SpriteFlipperUtility
    {
        public override void FlipSprite(SpriteRenderer sprite, Vector2 movementDirection)
        {
            if (movementDirection.x < 0)
            {
                sprite.flipX = true;
            }
            else if (movementDirection.x > 0)
            {
                sprite.flipX = false;
            }
        }
    }
}
