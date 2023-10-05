using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public MapData GenerateMap(int chunkWidth, Vector2 chunkPosition, float chunkResolution = 1)
    {
        Color[] colorMap = new Color[chunkWidth * chunkWidth];

        for (var i = 0; i < chunkWidth * chunkWidth; i++)
        {
            float x = (Mathf.Floor(i / chunkWidth) + chunkPosition.x) * chunkResolution;
            float y = ((i % chunkWidth) + chunkPosition.y) * chunkResolution;

            float val = Mathf.Clamp01(Mathf.PerlinNoise(x, y));

            colorMap[i] = new Color(val, val, val);
        }

        return new MapData(colorMap, null, null);
    }
}
