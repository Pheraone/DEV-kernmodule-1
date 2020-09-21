using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    void MoveActor(GameObject actor, Vector3 newDirection, List<Coordinate> possibleMoves);
}
