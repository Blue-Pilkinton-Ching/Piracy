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
    public int Resolution { get; private set; } = 100;
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
        Resolution = resolution;

        bake = new float[resolution];

        for (var i = 1; i < resolution; i++)
        {
            float evalPoint = Curve.Evaluate((float)i / resolution);
            bake[i] = evalPoint;

            Debug.Log(evalPoint);
        }

        return bake;
    }
}
