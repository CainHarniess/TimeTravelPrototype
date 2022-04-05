using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class PrimitiveFloorPadSpriteHandler : ISpriteHandler
    {
        private SpriteRenderer _spriteRenderer;
        private Color _colour;
        private Color _pressedColour;

        public PrimitiveFloorPadSpriteHandler(SpriteRenderer spriteRenderer)
        {
            _spriteRenderer = spriteRenderer;
            _colour = spriteRenderer.color;
            _pressedColour = Color.grey;
        }

        public void OnPress()
        {
            _spriteRenderer.color = _pressedColour;
        }

        public void OnRelease()
        {
            _spriteRenderer.color = _colour;
        }
    }
}
