using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseDecorator : IPowerUpDecorator
{
    public void PowerUpEffect()
    {
        Debug.Log("Haha controls go brr");
    }

    public AbPowerUp Decorate(AbPowerUp powerUp)
    {
        powerUp._points += 300;
        powerUp._usePowerUp = null;
        powerUp._usePowerUp += PowerUpEffect;
        return powerUp;
    }
}
