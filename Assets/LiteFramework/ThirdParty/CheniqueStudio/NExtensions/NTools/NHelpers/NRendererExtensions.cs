using UnityEngine;

namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    /// <summary>
    ///     Renderer extensions
    /// </summary>
    public static class RendererExtensions
    {
        /// <summary>
        ///     Returns true if a renderer is visible from a camera
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static bool NIsVisibleFrom(this Renderer renderer, Camera camera)
        {
            var frustumPlanes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds);
        }
    }
}