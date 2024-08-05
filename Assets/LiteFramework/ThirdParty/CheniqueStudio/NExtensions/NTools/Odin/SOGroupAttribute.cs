using System;
using Sirenix.OdinInspector;

namespace CheniqueStudio.NExtensions.NTools.Odin
{
    public class SOGroupAttribute : PropertyGroupAttribute
    {
        public float R, G, B, A;

        public SOGroupAttribute(string path, float other = -10f)
            : base(path, other)
        {
        }

        public SOGroupAttribute(string path = "SO", float r = 1f, float g = 0.161f, float b = 0.176f, float a = 1f,
            float other = -10f)
            : base(path, other)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        protected override void CombineValuesWith(PropertyGroupAttribute other)
        {
            var otherAttr = (SOGroupAttribute)other;

            R = Math.Max(otherAttr.R, R);
            G = Math.Max(otherAttr.G, G);
            B = Math.Max(otherAttr.B, B);
            A = Math.Max(otherAttr.A, A);
        }
    }
}