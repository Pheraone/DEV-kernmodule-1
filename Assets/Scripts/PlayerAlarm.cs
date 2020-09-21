using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAlarm
{
    private static float timerInt;
    public static bool TickingTimer()
    {
        timerInt += Time.deltaTime;

        if (timerInt >= 0.5f)
        {
            timerInt = 0;
            return true;
        }

        else return false;
    }
}
