using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using UnityRandom = UnityEngine.Random;
using SystemRandom = System.Random;

namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    public static class NHelpers
    {
        private static Camera _camera;

        public static Camera Camera
        {
            get
            {
                if (_camera == null) _camera = Camera.main;
                return _camera;
            }
        }

        public static Vector3 GetVectorFromAngle(float angle)
        {
            // angle = 0 -> 360
            var angleRad = angle * (Mathf.PI / 180);
            return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }

        public static float GetAngleFromVectorFloat(Vector3 dir)
        {
            dir = dir.normalized;
            var n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;
            return n;
        }
        
        public static float GetAngleFromVectorFloat(Vector3 dir, float offset)
        {
            dir = dir.normalized;
            var n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            n += offset;
            if (n < 0) n += 360;
            return n;
        }

        public static int GetAngleFromVector(Vector3 dir)
        {
            dir = dir.normalized;
            var n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;
            var angle = Mathf.RoundToInt(n);
            return angle;
        }


        public static int RemapToInt(this float x, float a, float b, float c, float d)
        {
            return (int)(c + (x - a) / (b - a) * (d - c));
        }

        public static float RemapToFloat(this float x, float a, float b, float c, float d)
        {
            return c + (x - a) / (b - a) * (d - c);
        }

        public static Vector3 PointAlongDirection(Vector3 origin, Vector3 direction,
            float distance)
        {
            return origin + direction.normalized * distance;
        }

        public static bool IsOnScreen(Vector3 objectPos)
        {
            var viewportPos = Camera.WorldToViewportPoint(objectPos);
            return viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1 &&
                   viewportPos.z > 0;
        }
        
        /// <summary>
        ///     Destroy all child objects of this transform (Unintentionally evil sounding).
        ///     Use it like so:
        ///     <code>
        /// transform.DestroyChildren();
        /// </code>
        /// </summary>
        public static void DestroyChildren(this Transform t)
        {
            foreach (Transform child in t) Object.Destroy(child.gameObject);
        }

        /// <summary>
        ///     Creates and returns a clone of any given scriptable object.
        /// </summary>
        public static T Clone<T>(this T scriptableObject) where T : ScriptableObject
        {
            if (scriptableObject == null)
            {
                Debug.LogError($"ScriptableObject was null. Returning default {typeof(T)} object.");
                return (T)ScriptableObject.CreateInstance(typeof(T));
            }

            var instance = Object.Instantiate(scriptableObject);
            instance.name = scriptableObject.name; // remove (Clone) from name
            return instance;
        }

        public static Vector3 RandomPositionInSpriteRenderer(SpriteRenderer renderer)
        {
            var bounds = renderer.bounds;
            var x = UnityRandom.Range(bounds.min.x, bounds.max.x);
            var y = UnityRandom.Range(bounds.min.y, bounds.max.y);
            return new Vector3(x, y, 0);
        }

        public static int ToInt(this string num)
        {
            return Convert.ToInt32(num);
        }
        
        public static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur) return true;

                toCheck = toCheck.BaseType;
            }

            return false;
        }
        
        public static Vector3 CalculatorForceToPosition(Vector3 currentPos, Vector3 targetPos, float mass)
        {
            var displacement = targetPos - currentPos;

            var accelerationMagnitude = displacement.magnitude / 2;
            var acceleration = displacement.normalized * accelerationMagnitude;
            var forceMagnitude = mass * accelerationMagnitude;
            var force = forceMagnitude * acceleration;
            return force;
        }

        public static T ChangeAlpha<T>(this T g, float newAlpha)
            where T : Graphic
        {
            var color = g.color;
            color.a = newAlpha;
            g.color = color;
            return g;
        }

        
        public static int RoundToInt(this float value)
        {
            return Mathf.RoundToInt(value);
        }

        public static T RandomEnum<T>()
        {
            var values = Enum.GetValues(typeof(T));
            var random = new SystemRandom();
            return (T)values.GetValue(random.Next(values.Length));
        }

        public static Quaternion RandomRotationY(this Quaternion quaternion)
        {
            var randomYRotation = UnityRandom.Range(0f, 360f);

            return Quaternion.Euler(quaternion.x, randomYRotation, quaternion.z);
        }

        public static Quaternion RandomRotation(this Quaternion quaternion)
        {
            var randomZRotation = UnityRandom.Range(0f, 360f);

            return Quaternion.Euler(quaternion.x, quaternion.z, randomZRotation);
        }

        public static void RandomRotationZ(ref Transform transform)
        {
            var randomZRotation = UnityRandom.Range(0f, 360f);
            var currentRotation = transform.rotation;
            transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.z, randomZRotation);
        }

        public static Vector3 RandomPositionAroundObject(this Transform transform, float radius)
        {
            return (Vector2)transform.position + UnityRandom.insideUnitCircle.normalized * radius;
        }

        public static Vector3 RandomPositionAroundPosition(this Vector3 pos, float radius)
        {
            return (Vector2)pos + UnityRandom.insideUnitCircle.normalized * radius;
        }
        
        public static float adHeight()
        {
            var f = Screen.dpi / 160f;
            var dp = Screen.height / f;
            return dp > 720f ? 90f * f
                : dp > 400f ? 50f * f
                : 32f * f;
        }

        public static float DpToPixel(float dp)
        {
            return dp * (Screen.dpi / 160);
        }

        public static double CompareDateTimeByDay(DateTime previousTime, DateTime currentTime)
        {
            var previousData = previousTime.ChangeTime(12, 00, 00, 00);
            var currentDate = currentTime.ChangeTime(12, 00, 00, 00);
            var elapsedDays = (currentDate - previousData).TotalDays;

            Debug.Log("previous day " + previousData + "current Day " + currentDate + " total day " +
                      elapsedDays);
            return elapsedDays;
        }

        private static DateTime ChangeTime(this DateTime dateTime, int hours, int minutes, int seconds,
            int milliseconds)
        {
            return new DateTime(dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }

        public static bool HasComponent<T>(this GameObject flag) where T : Component
        {
            return flag.GetComponent<T>() != null;
        }

        public static void SetFieldByName(this object obj, string fieldName, object value)
        {
            var field = obj.GetType().GetField(fieldName);
            if (field != null)
                field.SetValue(obj, value);
            else
                // Handle field not found error
                throw new ArgumentException($"Unable to set field '{fieldName}'.");
        }

        public static void SetPropertyByName(object obj, string propertyName, object value)
        {
            var field = obj.GetType().GetProperty(propertyName);
            if (field != null)
                field.SetValue(obj, value);
            else
                // Handle field not found error
                throw new ArgumentException($"Unable to set property '{propertyName}'.");
        }

        public static string FormatNumber(long num)
        {
            return $"{num:N0}";
        }

        public static float SmoothInputVertical(float targetV, ref float slidingV)
        {
            var sensitivity = 20f;
            var deadZone = 0.001f;

            slidingV = Mathf.MoveTowards(slidingV,
                targetV, sensitivity * Time.deltaTime);

            return Mathf.Abs(slidingV) < deadZone ? 0f : slidingV;
        }

        public static float SmoothInputHorizontal(float targetH, ref float slidingH)
        {
            var sensitivity = 8f;
            var deadZone = 0.001f;

            slidingH = Mathf.MoveTowards(slidingH,
                targetH, sensitivity * Time.deltaTime);

            return Mathf.Abs(slidingH) < deadZone ? 0f : slidingH;
        }

        public static Vector2 SmoothInput(float targetH, float targetV, ref float slidingV, ref float slidingH)
        {
            var sensitivity = 3f;
            var deadZone = 0.001f;

            slidingH = Mathf.MoveTowards(slidingH,
                targetH, sensitivity * Time.deltaTime);

            slidingV = Mathf.MoveTowards(slidingV,
                targetV, sensitivity * Time.deltaTime);

            return new Vector2(
                Mathf.Abs(slidingH) < deadZone ? 0f : slidingH,
                Mathf.Abs(slidingV) < deadZone ? 0f : slidingV);
        }

        #region Compare double

        // number of digits to be compared    
        private const int N = 12;

        // n+1 because b/a tends to 1 with n leading digits
        private static double MyEpsilon { get; } = Math.Pow(10, -(N + 1));

        public static bool IsEqual(double a, double b)
        {
            // Avoiding division by zero
            if (Math.Abs(a) <= double.Epsilon || Math.Abs(b) <= double.Epsilon)
                return Math.Abs(a - b) <= double.Epsilon;

            // Comparison
            return Math.Abs(1.0 - a / b) <= MyEpsilon;
        }

        #endregion
    }
}