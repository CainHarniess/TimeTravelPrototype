using Osiris.TimeTravelPuzzler.Core.Commands;

namespace Osiris.TimeTravelPuzzler.Timeline.Core
{
    public interface IRecorder
    {
        void Record(IRewindableCommand rewindableCommand);
        void StartRecording();
        void StopRecording();
    }
}