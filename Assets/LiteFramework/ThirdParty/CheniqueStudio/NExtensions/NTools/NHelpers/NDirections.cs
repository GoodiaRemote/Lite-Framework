using System.Collections.Generic;
using UnityEngine;

namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    public static class NDirections
    {
        public static readonly List<Vector2> EightDirections = new()
        {
            new Vector2(0, 1).normalized,
            new Vector2(1, 1).normalized,
            new Vector2(1, 0).normalized,
            new Vector2(1, -1).normalized,
            new Vector2(0, -1).normalized,
            new Vector2(-1, -1).normalized,
            new Vector2(-1, 0).normalized,
            new Vector2(-1, 1).normalized
        };

        public static readonly List<Vector2> FourDirections = new()
        {
            new Vector2(0, 1).normalized,
            new Vector2(1, 0).normalized,
            new Vector2(0, -1).normalized,
            new Vector2(-1, 0).normalized
        };

        public static Vector2 GetRandomEightDirection()
        {
            return EightDirections.GetRandomElement();
        }

        public static Vector2 GetRandomFourDirection()
        {
            return FourDirections.GetRandomElement();
        }

        public static Vector3 GetRandomDirection()
        {
            var x = Random.Range(-1f, 1f);
            var y = Random.Range(-1f, 1f);
            return new Vector3(x, y, 0f);
        }
    }

    public enum FourDirection
    {
        NONE,
        UP,
        RIGHT,
        DOWN,
        LEFT
    }
}