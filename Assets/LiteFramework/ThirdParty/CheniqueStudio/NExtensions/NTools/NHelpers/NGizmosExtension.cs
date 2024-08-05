using UnityEditor;
using UnityEngine;

namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    public static class NGizmosExtension
    {
#if UNITY_EDITOR
        private static void DrawString(string text, Vector3 worldPos, Color? colour = null)
        {
            Handles.BeginGUI();
            if (colour.HasValue) UnityEngine.GUI.color = colour.Value;
            var view = SceneView.currentDrawingSceneView;
            var screenPos = view.camera.WorldToScreenPoint(worldPos);
            var size = UnityEngine.GUI.skin.label.CalcSize(new GUIContent(text));
            UnityEngine.GUI.Label(
                new Rect(screenPos.x - size.x / 2, -screenPos.y + view.position.height + 4, size.x, size.y), text);
            Handles.EndGUI();
        }
#endif
    }
}