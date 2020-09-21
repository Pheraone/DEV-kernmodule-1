using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUpDecorator
{
    void powerUpEffect();
    AbPowerUp Decorate(AbPowerUp powerUp);
}
