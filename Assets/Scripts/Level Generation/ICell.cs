using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICell
{
    int Cost{ get; set; }
    Coordinate Position { get; set; }
}
