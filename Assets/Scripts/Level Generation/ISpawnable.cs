using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnable
{
    Coordinate Position { get; set; }
    void SpawnTo(Coordinate spawnPoint);
}
