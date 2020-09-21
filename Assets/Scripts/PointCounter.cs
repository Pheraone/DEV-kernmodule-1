using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCounter
{
    public bool _levelUp = false;
    private int _points;

    public void AddPoints(int amount, int toLevelup)
    {
        _points += amount;
        if (_points >= toLevelup)
        {
            _levelUp = true;
        }
    }

    public void ResetPoints()
    {
        _points = 0;
    }
}
