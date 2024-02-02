using System;
using UnityEngine;

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
                Stop();
            }

            _startTime = DateTime.UtcNow;
            IsRunning = true;
        }

        public void Stop()
        {
            if (!IsRunning)
            {
                return;
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