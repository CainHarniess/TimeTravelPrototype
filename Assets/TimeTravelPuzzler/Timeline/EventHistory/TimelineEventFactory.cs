using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Timeline.Core;
using UnityEngine;
using ILogger = Osiris.Utilities.Logging.ILogger;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    public class TimelineEventFactory : ITimelineEventFactory<ITimelineEvent>
    {
        private readonly ILogger _logger;

        public TimelineEventFactory(ILogger logger)
        {
            _logger = logger;
        }

        public ITimelineEvent Create(IRewindableCommand rewindableCommand)
        {
            return new TimelineEvent(Time.time, rewindableCommand, _logger);
        }
    }
}
