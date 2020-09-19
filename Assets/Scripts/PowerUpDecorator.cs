using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpDecorator
{
    public abstract void powerUpEffect();
    public abstract IPowerUp Decorate(IPowerUp powerUp);
}
