using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : AbPowerUp
{
    public override void PickUp()
    {
        Debug.Log(_points);

        _usePowerUp?.Invoke();
    }
}
