namespace Osiris.Testing.Abstractions
{
    /// <summary>
    /// Interface for wrapping classes that derive from <c>UnityEngine.Behaviour</c> to support
    /// unit testing.
    /// </summary>
    /// <remarks>
    /// Ensure that the object to be wrapped derives from <c>Behaviour</c>. For example,
    /// <c>SpriteRenderer</c> also has an <c>enabled</c> property, but it does no derive
    /// from <c>Behaviour</c>.
    /// </remarks>
    public interface IBehaviourProxy
    {
        /// <summary>
        /// Property to wrap the <c>Behaviour.enabled</c> property.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
