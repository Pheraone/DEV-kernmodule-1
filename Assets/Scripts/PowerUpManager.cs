using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager
{
    public PowerUpDecorator[] allPowerUps = new PowerUpDecorator[2] { new SpeedDecorator(), new ReverseDecorator() };
    public List<PowerUpWithVector> spawnedPowerUp = new List<PowerUpWithVector>();
    public IPowerUp createRandomPowerUp()
    {
        IPowerUp newPowerUp = new PowerUp();

        int randomInt = Random.Range(0, allPowerUps.Length);

        newPowerUp = allPowerUps[randomInt].Decorate(newPowerUp);
        return newPowerUp;
    }

    public void checkPickUp(Vector2 playerPos)
    {
        foreach (PowerUpWithVector powerUp in spawnedPowerUp)
        {
            if(powerUp.myVector == playerPos)
            {
                powerUp.myPowerUp.usePowerUp.Invoke();
            }
        }
    }
}

