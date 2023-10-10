using UnityEngine;

[CreateAssetMenu(fileName = "MapGenSettings", menuName = "ScriptableObjects/MapGenSettings", order = 0)]
public class MapGenSettings : ScriptableObject
{
    public BakeableAnimationCurves OceanHeightCurve;
}