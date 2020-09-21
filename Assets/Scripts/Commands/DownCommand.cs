using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownCommand : ICommand
{
    public Vector3 Execute(GameObject actor)
    {
        //player goes down

        return new Vector2(0, -1);
    }
}
