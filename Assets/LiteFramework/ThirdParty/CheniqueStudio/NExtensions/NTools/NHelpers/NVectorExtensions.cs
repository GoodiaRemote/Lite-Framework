using UnityEngine;

namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    public static class NVectorExtensions
    {
        public static Vector3 GetVector3ByPercent(this Vector3 origin, float percent)
        {
            return origin / 100f * percent;
        }
        
        public static float AngleDir(Vector2 a, Vector2 b)
        {
            return -a.x * b.y + a.y * b.x;
        }
    }
}