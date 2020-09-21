using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDecorator : PowerUpDecorator
{
    public void powerUpEffect()
    {
        Debug.Log("Haha speed go brr");
    }

    public IPowerUp Decorate(IPowerUp powerUp)
    {
        powerUp.Points += 100;
        powerUp.usePowerUp = null;
        powerUp.usePowerUp += powerUpEffect;
        return powerUp;
    }
}
