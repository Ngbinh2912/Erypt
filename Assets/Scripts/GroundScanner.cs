using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;


public class GroundScanner : MonoBehaviour
{
    public Tilemap groundTilemap;
    private List<Vector3> groundPositions = new List<Vector3>();

    public List<Vector3> GetGroundPositions()
    {
        groundPositions.Clear();

        BoundsInt bounds = groundTilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (groundTilemap.HasTile(pos))
                {
                    Vector3 worldPos = groundTilemap.CellToWorld(pos) + groundTilemap.tileAnchor;
                    groundPositions.Add(worldPos);
                }
            }
        }

        return groundPositions;
    }
}
