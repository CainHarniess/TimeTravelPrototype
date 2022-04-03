using UnityEngine;

namespace Osiris.Testing
{
    public class RendererProxy : ComponentProxy<Renderer>, IRendererProxy
    {
        public RendererProxy(Renderer renderer) : base(renderer)
        {

        }

        public bool Enabled
        {
            get => Component.enabled;
            set => Component.enabled = value;
        }
    }
}
