using UnityEngine;

namespace Osiris.Testing.Abstractions
{
    public interface ISpriteRendererProxy : IRendererProxy
    {
        Color Colour { get; set; }
    }
}
