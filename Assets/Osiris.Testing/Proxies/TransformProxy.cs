using Osiris.Testing.Abstractions;
using UnityEngine;

namespace Osiris.Testing
{
    public class TransformProxy : ComponentProxy<Transform>, ITransformProxy
    {
        public TransformProxy(Transform transform) : base(transform)
        {

        }

        public Vector3 Position
        { 
            get => Component.position;
            set => Component.position = value;
        }
    }
}
