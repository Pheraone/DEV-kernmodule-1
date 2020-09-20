using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpCommand : ICommand
{
    public Vector3 Execute(GameObject actor)
    {
        //player goes up

        return new Vector2(0, 1);
    }
}
