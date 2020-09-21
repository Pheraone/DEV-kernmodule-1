using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseDecorator : IPowerUpDecorator
{
    public void powerUpEffect()
    {
        Debug.Log("Haha controls go brr");
    }

    public AbPowerUp Decorate(AbPowerUp powerUp)
    {
        powerUp._points += 300;
        powerUp._usePowerUp = null;
        powerUp._usePowerUp += powerUpEffect;
        return powerUp;
    }
}
