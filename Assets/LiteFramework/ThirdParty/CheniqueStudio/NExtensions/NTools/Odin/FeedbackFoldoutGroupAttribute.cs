using System;
using Sirenix.OdinInspector;

namespace CheniqueStudio.NExtensions.NTools.Odin
{
    public class FeedbackGroupAttribute : PropertyGroupAttribute
    {
        public float R, G, B, A;

        public FeedbackGroupAttribute(string path, float order = 0.0f)
            : base(path, order)
        {
        }

        public FeedbackGroupAttribute(string path = "Feedbacks", float r = 0.098f, float g = 1f, float b = 0.643f,
            float a = 1f, float order = 10f)
            : base(path, order)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        protected override void CombineValuesWith(PropertyGroupAttribute other)
        {
            var otherAttr = (FeedbackGroupAttribute)other;

            R = Math.Max(otherAttr.R, R);
            G = Math.Max(otherAttr.G, G);
            B = Math.Max(otherAttr.B, B);
            A = Math.Max(otherAttr.A, A);
        }
    }
}