using UnityEngine;

namespace Osiris.Testing.Abstractions
{
    /// <summary>
    /// Interface for wrapping classes that derive from <c>UnityEngine.Collider2D</c> to support
    /// unit testing.
    /// </summary>
    public interface ICollider2DProxy : IBehaviourProxy
    {
        public int Cast(Vector2 direction, RaycastHit2D[] results, float distance);
    }
}
