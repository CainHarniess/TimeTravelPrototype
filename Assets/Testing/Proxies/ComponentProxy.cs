using UnityEngine;

namespace Osiris.Testing
{
    /// <summary>
    /// Wraps objects derived from <c>UnityEngine.Component</c> to support unit testing.
    /// </summary>
    public class ComponentProxy<T> where T : Component
    {
        private T _component;

        public ComponentProxy(T component)
        {
            _component = component;
        }

        protected T Component => _component;
    }
}
