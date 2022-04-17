namespace Osiris.Utilities.Logging
{
    public interface ILoggableBehaviour : IInjectableBehaviour
    {
        ILogger Logger { get; }
    }

    public interface IInjectableBehaviour : IMonoBehaviour
    {
    }

    public interface IMonoBehaviour
    {
        string GameObjectName { get; }
    }
}
