using Unity.VisualScripting;
using UnityEngine;

public static class Math
{
    public static float WeightedRandom(float min, float max)
    {
        float u, v, S;

        do
        {
            u = 2.0f * UnityEngine.Random.Range(min, max) - 1.0f;
            v = 2.0f * UnityEngine.Random.Range(min, max) - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0);

        float fac = Mathf.Sqrt(-2.0f * Mathf.Log10(S) / S);
        return u * fac;
    }

    public static float EaseInCubic(float x)
    {
        return x* x * x;
    }   

}
