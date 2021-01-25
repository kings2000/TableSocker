using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeTicker : MonoBehaviour
{
    public static DateTime Now => DateTime.UtcNow;

    public static int GetElapsedTimeSec(DateTime time)
    {
        TimeSpan span = time.Subtract(Now);
        return (int)span.TotalSeconds;
    }
}
