using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager
{
    //public PowerUpDecorator[] allPowerUps = new PowerUpDecorator[2] { new SpeedDecorator(), new ReverseDecorator() };
    //public List<PowerUp> spawnedPowerUp = new List<PowerUpWithTransform>();
    //public void createRandomPowerUp(Transform powerUpObj)
    //{
    //    IPowerUp newPowerUp = new PowerUp();
    //
    //    int randomInt = Random.Range(0, allPowerUps.Length);
    //
    //    newPowerUp = allPowerUps[randomInt].Decorate(newPowerUp);
    //    PowerUpWithTransform powerUpFinished = new PowerUpWithTransform() { myPowerUp = newPowerUp, myTransform = powerUpObj, myVector2 = powerUpObj.position };
    //    spawnedPowerUp.Add(powerUpFinished);
    //}

    public int checkPickUp(Vector2 playerPos, ObjectPool<PowerUp> powerUpPool)
    {
        int points = 0;
        foreach (PowerUp powerUp in powerUpPool._activeObjects)
        {
            if(Coordinate.ToVector2(powerUp.Position) == playerPos)
            {
                points += powerUp.PickUp();
                powerUpPool.DeactivateObject(powerUp);
                break;
            }
        }
        return points;
    }
}

