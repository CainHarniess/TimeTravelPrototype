using Osiris.Utilities.Logging;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris
{
    public abstract class LoggableMonoBehaviour : OsirisMonoBehaviour, ILoggableBehaviour
    {
        [SerializeField] private UnityConsoleLogger _Logger;

        public ILogger Logger => _Logger;
    }
}
