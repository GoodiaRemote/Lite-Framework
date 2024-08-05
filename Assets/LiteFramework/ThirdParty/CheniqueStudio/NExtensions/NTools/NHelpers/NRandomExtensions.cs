using System;
using System.Collections.Generic;
using UnityEngine;
using UnityRandom = UnityEngine.Random;
using SystemRandom = System.Random;

namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    public static class NRandomExtensions
    {
        public static int RandomRangeAroundIntValue(this int baseValue, int minRange, int maxRange)
        {
            return UnityRandom.Range(baseValue - minRange, baseValue + maxRange);
        }

        public static float RandomRangeAroundFloatValue(this float baseValue, float minRange, float maxRange)
        {
            return UnityRandom.Range(baseValue - minRange, baseValue + maxRange);
        }

        public static int RandomRange(int min, int max)
        {
            return UnityRandom.Range(min, max);
        }
        
        public static int RandomExcept(int min, int max, int except)
        {
            if (UnityRandom.Range(0, 1 - float.Epsilon) > (except - min) / (float)(max - min))
                return UnityRandom.Range(min, except);

            return 1 + UnityRandom.Range(except, max);
        }

        public static T GetRandomElement<T>(this List<T> list)
        {
            var index = UnityRandom.Range(0, list.Count);
            return list[index];
        }

        public static TEnum GetRandomEnumValueExcluding<TEnum>(this TEnum excludedValue)
        {
            var enumValues = Enum.GetValues(typeof(TEnum));
            TEnum randomValue;
            var random = new SystemRandom();
            do
            {
                randomValue = (TEnum)enumValues.GetValue(random.Next(enumValues.Length));
            } while (randomValue.Equals(excludedValue));

            return randomValue;
        }

        public static T GetRandomEnum<T>()
        {
            var A = Enum.GetValues(typeof(T));
            var V = (T)A.GetValue(UnityRandom.Range(0, A.Length));
            return V;
        }

        private static Vector3 RandomPointOnCircleEdge(float radius)
        {
            var vector2 = UnityRandom.insideUnitCircle * radius;
            return new Vector3(vector2.x, vector2.y, 0);
        }
    }
}