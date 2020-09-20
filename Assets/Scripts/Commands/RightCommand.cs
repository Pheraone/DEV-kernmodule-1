using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCommand : ICommand
{
    public Vector3 Execute(GameObject actor)
    {
        //player goes right

        return new Vector2(1,0);
    }
}
