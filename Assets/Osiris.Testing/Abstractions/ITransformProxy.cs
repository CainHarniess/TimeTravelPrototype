using UnityEngine;

namespace Osiris.Testing.Abstractions
{
    public interface IComponentProxy<T>
        where T : Component
    {
        public T Component { get; }
    }

    public interface ITransformProxy : IComponentProxy<Transform>
    {
        Vector3 Position { get; set; }
    }
}