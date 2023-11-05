using UnityEngine;

[CreateAssetMenu(fileName = "MapGenSettings", menuName = "ScriptableObjects/MapGenSettings", order = 0)]
public class MapGenSettings : ScriptableObject
{
    public float biomeScale = 0.1f;
    public float biomeCount = 3;
    public BakeableAnimationCurve OceanHeightCurve;
    public BakeableAnimationCurve TropicsHeightCurve;
}