using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDecorator : PowerUpDecorator
{
    public override void powerUpEffect()
    {
        //get faster
    }

    public override IPowerUp Decorate(IPowerUp powerUp)
    {
        return powerUp;
    }
}
