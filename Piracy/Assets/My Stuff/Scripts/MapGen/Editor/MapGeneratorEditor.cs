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
            Texture2D texture = new(500, 500);
            MapGenerator mapGenerator = (MapGenerator)target;

            MapData mapData = mapGenerator.GenerateMap(500, Vector2.zero, 0.1f);

            texture.SetPixels(mapData.ColorMap);
            texture.Apply();

            GameObject.FindGameObjectWithTag("Plane").GetComponent<MeshRenderer>().sharedMaterial.mainTexture = texture;
        }
    }
}