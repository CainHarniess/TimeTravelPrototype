namespace Osiris.Testing
{
    /// <summary>
    /// Interface for wrapping classes that derive from <c>UnityEngine.Collider</c> to support
    /// unit testing.
    /// </summary>
    public interface IColliderProxy
    {
        /// <summary>
        /// Property to wrap the <c>Collider.enabled</c> property.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
