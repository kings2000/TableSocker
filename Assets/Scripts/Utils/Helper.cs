using System;
using System.Collections;
using UnityEngine;

public static class Helper
{
    /// <summary>
    /// Get the time difference between now and any other time
    /// </summary>
    /// <param name="totalBuilingTime">the total time</param>
    /// <returns></returns>
    public static TimeSpan GetLaspTime(DateTime totalBuilingTime) => (totalBuilingTime.Subtract(TimeTicker.Now));

    /// <summary>
    /// used to get time format from the span time eg. 1h:30m
    /// </summary>
    /// <param name="span">the  total time in span time</param>
    /// <returns></returns>
    public static string TimeFormat(TimeSpan span)
    {

        if (span.Days > 0)
            return string.Format("{0}d:{1}h", span.Days, span.Hours);
        else if (span.Hours > 0)
            return string.Format("{0}h:{1}m", span.Hours, span.Minutes);
        else
            return string.Format("{0}:{1}", span.Minutes, span.Seconds);
        
    }

    public static float GetRatePercent01(float currentAmount, float minAmount, float maxAmount)
    {
        currentAmount = Mathf.Clamp(currentAmount, minAmount, maxAmount);

        float minValue = minAmount - currentAmount;
        float maxVlue = minAmount - maxAmount;
        float percent = minValue / maxVlue;
        if (percent <= 0)
        {
            return 0;
        }
        return (float)System.Math.Round(percent, 5);
    }

    public static float GetMainValueOffParcent(float percent, float minValue, float maxValue)
    {
        float diff = maxValue - minValue;
        float ratio = (diff * percent);
        return minValue + ratio;
    }

    public static Vector3 Snap(Vector3 pos, float v)
    {
        float x = pos.x;
        float y = pos.y;
        float z = pos.z;
        x = Mathf.FloorToInt(x / v) * v;
        y = Mathf.RoundToInt(y / v) * v;
        z = Mathf.RoundToInt(z / v) * v;
        return new Vector3(x, y, z);
    }

    public static int snap(int point, int v)
    {
        float x = point;
        return Mathf.FloorToInt(x / v) * v;
    }

    public static float snap(float point, float v)
    {
        float x = point;
        return Mathf.RoundToInt(x / v) * v;
    }
}