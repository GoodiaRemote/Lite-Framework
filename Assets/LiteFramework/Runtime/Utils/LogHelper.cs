using UnityEngine;

namespace LiteFramework.Runtime.Utils
{
    public static class LogHelper
    {
        public static string ColorLog(this string text, Color color)
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>";
        }
    }
}