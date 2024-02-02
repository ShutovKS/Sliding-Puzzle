using System;

namespace Units.Time
{
    public class Stopwatch
    {
        public bool IsRunning { get; private set; }

        private DateTime _startTime;
        private TimeSpan _elapsed;

        public void Start()
        {
            if (IsRunning)
            {
                throw new InvalidOperationException("Stopwatch is already running.");
            }

            _startTime = DateTime.UtcNow;
            IsRunning = true;
        }

        public void Stop()
        {
            if (!IsRunning)
            {
                throw new InvalidOperationException("Stopwatch is not running.");
            }

            _elapsed += DateTime.UtcNow - _startTime;
            IsRunning = false;
        }

        public TimeSpan GetElapsedTime()
        {
            return _elapsed + (IsRunning ? DateTime.UtcNow - _startTime : TimeSpan.Zero);
        }

        public void Reset()
        {
            _elapsed = TimeSpan.Zero;
            IsRunning = false;
        }
    }
}