using Osiris.EditorCustomisation;
using Osiris.Utilities.Variables;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Player.Movement
{
    [CreateAssetMenu(fileName = AssetMenu.CloneSpriteFlipperFileName, menuName = AssetMenu.CloneSpriteFlipperPath)]
    public class CloneSpriteFlipperUtility : PlayerSpriteFlipperUtility
    {
        [Header(InspectorHeaders.DebugVariables)]
        [SerializeField] private BoolVariableSO _IsRewinding;

        public override void FlipSpriteIfRequired(SpriteRenderer sprite, Vector2 movementDirection)
        {
            if (!_IsRewinding.Value)
            {
                base.FlipSpriteIfRequired(sprite, movementDirection);
                return;
            }

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
