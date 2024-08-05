using UnityEngine;

namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    public static class NProbabilityChecker
    {
        public static bool IsEventHappening(float probability)
        {
            var randomValue = Random.value;
            return randomValue < probability / 100f;
        }
        
        public static bool IsCritical(float probability, int damage, out int criticalDamage)
        {
            var randomValue = Random.value;
            var isCritical = randomValue < probability / 100f;
            if (isCritical)
            {
                criticalDamage = damage * 2;
            }
            else
            {
                criticalDamage = damage;
            }
            return isCritical;
        }
    }
}