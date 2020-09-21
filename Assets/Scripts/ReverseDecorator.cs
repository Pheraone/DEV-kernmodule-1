using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseDecorator : PowerUpDecorator
{
    public void powerUpEffect()
    {
        Debug.Log("Haha controls go brr");
    }

    public IPowerUp Decorate(IPowerUp powerUp)
    {
        powerUp.Points += 300;
        powerUp.usePowerUp = null;
        powerUp.usePowerUp += powerUpEffect;
        return powerUp;
    }
}
