using System;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler
{
    public class GoalSpriteAnimator// : MonoBehaviour
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
