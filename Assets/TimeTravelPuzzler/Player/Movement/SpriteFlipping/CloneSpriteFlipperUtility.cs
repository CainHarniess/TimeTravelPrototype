using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Timeline;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Player.Movement
{
    [CreateAssetMenu(fileName = AssetMenu.CloneSpriteFlipperFileName, menuName = AssetMenu.CloneSpriteFlipperPath)]
    public class CloneSpriteFlipperUtility : PlayerSpriteFlipperUtility
    {
        [Header(InspectorHeaders.DebugVariables)]
        [ReadOnly] [SerializeField] private bool IsRewind = false;

        [Header(InspectorHeaders.ListensTo)]
        [SerializeField] private RewindEventChannelSO _RewindStarted;
        [SerializeField] private RewindEventChannelSO _RewindCompleted;

        public override void FlipSpriteIfRequired(SpriteRenderer sprite, Vector2 movementDirection)
        {
            if (!IsRewind)
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

        private void OnRewindStarted()
        {
            IsRewind = true;
        }

        private void OnRewindCompleted()
        {
            IsRewind = false;
        }

        private void OnEnable()
        {
            _RewindStarted.Event += OnRewindStarted;
            _RewindCompleted.Event += OnRewindCompleted;
        }

        private void OnDisable()
        {
            _RewindStarted.Event -= OnRewindStarted;
            _RewindCompleted.Event -= OnRewindCompleted;
        }
    }
}
