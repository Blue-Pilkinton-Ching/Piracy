using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using System.Linq;
using Unity.Mathematics;
using System;
using Unity.VisualScripting;
using UnityEditor.Rendering.Universal.ShaderGUI;

public class MapGenerator : MonoBehaviour
{
    public MapGenSettings MapGenSettings;

    [SerializeField]
    private ComputeShader mapDataShader;
    public MapData GenerateMap(int chunkWidth, Vector2 chunkPosition, float chunkResolution = 1)
    {
        chunkPosition *= chunkResolution;

        // Float Params

        mapDataShader.SetFloat("chunkWidth", chunkWidth);
        mapDataShader.SetVector("chunkPosition", chunkPosition);

        // Map Points Buffer

        ComputeBuffer mapPointsBuffer = new ComputeBuffer(chunkWidth * chunkWidth, 20);
        mapDataShader.SetBuffer(0, "mapPoints", mapPointsBuffer);

        // OceanHeight Buffer

        float[] bake = MapGenSettings.OceanHeightCurve.CreateBake(MapGenSettings.OceanHeightCurve.Resolution);
        mapDataShader.SetFloat("oceanHeightLength", bake.Length);

        ComputeBuffer oceanHeightBuffer = new ComputeBuffer(bake.Length, bake.Length * sizeof(float));
        mapDataShader.SetBuffer(0, "oceanHeight", oceanHeightBuffer);

        oceanHeightBuffer.SetData(bake);

        // Dispatch Compute Shader

        mapDataShader.Dispatch(0, chunkWidth / 8, chunkWidth / 8, 1);

        // Get Map Points Buffer

        BufferData[] bufferData = new BufferData[chunkWidth * chunkWidth];
        mapPointsBuffer.GetData(bufferData);

        // Dispose of Buffers

        mapPointsBuffer.Dispose();

        // Set Color

        Color[] colors = new Color[bufferData.Length];
        for (var i = 0; i < bufferData.Length; i++)
        {
            colors[i] = new Color(bufferData[i].color.x, bufferData[i].color.y, bufferData[i].color.z);
        }

        return new MapData(colors, null, null);
    }
    struct BufferData
    {
        public float3 color;
        public float height;
        public float isSpawnPoint;
    }

    struct PerDrawData
    {
        public float _DebugValue;
    }
}
