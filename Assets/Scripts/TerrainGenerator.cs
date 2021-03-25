using UnityEngine;
using UnityEngine.U2D;

/// <summary> Info used by TerrainGenerator</summary>
public class TerrainEdgeInfo
{
    public TerrainEdgeInfo(SpriteShapeController terrain, float maxShapeHeight, float edgeWidth, float distanceBeetwenShapes)
    {
        Terrain = terrain;
        MaxShapeHeight = maxShapeHeight;
        DistanceBeetwenShapes = distanceBeetwenShapes;
        EdgeWidth = edgeWidth;
    }

    public SpriteShapeController Terrain;
    public float MaxShapeHeight;
    public float EdgeWidth;
    public float DistanceBeetwenShapes;
};

public static class TerrainGenerator
{

    /// <summary> Extend SpriteShape and add shapes to it
    /// <para>Return edge corner position</para> </summary>
    public static Vector3 GenerateTerrainEdge(TerrainEdgeInfo edgeInfo)
    {
        if (edgeInfo.Terrain == null)
        {
            Debug.LogError("Got null terrain");
            return Vector3.zero;
        }

        Vector3 startPos = edgeInfo.Terrain.spline.GetPosition(edgeInfo.Terrain.spline.GetPointCount() - 2);

        MakeTerrainLonger(edgeInfo);

        GenerateShapes(edgeInfo, startPos);

        return edgeInfo.Terrain.spline.GetPosition(edgeInfo.Terrain.spline.GetPointCount() - 3);
    }

    private static void MakeTerrainLonger(TerrainEdgeInfo edgeInfo)
    {
        int cornerIndex = edgeInfo.Terrain.spline.GetPointCount() - 2;
        Vector3 topBorderPos = edgeInfo.Terrain.spline.GetPosition(cornerIndex);

        topBorderPos.x += edgeInfo.EdgeWidth;

        edgeInfo.Terrain.spline.SetPosition(cornerIndex, topBorderPos);

        Vector3 bottomBorderPos = edgeInfo.Terrain.spline.GetPosition(cornerIndex + 1);
        bottomBorderPos.x += edgeInfo.EdgeWidth;

        edgeInfo.Terrain.spline.SetPosition(cornerIndex + 1, bottomBorderPos);
    }

    private static void GenerateShapes(TerrainEdgeInfo edgeInfo, Vector3 startPos)
    {
        int currentIndex = edgeInfo.Terrain.spline.GetPointCount() - 2;

        float currentXPos = startPos.x;
        float lastYPos = startPos.y;

        while (currentXPos < startPos.x + edgeInfo.EdgeWidth)
        {
            currentXPos += edgeInfo.DistanceBeetwenShapes;

            if (currentXPos > startPos.x + edgeInfo.EdgeWidth)
                currentXPos = startPos.x + edgeInfo.EdgeWidth;

            Vector3 newPoint = new Vector3(currentXPos, startPos.y + Random.Range(edgeInfo.MaxShapeHeight * .1f, edgeInfo.MaxShapeHeight));

            edgeInfo.Terrain.spline.InsertPointAt(currentIndex, newPoint);

            currentIndex++;
        }
    }
}
