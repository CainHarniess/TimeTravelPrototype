using Osiris.Testing.Abstractions;
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

    }

    public class TransformProxy : ComponentProxy<Transform>, ITransformProxy
    {
        public TransformProxy(Transform transform) : base(transform)
        {

        }

        public Vector3 Position { get; set; }
    }
}
