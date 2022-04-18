using System.Collections;

namespace Osiris.TimeTravelPuzzler.Timeline.Core
{
    public interface ITimelinePlayer
    {
        void Build(ListEventHistory eventHistory);
        bool CanPlay();
        IEnumerator Play(float rewindStartTime);
        bool CanStop();
        void Stop();
    }
}