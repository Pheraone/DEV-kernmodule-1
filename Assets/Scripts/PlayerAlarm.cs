using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAlarm
{
    private static float TIMER_INT;
    public static bool TickingTimer()
    {
        TIMER_INT += Time.deltaTime;

        if (TIMER_INT >= 0.5f)
        {
            TIMER_INT = 0;
            return true;
        }

        else return false;
    }
}
