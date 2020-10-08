using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : ISpawnable
{
    private GameObject _thisEnemy;
    public Coordinate Position { get; set; }

    public TestEnemy()
    {
        _thisEnemy = new GameObject();
        _thisEnemy.name = "Enemy";
        _thisEnemy.AddComponent<SpriteRenderer>();
        _thisEnemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cell");
        _thisEnemy.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
    }

    public void SpawnTo(Coordinate spawnPoint)
    {
        Position = spawnPoint;
        _thisEnemy.transform.position = new Vector3(spawnPoint._x, spawnPoint._y, -1);
    }

    public bool CheckCollision(Vector3 playerPos)
    {
        if (_thisEnemy.transform.position == playerPos)
        {
            return true;
        }
        return false;
    }
}
