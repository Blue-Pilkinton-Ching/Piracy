using UnityEngine;

[CreateAssetMenu(fileName = "EntityMovementSettings", menuName = "ScriptableObjects/EntityMovementSettings", order = 0)]
public class EntityMovementSettings : ScriptableObject
{
    public AnimationCurve DefaultAlertImpactOverDistance;
    public AnimationCurve DefaultAlertImpactOverTime;
}