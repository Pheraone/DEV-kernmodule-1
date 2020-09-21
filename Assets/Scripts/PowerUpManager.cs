using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager
{
    public IPowerUpDecorator[] _allPowerUps = new IPowerUpDecorator[2] { new SpeedDecorator(), new ReverseDecorator() };
    public List<PowerUpWithTransform> _spawnedPowerUp = new List<PowerUpWithTransform>();
    public void CreateRandomPowerUp(Transform powerUpObj)
    {
        AbPowerUp newPowerUp = new PowerUp();

        int randomInt = Random.Range(0, _allPowerUps.Length);

        newPowerUp = _allPowerUps[randomInt].Decorate(newPowerUp);
        PowerUpWithTransform powerUpFinished = new PowerUpWithTransform() { _myPowerUp = newPowerUp, _myTransform = powerUpObj, _myVector2 = powerUpObj.position };
        _spawnedPowerUp.Add(powerUpFinished);
    }

    public void CheckPickUp(Vector2 playerPos)
    {
        foreach (PowerUpWithTransform powerUp in _spawnedPowerUp)
        {
            if(powerUp._myVector2 == playerPos)
            {
                powerUp._myPowerUp.PickUp();
                _spawnedPowerUp.Remove(powerUp);
                break;
            }
        }
    }
}

