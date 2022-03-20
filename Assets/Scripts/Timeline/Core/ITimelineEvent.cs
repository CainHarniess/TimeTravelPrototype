using Osiris.TimeTravelPuzzler.Core.Commands;

namespace Osiris.TimeTravelPuzzler.Timeline.Core
{
    public interface ITimelineEvent
    {
        IRewindableCommand EventAction { get; }
        float Time { get; }

        void Redo();
        void Undo();
    }
}