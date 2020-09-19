using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCommand : ICommand
{
    public Vector3 Execute(GameObject actor)
    {
        //player goes left

        return new Vector2(-1,0);
    }
}
