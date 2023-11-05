using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
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

        List<ComputeBuffer> computeBuffers = new List<ComputeBuffer>();

        // Constants Params

        mapDataShader.SetFloat("biomeCount", MapGenSettings.biomeCount);
        mapDataShader.SetFloat("continentScale", MapGenSettings.biomeScale);
        mapDataShader.SetFloat("chunkWidth", chunkWidth);
        mapDataShader.SetVector("chunkPosition", chunkPosition);

        // Map Points Buffer

        ComputeBuffer mapPointsBuffer = new ComputeBuffer(chunkWidth * chunkWidth, 20);
        mapDataShader.SetBuffer(0, "mapPoints", mapPointsBuffer);
        computeBuffers.Add(mapPointsBuffer);

        // HeightCurve Buffers

        CreateHeightCurveBuffer(MapGenSettings.OceanHeightCurve, "oceanHeight");
        CreateHeightCurveBuffer(MapGenSettings.TropicsHeightCurve, "tropicsHeight");

        // Dispatch Compute Shader

        mapDataShader.Dispatch(0, chunkWidth / 8, chunkWidth / 8, 1);

        // Get Map Points Buffer

        MapPointsBufferData[] bufferData = new MapPointsBufferData[chunkWidth * chunkWidth];
        mapPointsBuffer.GetData(bufferData);

        // Set Color

        Color[] colors = new Color[bufferData.Length];
        for (var i = 0; i < bufferData.Length; i++)
        {
            colors[i] = new Color(bufferData[i].color.x, bufferData[i].color.y, bufferData[i].color.z);
        }

        // Dispose of Buffers

        foreach (ComputeBuffer buffer in computeBuffers)
        {
            buffer.Dispose();
        }

        return new MapData(colors, null, null);

        void CreateHeightCurveBuffer(BakeableAnimationCurve curve, string bufferName)
        {
            float[] bake = curve.CreateBake(curve.Resolution);

            ComputeBuffer buffer = new ComputeBuffer(bake.Length, bake.Length * sizeof(float));
            mapDataShader.SetBuffer(0, bufferName, buffer);
            buffer.SetData(bake);

            mapDataShader.SetFloat(bufferName + "Length", bake.Length);

            computeBuffers.Add(buffer);
        }
    }
    struct MapPointsBufferData
    {
        public float3 color;
        public float height;
        public float isSpawnPoint;
    }
}
