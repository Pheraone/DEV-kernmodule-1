using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PowerUpDecorator
{
    void PowerUpEffect();
    AbPowerUp Decorate(AbPowerUp powerUp);
}
