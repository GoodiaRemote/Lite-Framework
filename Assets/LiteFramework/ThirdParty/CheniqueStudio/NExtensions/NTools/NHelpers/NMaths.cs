﻿using UnityEngine;

namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    public static class NMaths
    {
	    /// <summary>
	    ///     Internal method used to compute the spring velocity
	    /// </summary>
	    /// <param name="currentValue"></param>
	    /// <param name="targetValue"></param>
	    /// <param name="velocity"></param>
	    /// <param name="damping"></param>
	    /// <param name="frequency"></param>
	    /// <param name="speed"></param>
	    /// <param name="deltaTime"></param>
	    /// <returns></returns>
	    private static float SpringVelocity(float currentValue, float targetValue, float velocity, float damping,
            float frequency, float speed, float deltaTime)
        {
            frequency = frequency * 2f * Mathf.PI;
            return velocity + deltaTime * frequency * frequency * (targetValue - currentValue) +
                   -2.0f * deltaTime * frequency * damping * velocity;
        }

	    /// <summary>
	    ///     Springs a float towards a target value
	    /// </summary>
	    /// <param name="currentValue">the current value to spring, passed as a ref</param>
	    /// <param name="targetValue">the target value we're aiming for</param>
	    /// <param name="velocity">a velocity value, passed as ref, used to compute the current speed of the springed value</param>
	    /// <param name="damping">the damping, between 0.01f and 1f, the higher the daming, the less springy it'll be</param>
	    /// <param name="frequency">the frequency, in Hz, so the amount of periods the spring should go over in 1 second</param>
	    /// <param name="speed">the speed (between 0 and 1) at which the spring should operate</param>
	    /// <param name="deltaTime">the delta time (usually Time.deltaTime or Time.unscaledDeltaTime)</param>
	    public static void Spring(ref float currentValue, float targetValue, ref float velocity, float damping,
            float frequency, float speed, float deltaTime)
        {
            var initialVelocity = velocity;
            velocity = SpringVelocity(currentValue, targetValue, velocity, damping, frequency, speed, deltaTime);
            velocity = Lerp(initialVelocity, velocity, speed, Time.deltaTime);
            currentValue += deltaTime * velocity;
        }

	    /// <summary>
	    ///     Springs a Vector2 towards a target value
	    /// </summary>
	    /// <param name="currentValue">the current value to spring, passed as a ref</param>
	    /// <param name="targetValue">the target value we're aiming for</param>
	    /// <param name="velocity">a velocity value, passed as ref, used to compute the current speed of the springed value</param>
	    /// <param name="damping">the damping, between 0.01f and 1f, the higher the daming, the less springy it'll be</param>
	    /// <param name="frequency">the frequency, in Hz, so the amount of periods the spring should go over in 1 second</param>
	    /// <param name="speed">the speed (between 0 and 1) at which the spring should operate</param>
	    /// <param name="deltaTime">the delta time (usually Time.deltaTime or Time.unscaledDeltaTime)</param>
	    public static void Spring(ref Vector2 currentValue, Vector2 targetValue, ref Vector2 velocity, float damping,
            float frequency, float speed, float deltaTime)
        {
            var initialVelocity = velocity;
            velocity.x = SpringVelocity(currentValue.x, targetValue.x, velocity.x, damping, frequency, speed,
                deltaTime);
            velocity.y = SpringVelocity(currentValue.y, targetValue.y, velocity.y, damping, frequency, speed,
                deltaTime);
            velocity.x = Lerp(initialVelocity.x, velocity.x, speed, Time.deltaTime);
            velocity.y = Lerp(initialVelocity.y, velocity.y, speed, Time.deltaTime);
            currentValue += deltaTime * velocity;
        }

	    /// <summary>
	    ///     Springs a Vector3 towards a target value
	    /// </summary>
	    /// <param name="currentValue">the current value to spring, passed as a ref</param>
	    /// <param name="targetValue">the target value we're aiming for</param>
	    /// <param name="velocity">a velocity value, passed as ref, used to compute the current speed of the springed value</param>
	    /// <param name="damping">the damping, between 0.01f and 1f, the higher the daming, the less springy it'll be</param>
	    /// <param name="frequency">the frequency, in Hz, so the amount of periods the spring should go over in 1 second</param>
	    /// <param name="speed">the speed (between 0 and 1) at which the spring should operate</param>
	    /// <param name="deltaTime">the delta time (usually Time.deltaTime or Time.unscaledDeltaTime)</param>
	    public static void Spring(ref Vector3 currentValue, Vector3 targetValue, ref Vector3 velocity, float damping,
            float frequency, float speed, float deltaTime)
        {
            var initialVelocity = velocity;
            velocity.x = SpringVelocity(currentValue.x, targetValue.x, velocity.x, damping, frequency, speed,
                deltaTime);
            velocity.y = SpringVelocity(currentValue.y, targetValue.y, velocity.y, damping, frequency, speed,
                deltaTime);
            velocity.z = SpringVelocity(currentValue.z, targetValue.z, velocity.z, damping, frequency, speed,
                deltaTime);
            velocity.x = Lerp(initialVelocity.x, velocity.x, speed, Time.deltaTime);
            velocity.y = Lerp(initialVelocity.y, velocity.y, speed, Time.deltaTime);
            velocity.z = Lerp(initialVelocity.z, velocity.z, speed, Time.deltaTime);
            currentValue += deltaTime * velocity;
        }

	    /// <summary>
	    ///     Springs a Vector4 towards a target value
	    /// </summary>
	    /// <param name="currentValue">the current value to spring, passed as a ref</param>
	    /// <param name="targetValue">the target value we're aiming for</param>
	    /// <param name="velocity">a velocity value, passed as ref, used to compute the current speed of the springed value</param>
	    /// <param name="damping">the damping, between 0.01f and 1f, the higher the daming, the less springy it'll be</param>
	    /// <param name="frequency">the frequency, in Hz, so the amount of periods the spring should go over in 1 second</param>
	    /// <param name="speed">the speed (between 0 and 1) at which the spring should operate</param>
	    /// <param name="deltaTime">the delta time (usually Time.deltaTime or Time.unscaledDeltaTime)</param>
	    public static void Spring(ref Vector4 currentValue, Vector4 targetValue, ref Vector4 velocity, float damping,
            float frequency, float speed, float deltaTime)
        {
            var initialVelocity = velocity;
            velocity.x = SpringVelocity(currentValue.x, targetValue.x, velocity.x, damping, frequency, speed,
                deltaTime);
            velocity.y = SpringVelocity(currentValue.y, targetValue.y, velocity.y, damping, frequency, speed,
                deltaTime);
            velocity.z = SpringVelocity(currentValue.z, targetValue.z, velocity.z, damping, frequency, speed,
                deltaTime);
            velocity.w = SpringVelocity(currentValue.w, targetValue.w, velocity.w, damping, frequency, speed,
                deltaTime);
            velocity.x = Lerp(initialVelocity.x, velocity.x, speed, Time.deltaTime);
            velocity.y = Lerp(initialVelocity.y, velocity.y, speed, Time.deltaTime);
            velocity.z = Lerp(initialVelocity.z, velocity.z, speed, Time.deltaTime);
            velocity.w = Lerp(initialVelocity.w, velocity.w, speed, Time.deltaTime);
            currentValue += deltaTime * velocity;
        }

	    /// <summary>
	    ///     internal method used to determine the lerp rate
	    /// </summary>
	    /// <param name="rate"></param>
	    /// <returns></returns>
	    private static float LerpRate(float rate, float deltaTime)
        {
            rate = Mathf.Clamp01(rate);
            var invRate = -Mathf.Log(1.0f - rate, 2.0f) * 60f;
            return Mathf.Pow(2.0f, -invRate * deltaTime);
        }

	    /// <summary>
	    ///     Lerps a float towards a target at the specified rate
	    /// </summary>
	    /// <param name="value"></param>
	    /// <param name="target"></param>
	    /// <param name="rate"></param>
	    /// <returns></returns>
	    public static float Lerp(float value, float target, float rate, float deltaTime)
        {
            if (deltaTime == 0f) return value;
            return Mathf.Lerp(target, value, LerpRate(rate, deltaTime));
        }

	    /// <summary>
	    ///     Lerps a Vector2 towards a target at the specified rate
	    /// </summary>
	    /// <param name="value"></param>
	    /// <param name="target"></param>
	    /// <param name="rate"></param>
	    /// <returns></returns>
	    public static Vector2 Lerp(Vector2 value, Vector2 target, float rate, float deltaTime)
        {
            if (deltaTime == 0f) return value;
            return Vector2.Lerp(target, value, LerpRate(rate, deltaTime));
        }

	    /// <summary>
	    ///     Lerps a Vector3 towards a target at the specified rate
	    /// </summary>
	    /// <param name="value"></param>
	    /// <param name="target"></param>
	    /// <param name="rate"></param>
	    /// <returns></returns>
	    public static Vector3 Lerp(Vector3 value, Vector3 target, float rate, float deltaTime)
        {
            if (deltaTime == 0f) return value;
            return Vector3.Lerp(target, value, LerpRate(rate, deltaTime));
        }

	    /// <summary>
	    ///     Lerps a Vector4 towards a target at the specified rate
	    /// </summary>
	    /// <param name="value"></param>
	    /// <param name="target"></param>
	    /// <param name="rate"></param>
	    /// <returns></returns>
	    public static Vector4 Lerp(Vector4 value, Vector4 target, float rate, float deltaTime)
        {
            if (deltaTime == 0f) return value;
            return Vector4.Lerp(target, value, LerpRate(rate, deltaTime));
        }

	    /// <summary>
	    ///     Lerps a Quaternion towards a target at the specified rate
	    /// </summary>
	    /// <param name="value"></param>
	    /// <param name="target"></param>
	    /// <param name="rate"></param>
	    /// <returns></returns>
	    public static Quaternion Lerp(Quaternion value, Quaternion target, float rate, float deltaTime)
        {
            if (deltaTime == 0f) return value;
            return Quaternion.Lerp(target, value, LerpRate(rate, deltaTime));
        }

	    /// <summary>
	    ///     Lerps a Color towards a target at the specified rate
	    /// </summary>
	    /// <param name="value"></param>
	    /// <param name="target"></param>
	    /// <param name="rate"></param>
	    /// <returns></returns>
	    public static Color Lerp(Color value, Color target, float rate, float deltaTime)
        {
            if (deltaTime == 0f) return value;
            return Color.Lerp(target, value, LerpRate(rate, deltaTime));
        }

	    /// <summary>
	    ///     Lerps a Color32 towards a target at the specified rate
	    /// </summary>
	    /// <param name="value"></param>
	    /// <param name="target"></param>
	    /// <param name="rate"></param>
	    /// <returns></returns>
	    public static Color32 Lerp(Color32 value, Color32 target, float rate, float deltaTime)
        {
            if (deltaTime == 0f) return value;
            return Color32.Lerp(target, value, LerpRate(rate, deltaTime));
        }

	    /// <summary>
	    ///     Clamps a float between min and max, both bounds being optional and driven by clampMin and clampMax respectively
	    /// </summary>
	    /// <param name="value"></param>
	    /// <param name="min"></param>
	    /// <param name="max"></param>
	    /// <param name="clampMin"></param>
	    /// <param name="clampMax"></param>
	    /// <returns></returns>
	    public static float Clamp(float value, float min, float max, bool clampMin, bool clampMax)
        {
            var returnValue = value;
            if (clampMin && returnValue < min) returnValue = min;
            if (clampMax && returnValue > max) returnValue = max;
            return returnValue;
        }

	    /// <summary>
	    ///     Rounds a float to the nearest half value : 1, 1.5, 2, 2.5 etc
	    /// </summary>
	    /// <param name="a"></param>
	    /// <returns></returns>
	    public static float RoundToNearestHalf(float a)
        {
            return a = a - a % 0.5f;
        }

	    /// <summary>
	    /// </summary>
	    /// <param name="direction"></param>
	    /// <returns></returns>
	    public static Quaternion LookAt2D(Vector2 direction)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }

	    /// <summary>
	    ///     Takes a Vector3 and turns it into a Vector2
	    /// </summary>
	    /// <returns>The vector2.</returns>
	    /// <param name="target">The Vector3 to turn into a Vector2.</param>
	    public static Vector2 Vector3ToVector2(Vector3 target)
        {
            return new Vector2(target.x, target.y);
        }

	    /// <summary>
	    ///     Takes a Vector2 and turns it into a Vector3 with a null z value
	    /// </summary>
	    /// <returns>The vector3.</returns>
	    /// <param name="target">The Vector2 to turn into a Vector3.</param>
	    public static Vector3 Vector2ToVector3(Vector2 target)
        {
            return new Vector3(target.x, target.y, 0);
        }

	    /// <summary>
	    ///     Takes a Vector2 and turns it into a Vector3 with the specified z value
	    /// </summary>
	    /// <returns>The vector3.</returns>
	    /// <param name="target">The Vector2 to turn into a Vector3.</param>
	    /// <param name="newZValue">New Z value.</param>
	    public static Vector3 Vector2ToVector3(Vector2 target, float newZValue)
        {
            return new Vector3(target.x, target.y, newZValue);
        }

	    /// <summary>
	    ///     Rounds all components of a Vector3.
	    /// </summary>
	    /// <returns>The vector3.</returns>
	    /// <param name="vector">Vector.</param>
	    public static Vector3 RoundVector3(Vector3 vector)
        {
            return new Vector3(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));
        }

	    /// <summary>
	    ///     Returns a random Vector2 from 2 defined Vector2.
	    /// </summary>
	    /// <returns>The random Vector2.</returns>
	    /// <param name="min">Minimum.</param>
	    /// <param name="max">Maximum.</param>
	    public static Vector2 RandomVector2(Vector2 minimum, Vector2 maximum)
        {
            return new Vector2(Random.Range(minimum.x, maximum.x),
                Random.Range(minimum.y, maximum.y));
        }

	    /// <summary>
	    ///     Returns a random Vector3 from 2 defined Vector3.
	    /// </summary>
	    /// <returns>The random Vector3.</returns>
	    /// <param name="min">Minimum.</param>
	    /// <param name="max">Maximum.</param>
	    public static Vector3 RandomVector3(Vector3 minimum, Vector3 maximum)
        {
            return new Vector3(Random.Range(minimum.x, maximum.x),
                Random.Range(minimum.y, maximum.y),
                Random.Range(minimum.z, maximum.z));
        }

	    /// <summary>
	    ///     Returns a random point on the circle of the specified radius
	    /// </summary>
	    /// <param name="circleRadius"></param>
	    /// <returns></returns>
	    public static Vector2 RandomPointOnCircle(float circleRadius)
        {
            return Random.insideUnitCircle.normalized * circleRadius;
        }

	    /// <summary>
	    ///     Returns a random point on the sphere of the specified radius
	    /// </summary>
	    /// <param name="sphereRadius"></param>
	    /// <returns></returns>
	    public static Vector3 RandomPointOnSphere(float sphereRadius)
        {
            return Random.onUnitSphere * sphereRadius;
        }

	    /// <summary>
	    ///     Rotates a point around the given pivot.
	    /// </summary>
	    /// <returns>The new point position.</returns>
	    /// <param name="point">The point to rotate.</param>
	    /// <param name="pivot">The pivot's position.</param>
	    /// <param name="angle">The angle we want to rotate our point.</param>
	    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, float angle)
        {
            angle = angle * (Mathf.PI / 180f);
            var rotatedX = Mathf.Cos(angle) * (point.x - pivot.x) - Mathf.Sin(angle) * (point.y - pivot.y) + pivot.x;
            var rotatedY = Mathf.Sin(angle) * (point.x - pivot.x) + Mathf.Cos(angle) * (point.y - pivot.y) + pivot.y;
            return new Vector3(rotatedX, rotatedY, 0);
        }

	    /// <summary>
	    ///     Rotates a point around the given pivot.
	    /// </summary>
	    /// <returns>The new point position.</returns>
	    /// <param name="point">The point to rotate.</param>
	    /// <param name="pivot">The pivot's position.</param>
	    /// <param name="angles">The angle as a Vector3.</param>
	    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angle)
        {
            // we get point direction from the point to the pivot
            var direction = point - pivot;
            // we rotate the direction
            direction = Quaternion.Euler(angle) * direction;
            // we determine the rotated point's position
            point = direction + pivot;
            return point;
        }

	    /// <summary>
	    ///     Rotates a point around the given pivot.
	    /// </summary>
	    /// <returns>The new point position.</returns>
	    /// <param name="point">The point to rotate.</param>
	    /// <param name="pivot">The pivot's position.</param>
	    /// <param name="angles">The angle as a Vector3.</param>
	    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion quaternion)
        {
            // we get point direction from the point to the pivot
            var direction = point - pivot;
            // we rotate the direction
            direction = quaternion * direction;
            // we determine the rotated point's position
            point = direction + pivot;
            return point;
        }

	    /// <summary>
	    ///     Rotates a vector2 by the angle (in degrees) specified and returns it
	    /// </summary>
	    /// <returns>The rotated Vector2.</returns>
	    /// <param name="vector">The vector to rotate.</param>
	    /// <param name="angle">Degrees.</param>
	    public static Vector2 RotateVector2(Vector2 vector, float angle)
        {
            if (angle == 0) return vector;
            var sinus = Mathf.Sin(angle * Mathf.Deg2Rad);
            var cosinus = Mathf.Cos(angle * Mathf.Deg2Rad);

            var oldX = vector.x;
            var oldY = vector.y;
            vector.x = cosinus * oldX - sinus * oldY;
            vector.y = sinus * oldX + cosinus * oldY;
            return vector;
        }

	    /// <summary>
	    ///     Computes and returns the angle between two vectors, on a 360° scale
	    /// </summary>
	    /// <returns>The <see cref="System.Single" />.</returns>
	    /// <param name="vectorA">Vector a.</param>
	    /// <param name="vectorB">Vector b.</param>
	    public static float AngleBetween(Vector2 vectorA, Vector2 vectorB)
        {
            var angle = Vector2.Angle(vectorA, vectorB);
            var cross = Vector3.Cross(vectorA, vectorB);

            if (cross.z > 0) angle = 360 - angle;

            return angle;
        }

	    /// <summary>
	    ///     Computes and returns the direction between two vector3, used to check if a vector is pointing left or right of
	    ///     another one
	    /// </summary>
	    /// <returns>The <see cref="System.Single" />.</returns>
	    /// <param name="vectorA">Vector a.</param>
	    /// <param name="vectorB">Vector b.</param>
	    public static float AngleDirection(Vector3 vectorA, Vector3 vectorB, Vector3 up)
        {
            var cross = Vector3.Cross(vectorA, vectorB);
            var direction = Vector3.Dot(cross, up);

            return direction;
        }

	    /// <summary>
	    ///     Returns the distance between a point and a line.
	    /// </summary>
	    /// <returns>The between point and line.</returns>
	    /// <param name="point">Point.</param>
	    /// <param name="lineStart">Line start.</param>
	    /// <param name="lineEnd">Line end.</param>
	    public static float DistanceBetweenPointAndLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
        {
            return Vector3.Magnitude(ProjectPointOnLine(point, lineStart, lineEnd) - point);
        }

	    /// <summary>
	    ///     Projects a point on a line (perpendicularly) and returns the projected point.
	    /// </summary>
	    /// <returns>The point on line.</returns>
	    /// <param name="point">Point.</param>
	    /// <param name="lineStart">Line start.</param>
	    /// <param name="lineEnd">Line end.</param>
	    public static Vector3 ProjectPointOnLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
        {
            var rhs = point - lineStart;
            var vector2 = lineEnd - lineStart;
            var magnitude = vector2.magnitude;
            var lhs = vector2;
            if (magnitude > 1E-06f) lhs = lhs / magnitude;
            var num2 = Mathf.Clamp(Vector3.Dot(lhs, rhs), 0f, magnitude);
            return lineStart + lhs * num2;
        }

	    /// <summary>
	    ///     Returns the sum of all the int passed in parameters
	    /// </summary>
	    /// <param name="thingsToAdd">Things to add.</param>
	    public static int Sum(params int[] thingsToAdd)
        {
            var result = 0;
            for (var i = 0; i < thingsToAdd.Length; i++) result += thingsToAdd[i];
            return result;
        }

	    /// <summary>
	    ///     Returns the result of rolling a dice of the specified number of sides
	    /// </summary>
	    /// <returns>The result of the dice roll.</returns>
	    /// <param name="numberOfSides">Number of sides of the dice.</param>
	    public static int RollADice(int numberOfSides)
        {
            return Random.Range(1, numberOfSides + 1);
        }

	    /// <summary>
	    ///     Returns a random success based on X% of chance.
	    ///     Example : I have 20% of chance to do X, Chance(20) > true, yay!
	    /// </summary>
	    /// <param name="percent">Percent of chance.</param>
	    public static bool Chance(int percent)
        {
            return Random.Range(0, 100) <= percent;
        }

	    /// <summary>
	    ///     Moves from "from" to "to" by the specified amount and returns the corresponding value
	    /// </summary>
	    /// <param name="from">From.</param>
	    /// <param name="to">To.</param>
	    /// <param name="amount">Amount.</param>
	    public static float Approach(float from, float to, float amount)
        {
            if (from < to)
            {
                from += amount;
                if (from > to) return to;
            }
            else
            {
                from -= amount;
                if (from < to) return to;
            }

            return from;
        }


	    /// <summary>
	    ///     Remaps a value x in interval [A,B], to the proportional value in interval [C,D]
	    /// </summary>
	    /// <param name="x">The value to remap.</param>
	    /// <param name="A">the minimum bound of interval [A,B] that contains the x value</param>
	    /// <param name="B">the maximum bound of interval [A,B] that contains the x value</param>
	    /// <param name="C">the minimum bound of target interval [C,D]</param>
	    /// <param name="D">the maximum bound of target interval [C,D]</param>
	    public static float Remap(float x, float A, float B, float C, float D)
        {
            var remappedValue = C + (x - A) / (B - A) * (D - C);
            return remappedValue;
        }

	    /// <summary>
	    ///     Clamps the angle in parameters between a minimum and maximum angle (all angles expressed in degrees)
	    /// </summary>
	    /// <param name="angle"></param>
	    /// <param name="minimumAngle"></param>
	    /// <param name="maximumAngle"></param>
	    /// <returns></returns>
	    public static float ClampAngle(float angle, float minimumAngle, float maximumAngle)
        {
            if (angle < -360) angle += 360;
            if (angle > 360) angle -= 360;
            return Mathf.Clamp(angle, minimumAngle, maximumAngle);
        }

        public static float RoundToDecimal(float value, int numberOfDecimals)
        {
            if (numberOfDecimals <= 0)
                return Mathf.Round(value);
            return Mathf.Round(value * 10f * numberOfDecimals) / (10f * numberOfDecimals);
        }

        /// <summary>
        ///     Rounds the value passed in parameters to the closest value in the parameter array
        /// </summary>
        /// <param name="value"></param>
        /// <param name="possibleValues"></param>
        /// <returns></returns>
        public static float RoundToClosest(float value, float[] possibleValues, bool pickSmallestDistance = false)
        {
            if (possibleValues.Length == 0) return 0f;

            var closestValue = possibleValues[0];

            foreach (var possibleValue in possibleValues)
            {
                var closestDistance = Mathf.Abs(closestValue - value);
                var possibleDistance = Mathf.Abs(possibleValue - value);

                if (closestDistance > possibleDistance)
                    closestValue = possibleValue;
                else if (closestDistance == possibleDistance)
                    if ((pickSmallestDistance && closestValue > possibleValue) ||
                        (!pickSmallestDistance && closestValue < possibleValue))
                        closestValue = value < 0 ? closestValue : possibleValue;
            }

            return closestValue;
        }

        /// <summary>
        ///     Returns a vector3 based on the angle in parameters
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector3 DirectionFromAngle(float angle, float additionalAngle)
        {
            angle += additionalAngle;

            var direction = Vector3.zero;
            direction.x = Mathf.Sin(angle * Mathf.Deg2Rad);
            direction.y = 0f;
            direction.z = Mathf.Cos(angle * Mathf.Deg2Rad);
            return direction;
        }

        /// <summary>
        ///     Returns a vector3 based on the angle in parameters
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector3 DirectionFromAngle2D(float angle, float additionalAngle)
        {
            angle += additionalAngle;

            var direction = Vector3.zero;
            direction.x = Mathf.Cos(angle * Mathf.Deg2Rad);
            direction.y = Mathf.Sin(angle * Mathf.Deg2Rad);
            direction.z = 0f;
            return direction;
        }

        /// <summary>
        ///     Returns a int based on direction
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static int GetAngleFromVectorInt(Vector3 dir)
        {
            dir = dir.normalized;
            var n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;
            var angle = Mathf.RoundToInt(n);
            return angle;
        }

        /// <summary>
        ///     Returns a float based on direction
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static float GetAngleFromVectorFloat(Vector3 dir)
        {
            dir = dir.normalized;
            var n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;
            return n;
        }

        /// <summary>
        ///     Returns a position base on direction and distance
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="direction"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static Vector3 PointAlongDirection(Vector3 origin, Vector3 direction,
            float distance)
        {
            return origin + direction.normalized * distance;
        }

        /// <summary>
        ///     Returns a bool if object is on screen ( can see in camera )
        /// </summary>
        /// <param name="objectPos"></param>
        /// <returns></returns>
        public static bool IsOnScreen(Vector3 objectPos)
        {
            var viewportPos = CheniqueStudio.NExtensions.NTools.NHelpers.NHelpers.Camera.WorldToViewportPoint(objectPos);
            return viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1 &&
                   viewportPos.z > 0;
        }

        /// <summary>
        ///     Return middle number from two number
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static int GetMiddleNumberFromTwoNumber(int num1, int num2)
        {
            return Mathf.FloorToInt((num1 + num2) / 2f);
        }

        /// <summary>
        ///     Returns squared of number
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double Squared(this double a)
        {
            return a * a;
        }
        
        public static float Squared(this float a)
        {
	        return a * a;
        }
        
        public static int Squared(this int a)
        {
	        return a * a;
        }

        public static float Reverse(this float a)
        {
	        return a * -1f;
        }
        
        public static int Reverse(this int a)
        {
	        return a * -1;
        }

        public static double Reverse(this double a)
        {
	        return a * -1;
        }
        
        public static float GetRemainingMultiplierByPercent(float percent)
        {
	        return 1 - percent / 100f;
        }
        
        public static int GetIntValueByPercent(this int value, float percent)
        {
            return (int)(value * percent / 100f);
        }

        public static float GetFloatValueByPercent(this int value, float percent)
        {
            return value * percent / 100f;
        }

        public static float GetFloatValueByPercent(this float value, float percent)
        {
            return value * percent / 100f;
        }
    }
}