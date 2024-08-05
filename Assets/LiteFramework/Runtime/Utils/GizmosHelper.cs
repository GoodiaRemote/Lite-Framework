using UnityEngine;

namespace LiteFramework.Runtime.Utils
{
    public static class GizmosHelper
    {
        public static void DrawGizmoCircle(Vector2 center, float radius)
        {
            for (var i = 0; i < 32; i++)
            {
                var radians = i / 32f * Mathf.PI * 2;
                var nextRadian = (i + 1) / 32f * Mathf.PI * 2;
                Gizmos.DrawLine(
                    new Vector3(center.x + Mathf.Cos(radians) * radius, center.y + Mathf.Sin(radians) * radius, 0),
                    new Vector3(center.x + Mathf.Cos(nextRadian) * radius, center.y + Mathf.Sin(nextRadian) * radius, 0));
            }
        }
    }
}