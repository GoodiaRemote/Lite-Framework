using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    public static class NTilemapExtension
    {
        public static List<Vector3> GetAllPositionTileInTilemap(this Tilemap tilemap)
        {
            var tilesPos = new List<Vector3>();
            var bounds = tilemap.cellBounds;
            for (var x = bounds.min.x; x < bounds.max.x; x++)
            for (var y = bounds.min.y; y < bounds.max.y; y++)
            {
                var position = new Vector3Int(x, y, 0);

                if (tilemap.HasTile(position)) tilesPos.Add(position);
            }

            return tilesPos;
        }

        public static Vector3 GetPointFarthest(Vector3 referencePos, IReadOnlyList<Vector3> points)
        {
            var farthestPoint = Vector3.zero;
            var maxDistance = Vector3.Distance(referencePos, points[0]);

            for (var i = 1; i < points.Count; i++)
            {
                var distance = Vector3.Distance(referencePos, points[i]);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    farthestPoint = points[i];
                }
            }

            return farthestPoint;
        }
    }
}