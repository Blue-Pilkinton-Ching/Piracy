using UnityEngine;

[CreateAssetMenu(fileName = "MapGenSettings", menuName = "ScriptableObjects/MapGenSettings", order = 0)]
public class MapGenSettings : ScriptableObject
{
    public ColorValuePair[] MapColorHeight;

    public class ColorValuePair
    {
        public Color Color;
        public float Value;
    }
}