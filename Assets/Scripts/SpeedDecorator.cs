using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDecorator : IPowerUpDecorator
{
    public void PowerUpEffect()
    {
        Debug.Log("Haha speed go brr");
    }

    public AbPowerUp Decorate(AbPowerUp powerUp)
    {
        powerUp._points += 100;
        powerUp._usePowerUp = null;
        powerUp._usePowerUp += PowerUpEffect;
        return powerUp;
    }
}
