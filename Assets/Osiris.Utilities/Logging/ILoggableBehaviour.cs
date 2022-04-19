using Osiris.Utilities.Extensions;

namespace Osiris.Utilities.Logging
{
    public interface ILoggableBehaviour : IInjectableBehaviour
    {
        ILogger Logger { get; }
    }
}
