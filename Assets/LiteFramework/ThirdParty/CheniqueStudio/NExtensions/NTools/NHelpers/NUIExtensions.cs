using UnityEngine;
using UnityEngine.UI;

namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    public static class NUIExtensions
    {
        public static void ChangeNormalColor(this Button button, Color color)
        {
            var colors = button.colors;
            colors.normalColor = color;
            button.colors = colors;
        }
    }
}