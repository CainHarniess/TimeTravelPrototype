using Osiris.Testing.Abstractions;
using UnityEngine;

namespace Osiris.Testing
{
    /// <summary>
    /// Wraps objects derived from <c>UnityEngine.Behaviour</c> to support unit testing.
    /// </summary>
    public class BehaviourProxy : BehaviourProxy<Behaviour>
    {
        public BehaviourProxy(Behaviour behaviour) : base(behaviour)
        {
        
        }
    }

    public class BehaviourProxy<T> : ComponentProxy<T>, IBehaviourProxy
        where T : Behaviour
    {
        public BehaviourProxy(T component) : base(component)
        {

        }

        public bool Enabled
        {
            get => Component.enabled;
            set => Component.enabled = value;
        }
    }
}
