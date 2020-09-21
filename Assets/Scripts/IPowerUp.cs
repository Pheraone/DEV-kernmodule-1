﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPowerUp
{
    public delegate void powerFunction();
    public powerFunction usePowerUp;
    public int Points { get; set; }
    public abstract int PickUp();
}
