using System;

namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    public static class NStringExtensions
    {
        public static string GetFormattedTime(this float secondsRemaining, string format = "{0:00}:{1:00}")
        {
            var timeSpan = TimeSpan.FromSeconds(secondsRemaining);
            return string.Format(format, timeSpan.Minutes, timeSpan.Seconds);
        }
    }
}