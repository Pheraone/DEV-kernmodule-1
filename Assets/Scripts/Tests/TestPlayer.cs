using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : ISpawnable
{
    private GameObject _thisPlayer;
    public Coordinate Position { get; set; }

    public TestPlayer()
    {
        _thisPlayer = new GameObject();
        _thisPlayer.name = "Player";
        _thisPlayer.AddComponent<SpriteRenderer>();
        _thisPlayer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pacman");
    }

    public void SpawnTo(Coordinate spawnPoint)
    {
        Position = spawnPoint;
        _thisPlayer.transform.position = new Vector3(spawnPoint._x, spawnPoint._y, -1);
    }
}
