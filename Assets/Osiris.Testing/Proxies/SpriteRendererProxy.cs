using Osiris.Testing.Abstractions;
using UnityEngine;

namespace Osiris.Testing
{
    public class SpriteRendererProxy : RendererProxy<SpriteRenderer>, ISpriteRendererProxy
    {
        public SpriteRendererProxy(SpriteRenderer spriteRenderer) : base(spriteRenderer)
        {

        }

        public Color Colour
        {
            get => Component.color;
            set => Component.color = value;
        }
    }
}
