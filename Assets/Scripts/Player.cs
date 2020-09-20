using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public Vector3 direction;


    public void movePlayer(GameObject actor, Vector3 newDirection)
    {
        if (newDirection != null)
            direction = newDirection;

        actor.transform.position += direction;
    }
}
