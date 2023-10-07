using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    
}

public static class FloatExtensions
{
    public static float Remap(this float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        // Clamp the value to be within the original range
        value = Mathf.Clamp(value, fromMin, fromMax);

        // Calculate the percentage of the value within the original range
        float t = (value - fromMin) / (fromMax - fromMin);

        // Map the percentage to the new range and return the result
        return Mathf.Lerp(toMin, toMax, t);
    }

}

public static class ListExtensions
{
    public static T RandomElement<T>(this IList<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}
