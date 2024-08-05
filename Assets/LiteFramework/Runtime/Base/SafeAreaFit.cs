using UnityEngine;

namespace LiteFramework.Runtime.Base
{
    public class SafeAreaFit : MonoBehaviour
    {
        private RectTransform _panel;
        [SerializeField] private bool _fitX = true;
        [SerializeField] private bool _fitY = true;

        private void Awake()
        {
            _panel = GetComponent<RectTransform>();
            ApplySafeArea();
        }

        private void ApplySafeArea()
        {
            var safeArea = Screen.safeArea;
            if (!_fitX)
            {
                safeArea.x = 0;
                safeArea.width = Screen.width;
            }

            if (!_fitY)
            {
                safeArea.y = 0;
                safeArea.height = Screen.height;
            }

            // Check for invalid screen startup state on some Samsung devices (see below)
            if (Screen.width > 0 && Screen.height > 0)
            {
                // Convert safe area rectangle from absolute pixels to normalised anchor coordinates
                var anchorMin = safeArea.position;
                var anchorMax = safeArea.position + safeArea.size;
                anchorMin.x /= Screen.width;
                anchorMin.y /= Screen.height;
                anchorMax.x /= Screen.width;
                anchorMax.y /= Screen.height;

                // Fix for some Samsung devices (e.g. Note 10+, A71, S20) where Refresh gets called twice and the first time returns NaN anchor coordinates
                if (anchorMin.x >= 0 && anchorMin.y >= 0 && anchorMax.x >= 0 && anchorMax.y >= 0)
                {
                    _panel.anchorMin = anchorMin;
                    _panel.anchorMax = anchorMax;
                }
            }
        }
    }
}