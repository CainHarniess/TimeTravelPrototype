using Osiris.EditorCustomisation;
using System;
using UnityEngine;

namespace Osiris.TimeTravelPuzzler.Timeline
{
    [Serializable]
    public class Stopwatch
    {
        [ReadOnly] [SerializeField] private float _startTime;
        public float StartTime { get => _startTime; }

        [ReadOnly] [SerializeField] private float _stopTime;
        public float StopTime { get => _stopTime;}

        [ReadOnly] [SerializeField] private float _deltaTime;
        public float DeltaTime { get => _deltaTime; }

        [ReadOnly] [SerializeField] private bool _isRunning;
        public bool IsRunning { get => _isRunning; }

        public void Start()
        {
            if (_isRunning)
            {
                throw new Exception("Stopwatch may not be started while it is running."
                                    + " Ensure the Stop method has been called.");
            }
            _startTime = Time.time;
            _isRunning = true;
        }

        public void Stop()
        {
            _stopTime = Time.time;
            _deltaTime = _stopTime - _startTime;
            _isRunning = false;
        }

        public void Reset()
        {
            _isRunning = false;
        }

        public void Lap()
        {
            if (!_isRunning)
            {
                throw new Exception("Lap may nat be called when the stopwatch is not running."
                                    + " Ensure the Start method has been called.");
            }
            _startTime = Time.time;
        }
    }
}
