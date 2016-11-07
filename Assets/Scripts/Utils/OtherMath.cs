using UnityEngine;

public static class OtherMath
{
    //vede se due numeri sono simili tramite un fattore di precisione
    public static bool Similar(float a, float b, float precision)
    {
        float difference = Mathf.Abs(a - b);
        precision = 1f / Mathf.Pow(10, precision);
        return difference < precision;
    }

    public static bool Similar(float a, float b)
    {
        return Similar(a, b, 1);
    }
}
