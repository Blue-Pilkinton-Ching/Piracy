using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BakeableAnimationCurves
{
    [SerializeField]
    private AnimationCurve Curve;

    private float[] bake;

    [field: SerializeField]
    public int Resolution { get; private set; } = 100;
    public BakeableAnimationCurves(AnimationCurve curve, int resolution)
    {
        Curve = curve;
        Resolution = resolution;
    }

    public float[] GetBake()
    {
        if (bake.Length == 0)
        {
            Debug.LogError("Tried To Access Baked Curve Before Bake");
        }

        return bake;
    }

    public float[] CreateBake(int resolution)
    {
        Resolution = resolution;

        bake = new float[resolution];

        bake[0] = Curve.Evaluate(0f);

        for (var i = 1; i < resolution; i++)
        {
            float evalPoint = Curve.Evaluate((float)i / resolution);
            bake[i] = evalPoint;
        }

        return bake;
    }
}
