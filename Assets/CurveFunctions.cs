using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveFunctions
{
    public static Vector2 Quadratic(Vector2 start, Vector2 middle, Vector2 end, float time)
    {
        Vector2 v0 = Vector2.Lerp(start, middle, time);
        Vector2 v1 = Vector2.Lerp(middle, end, time);
        return Vector2.Lerp(v0, v1, time);
    }
    public static Vector2 Cubic(Vector2 start, Vector2 midSection1, Vector2 midSection2, Vector2 end, float time)
    {
        Vector2 v0 = Quadratic(start, midSection1, midSection2, time);
        Vector2 v1 = Quadratic(midSection1, midSection2, end, time);
        return Vector2.Lerp(v0, v1, time);
    }
}
