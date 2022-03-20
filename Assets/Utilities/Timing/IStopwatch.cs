namespace Osiris.Utilities.Timing
{
    public interface IStopwatch
    {
        float DeltaTime { get; }
        float StartTime { get; }
        float StopTime { get; }

        void Start();
        void Stop();
    }
}