using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUpDecorator
{
    void PowerUpEffect();
    AbPowerUp Decorate(AbPowerUp powerUp);
}
