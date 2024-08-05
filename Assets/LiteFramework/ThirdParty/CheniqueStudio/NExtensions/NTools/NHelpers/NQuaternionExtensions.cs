using UnityEngine;
using Random = UnityEngine.Random;

namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    public static class NQuaternionExtensions
    {
        public static Quaternion GetRotationToTarget(Vector3 targetPos, Vector3 ownerPos)
        {
            var targetDir = (targetPos - ownerPos).normalized;
            // Directions.GetRandomFourDirection();
            var angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
        public static Quaternion GetRotationByDirection(Vector3 direction)
        {
            // Directions.GetRandomFourDirection();
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }

        public static Quaternion RandomRotationZ()
        {
            return Quaternion.Euler(0f, 0f, Random.Range(0f, 360.0f));
        }
    }
}