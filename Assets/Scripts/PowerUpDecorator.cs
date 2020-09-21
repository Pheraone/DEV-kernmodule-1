using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PowerUpDecorator
{
    void powerUpEffect();
    IPowerUp Decorate(IPowerUp powerUp);
}
