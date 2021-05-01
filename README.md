# Game

>A game with mechanics and gameplay from the old button game Gravity Defied made on the Unity 3D engine

![](https://vgtimes.ru/uploads/posts/2020-07/thumbs/1594285914_d6c1a6becad08caf939b23ccf74c222f_fgraphic.jpg)

##  Level generation

For display shapes i used `Sprite shape asset (Official unity package)`


### Terrain Generationg Info
To make generation be easily to modifiable here been used TerrainGenerationEdgeInfo, that filling with all required info


```csharp
public TerrainEdgeInfo(SpriteShapeController terrain, float maxShapeHeight, float edgeWidth, float distanceBeetwenShapes)
{
    Terrain = terrain;
    MaxShapeHeight = maxShapeHeight;
    DistanceBeetwenShapes = distanceBeetwenShapes;
    EdgeWidth = edgeWidth;
}
```

### Terrain Generator

```csharp
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
```

```csharp
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
```

```csharp
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
```
