using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshData
{
    public Vector3[] Verticies { get; }
    public int[] Tris { get; }

    public MeshData(int[] triangles, Vector3[] vertices)
    {
        Tris = triangles;
        Verticies = vertices;
    }
}
