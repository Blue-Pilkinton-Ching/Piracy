using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementSettings", menuName = "ScriptableObjects/PlayerMovementSettings", order = 0)]
public class PlayerMovementSettings : ScriptableObject {
    public float SprintSpeed;
    public float WalkSpeed;
    public float SneakSpeed;

    public float BackwardsSpeedMultiplier = 0.5f;
    public float SidewaysSpeedMultiplier = 0.8f;
}