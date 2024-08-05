using UnityEngine;

namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    public static class NLogger
    {
        public static string Color(this string myStr, Color color)
        {
            // return $"<color={color}>{myStr}</color>";
            return $"<color=#{NColors.ColorToHtml(color)}>{myStr}</color>";
        }

        private static void DoLog(System.Action<string, Object> LogFunction, string prefix, Object myObj, Color color,
            params object[] msg)
        {
#if UNITY_EDITOR
            var name = myObj ? myObj.name : "NullObject";
            LogFunction($"{prefix}[{name}]: {string.Join("; ", msg)}\n ".Color(color), myObj);
#endif
        }

        public static void Log(this Object myObj, params object[] msg)
        {
            DoLog(Debug.Log, "", myObj, NColors.White, msg);
        }

        public static void Log(this Object myObj, Color? color = null, params object[] msg)
        {
            color ??= UnityEngine.Color.white;
            DoLog(Debug.Log, "", myObj, color.Value, msg);
        }

        public static void LogDetail(this Object myObj, params object[] msg)
        {
            DoLog(Debug.Log, "➤", myObj, NColors.Cyan, msg);
        }

        public static void LogError(this Object myObj, params object[] msg)
        {
            DoLog(Debug.LogError, "<!>", myObj, NColors.Red, msg);
        }

        public static void LogWarning(this Object myObj, params object[] msg)
        {
            DoLog(Debug.LogWarning, "⚠️", myObj, NColors.Yellow, msg);
        }

        public static void LogSuccess(this Object myObj, params object[] msg)
        {
            DoLog(Debug.Log, "☻", myObj, NColors.Green, msg);
        }
    }
}