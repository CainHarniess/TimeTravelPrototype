using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables
{
    public class PressSpriteEffect
    {
        private SpriteRenderer _spriteRenderer;
        private Color _colour;
        private Color _pressedColour;

        public PressSpriteEffect(SpriteRenderer spriteRenderer)
        {
            _spriteRenderer = spriteRenderer;
            _colour = spriteRenderer.color;
            _pressedColour = Color.grey;
        }

        public void Darken()
        {
            _spriteRenderer.color = _pressedColour;
        }

        public void Lighten()
        {
            _spriteRenderer.color = _colour;
        }
    }
}
