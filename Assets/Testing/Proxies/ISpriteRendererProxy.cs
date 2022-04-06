using UnityEngine;

namespace Osiris.Testing
{
    public interface ISpriteRendererProxy : IRendererProxy
    {
        Color Colour { get; set; }
    }
}
