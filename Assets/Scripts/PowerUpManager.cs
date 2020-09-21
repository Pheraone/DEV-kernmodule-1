using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager
{
    public PowerUpDecorator[] allPowerUps = new PowerUpDecorator[2] { new SpeedDecorator(), new ReverseDecorator() };
    public List<PowerUpWithTransform> spawnedPowerUp = new List<PowerUpWithTransform>();
    public void createRandomPowerUp(Transform powerUpObj)
    {
        IPowerUp newPowerUp = new PowerUp();

        int randomInt = Random.Range(0, allPowerUps.Length);

        newPowerUp = allPowerUps[randomInt].Decorate(newPowerUp);
        PowerUpWithTransform powerUpFinished = new PowerUpWithTransform() { myPowerUp = newPowerUp, myTransform = powerUpObj, myVector2 = powerUpObj.position };
        spawnedPowerUp.Add(powerUpFinished);
    }

    public void checkPickUp(Vector2 playerPos)
    {
        foreach (PowerUpWithTransform powerUp in spawnedPowerUp)
        {
            if(powerUp.myVector2 == playerPos)
            {
                powerUp.myPowerUp.PickUp();
                spawnedPowerUp.Remove(powerUp);
                break;
            }
        }
    }
}

