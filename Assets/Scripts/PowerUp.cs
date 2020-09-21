using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : IPowerUp
{
    public override void PickUp()
    {
        Debug.Log(points);

        usePowerUp?.Invoke();
    }
}
