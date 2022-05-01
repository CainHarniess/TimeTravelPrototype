using Osiris.Utilities.DependencyInjection;

namespace Osiris.Utilities.Logging
{
    public interface ILoggableBehaviour : IInjectableBehaviour
    {
        ILogger Logger { get; }
    }
}
