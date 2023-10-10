using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BakeableAnimationCurves
{
    [SerializeField]
    private AnimationCurve Curve;

    private float[] bake;
    private bool baked = false;

    [field: SerializeField]
    public int Resolution { get; private set; }
    public BakeableAnimationCurves(AnimationCurve curve, int resolution)
    {
        Curve = curve;
        Resolution = resolution;
    }

    public float[] GetBake(int resolution)
    {
        if (baked)
        {
            return bake;
        }

        bake = new float[resolution];

        for (var i = 0; i < resolution; i++)
        {
            float evalPoint = Curve.Evaluate(resolution * i);
            bake[i] = evalPoint;
        }

        return bake;
    }
}
