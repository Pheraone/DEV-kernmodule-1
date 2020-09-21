using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : IPowerUp
{
    public new powerFunction usePowerUp;
    public override void PickUp()
    {
        usePowerUp.Invoke();
    }
}
