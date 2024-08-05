using System;
using Sirenix.OdinInspector;

namespace CheniqueStudio.NExtensions.NTools.Odin
{
    public class GUIGroupAttribute : PropertyGroupAttribute
    {
        public float R, G, B, A;

        public GUIGroupAttribute(string path, float other = -20f)
            : base(path, other)
        {
        }

        public GUIGroupAttribute(string path = "GUI", float r = 0.549f, float g = 0.835f, float b = 1f, float a = 1f,
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
            var otherAttr = (GUIGroupAttribute)other;

            R = Math.Max(otherAttr.R, R);
            G = Math.Max(otherAttr.G, G);
            B = Math.Max(otherAttr.B, B);
            A = Math.Max(otherAttr.A, A);
        }
    }
}