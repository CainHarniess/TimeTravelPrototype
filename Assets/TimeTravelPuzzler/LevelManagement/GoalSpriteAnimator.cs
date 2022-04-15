using UnityEngine;

namespace Osiris.TimeTravelPuzzler.LevelManagement
{
    public class GoalSpriteAnimator
    {
        private SpriteRenderer _sprite;

        public GoalSpriteAnimator(SpriteRenderer sprite)
        {
            _sprite = sprite;
        }

        public void ChangeColour(Color targetColour)
        {
            _sprite.color = targetColour;
        }
    }
}
