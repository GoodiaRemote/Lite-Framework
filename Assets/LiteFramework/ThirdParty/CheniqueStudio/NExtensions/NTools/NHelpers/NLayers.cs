using UnityEngine;

namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    public static class NLayers
    {
        public static bool LayerInLayerMask(int layer, LayerMask layerMask)
        {
            return ((1 << layer) & layerMask) != 0;
        }

        public static void SetLayerMask(this GameObject gameObject, LayerMask layerMask)
        {
            gameObject.layer = layerMask;
        }

        public static void SetLayerMaskBinaric(this GameObject gameObject, LayerMask layerMask)
        {
            gameObject.layer = 1 << layerMask;
        }
    }
}