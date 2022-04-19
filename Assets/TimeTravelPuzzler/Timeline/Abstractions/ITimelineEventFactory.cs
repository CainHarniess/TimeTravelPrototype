using Osiris.TimeTravelPuzzler.Core.Commands;

namespace Osiris.TimeTravelPuzzler.Timeline.Core
{
    public interface ITimelineEventFactory<T>
    {
        T Create(IRewindableCommand rewindableCommand);
    }
}