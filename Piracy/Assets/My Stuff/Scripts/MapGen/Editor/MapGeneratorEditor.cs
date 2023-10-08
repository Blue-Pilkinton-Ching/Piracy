using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate"))
        {
            int res = 512;

            Texture2D texture = new(res, res);

            texture.filterMode = FilterMode.Point;

            MapGenerator mapGenerator = (MapGenerator)target;

            MapData mapData = mapGenerator.GenerateMap(res, Vector2.zero, 1);

            texture.SetPixels(mapData.ColorMap);
            texture.Apply();

            GameObject.FindGameObjectWithTag("Plane").GetComponent<MeshRenderer>().sharedMaterial.mainTexture = texture;
        }
    }
}