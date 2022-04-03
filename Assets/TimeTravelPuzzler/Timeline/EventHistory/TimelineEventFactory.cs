using Osiris.TimeTravelPuzzler.Core.Commands;
using Osiris.TimeTravelPuzzler.Timeline.Core;
using UnityEngine;
using OUL = Osiris.Utilities.Logging;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    public class TimelineEventFactory : ITimelineEventFactory<ITimelineEvent>
    {
        private readonly OUL.ILogger _logger;

        public TimelineEventFactory(OUL.ILogger logger)
        {
            _logger = logger;
        }

        public ITimelineEvent Create(IRewindableCommand rewindableCommand)
        {
            return new TimelineEvent(Time.time, rewindableCommand, _logger);
        }
    }
}
