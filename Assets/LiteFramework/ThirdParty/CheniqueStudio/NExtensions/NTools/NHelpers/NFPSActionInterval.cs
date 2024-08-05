using UnityEngine;

namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    public class NFPSActionInterval
    {
        private readonly float _detectionIntervalPerSecond;
        private readonly float _frameInterval;

        private int _frameCount;

        public NFPSActionInterval(int detectionIntervalPerSecond)
        {
            _detectionIntervalPerSecond = detectionIntervalPerSecond;
            _frameInterval = Application.targetFrameRate / _detectionIntervalPerSecond;
        }

        public bool Update()
        {
            _frameCount++;

            if (_frameCount >= _frameInterval)
            {
                _frameCount = 0; // Reset frame count
                return true;
            }

            return false;
        }
    }
}