using System;

namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    public static class NTimeExtension
    {
        public static int MinutesToSeconds(this int minutes)
        {
            return (int) TimeSpan.FromMinutes(minutes).TotalSeconds;
        }
    }
    
    public class TimeCountdown
    {
        private readonly float countdownTime; // The total countdown time in seconds
        private float elapsedTime; // The elapsed time since the countdown started
        public float remainingTime;
        public TimeCountdown(float countdownTime)
        {
            this.countdownTime = countdownTime;
            elapsedTime = 0f;
        }

        public bool Update(float deltaTime)
        {
            elapsedTime += deltaTime;
            remainingTime = countdownTime - elapsedTime;
            if (remainingTime <= 0)
            {
                elapsedTime = countdownTime;
                return true;
            }

            return false;
        }

        public string GetFormattedTime(string format = "{0:00}:{1:00}")
        {
            var timeSpan = TimeSpan.FromSeconds(remainingTime);
            return string.Format(format, timeSpan.Minutes, timeSpan.Seconds);
        }
    }
    public class TimeCountup
    {
        private readonly float countUpTime;
        private float elapsedTime;
        
        public TimeCountup(float countUpTime)
        {
            this.countUpTime = countUpTime;
            elapsedTime = 0f;
        }

        public bool Update(float deltaTime)
        {
            elapsedTime += deltaTime;
            if (elapsedTime >= countUpTime)
            {
                elapsedTime = countUpTime;
                return true;
            }
            return false;
        }

        public string GetFormattedTime(string format = "{0:00}:{1:00}")
        {
            var timeSpan = TimeSpan.FromSeconds(elapsedTime);
            return string.Format(format, timeSpan.Minutes, timeSpan.Seconds);
        }
    }
}