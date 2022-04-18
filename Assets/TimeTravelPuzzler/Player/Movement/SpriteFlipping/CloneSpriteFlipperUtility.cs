using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Player.Movement
{
    [CreateAssetMenu(fileName = AssetMenu.CloneSpriteFlipperFileName, menuName = AssetMenu.CloneSpriteFlipperPath)]
    public class CloneSpriteFlipperUtility : SpriteFlipperUtility
    {
        public override void FlipSprite(SpriteRenderer sprite, Vector2 movementDirection)
        {
            if (movementDirection.x < 0)
            {
                sprite.flipX = false;
            }
            else if (movementDirection.x > 0)
            {
                sprite.flipX = true;
            }
        }
    }
}
