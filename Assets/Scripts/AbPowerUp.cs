using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbPowerUp
{
    public delegate void powerFunction();
    public powerFunction _usePowerUp;
    public int _points { get; set; }
    public abstract void PickUp();
}
