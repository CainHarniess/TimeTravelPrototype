using Osiris.Testing.Abstractions;
using UnityEngine;

namespace Osiris.Testing
{
    /// <summary>
    /// Wraps objects derived from <c>UnityEngine.Component</c> to support unit testing.
    /// </summary>
    public class ComponentProxy<T> : IComponentProxy<T>
        where T : Component
    {
        private T _component;

        public ComponentProxy(T component)
        {
            _component = component;
        }
        public T Component => _component;
    }
}
