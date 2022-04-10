using UnityEngine;

namespace Osiris.Testing
{
    /// <summary>
    /// Wraps objects derived from <c>UnityEngine.Behaviour</c> to support unit testing.
    /// </summary>
    public class BehaviourProxy : ComponentProxy<Behaviour>, IBehaviourProxy
    {
        public BehaviourProxy(Behaviour behaviour) : base(behaviour)
        {
        
        }

        public bool Enabled
        {
            get => Component.enabled;
            set => Component.enabled = value;
        }
    }
}
