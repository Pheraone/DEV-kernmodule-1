using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    Vector3 Execute(GameObject actor);
}
