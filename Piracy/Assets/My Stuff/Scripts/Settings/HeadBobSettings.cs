using UnityEngine;
using Cinemachine;

[CreateAssetMenu(fileName = "HeadBobSettings", menuName = "ScriptableObjects/HeadBobSettings", order = 0)]
public class HeadBobSettings : ScriptableObject {
    public AnimationCurve BobHeight;
    public AnimationCurve BobSpeed;
    public AnimationCurve XBobAmount;
    public AnimationCurve YBobAmount;
    public AnimationCurve DutchBobAmount;
    public AnimationCurve PanBobAmount;
    public AnimationCurve TiltBobAmount;

    public bool TiltBobOnSecondFootstep = false;

    public float BobBlendTime = 0.2f;
}