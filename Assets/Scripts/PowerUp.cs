using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : IPowerUp, ISpawnable, IPoolable
{
    public bool Active { get; set; }
    public Coordinate Position { get; set; }

    private GameObject _thisPowerUp;

    public PowerUp()
    {
        _thisPowerUp = new GameObject();
        _thisPowerUp.name = "PowerUp";
        _thisPowerUp.AddComponent<SpriteRenderer>();
        _thisPowerUp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cell");
        _thisPowerUp.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
    }

    public void OnDisabled()
    {
        _thisPowerUp.SetActive(false);
    } 

    public void OnEnabled()
    {
        _thisPowerUp.SetActive(true);
    }

    public override int PickUp()
    {
        return 300;
        //usePowerUp?.Invoke();
    }

    public void SpawnTo(Coordinate spawnPoint)
    {
        Position = spawnPoint;
        _thisPowerUp.transform.position = new Vector3(spawnPoint._x, spawnPoint._y, -1);
        _thisPowerUp.name = "PowerUp " + spawnPoint._x + ", " + spawnPoint._y;
    }
}
