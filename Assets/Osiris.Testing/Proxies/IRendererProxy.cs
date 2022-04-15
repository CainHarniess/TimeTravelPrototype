namespace Osiris.Testing
{
    /// <summary>
    /// Interface for wrapping classes that derive from <c>UnityEngine.Renderer</c> to support
    /// unit testing.
    /// </summary>
    public interface IRendererProxy
    {
        /// <summary>
        /// Property to wrap the <c>Behaviour.enabled</c> property.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
