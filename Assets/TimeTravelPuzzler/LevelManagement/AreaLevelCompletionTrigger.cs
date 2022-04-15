using Osiris.EditorCustomisation;
using Osiris.TimeTravelPuzzler.Interactables;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Game
{
    public class AreaLevelCompletionTrigger : MonoBehaviour, ILevelCompletionTrigger
    {
        [SerializeField] private GoalSpriteAnimator _spriteAnimator;

        [Header(InspectorHeaders.BroadcastsOn)]
        [SerializeField] private LevelCompletionEventChannel _levelCompletion;

        private void Awake()
        {
            var sprite = GetComponent<SpriteRenderer>();
            
            if (sprite == null)
            {
                Debug.LogWarning("No sprite component attached to Area Level Completion trigger.");
                return;
            }

            _spriteAnimator = new GoalSpriteAnimator(sprite);
        }
        public void TriggerLevelCompletion()
        {
            _spriteAnimator.ChangeColour(Color.green);
            _levelCompletion.Raise();
        }

        public void UndoLevelCompletion()
        {
            _spriteAnimator.ChangeColour(Color.yellow);
        }
    }
}