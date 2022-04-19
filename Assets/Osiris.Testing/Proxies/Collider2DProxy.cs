using Osiris.Testing.Abstractions;
using UnityEngine;

namespace Osiris.Testing
{
    /// <summary>
    /// Wrapper class for BoxCollider2D objects to support unit testing.
    /// </summary>
    public class Collider2DProxy : BehaviourProxy<Collider2D>, ICollider2DProxy
    {
        public Collider2DProxy(Collider2D behaviour) : base(behaviour)
        {

        }

        public int Cast(Vector2 direction, RaycastHit2D[] results, float distance)
        {
            return Component.Cast(direction, results, distance);
        }
    }
}
