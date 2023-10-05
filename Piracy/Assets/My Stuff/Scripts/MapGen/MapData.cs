using UnityEngine;

public class MapData
{
    public Color[] ColorMap { get; }
    public float[] HeightMap { get; }
    public int[] SpawnPoints { get; }

    public bool ContainsMeshData;

    public MeshData MeshData { get; } = null;

    public MapData(Color[] colorMap, float[] heightMap, int[] spawnPoints)
    {
        ColorMap = colorMap;
        HeightMap = heightMap;
        SpawnPoints = spawnPoints;

        ContainsMeshData = false;
    }

    public MapData(Color[] colorMap, float[] heightMap, int[] spawnPoints, MeshData meshData)
    {
        ColorMap = colorMap;
        HeightMap = heightMap;
        SpawnPoints = spawnPoints;

        ContainsMeshData = true;
    }
}