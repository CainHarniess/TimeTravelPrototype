using Osiris.Testing;
using Osiris.TimeTravelPuzzler.Interactables.FloorPads.Core;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Interactables.FloorPads
{
    public class PrimitiveFloorPadSpriteHandler : IFloorPadSpriteHandler
    {
        private ISpriteRendererProxy _spriteRenderer;
        private Color _colour;
        private Color _pressedColour;

        public PrimitiveFloorPadSpriteHandler(ISpriteRendererProxy spriteRenderer)
        {
            _spriteRenderer = spriteRenderer;
            _colour = spriteRenderer.Colour;
            _pressedColour = Color.grey;
        }

        public void OnPress()
        {
            _spriteRenderer.Colour = _pressedColour;
        }

        public void OnRelease()
        {
            _spriteRenderer.Colour = _colour;
        }
    }
}
