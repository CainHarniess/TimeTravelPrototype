using UnityEngine;

namespace Osiris.Testing
{
    public class SpriteRendererProxy : ComponentProxy<SpriteRenderer>, ISpriteRendererProxy
    {
        public SpriteRendererProxy(SpriteRenderer spriteRenderer) : base(spriteRenderer)
        {

        }

        public bool Enabled
        {
            get => Component.enabled;
            set => Component.enabled = value;
        }

        public Color Colour
        {
            get => Component.color;
            set => Component.color = value;
        }
    }
}
