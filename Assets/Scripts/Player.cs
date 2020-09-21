using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : IMoveable, ISpawnable
{
    public Vector3 direction;

    GameObject _thisPlayer;
    public Coordinate Position { get; set; }

    public Player(GameObject player)
    {
        _thisPlayer = player;
    }

    public void SpawnTo(Coordinate spawnPoint)
    {
        Position = spawnPoint;
        _thisPlayer.transform.position = new Vector3(spawnPoint._x, spawnPoint._y, -2);
    }

    public void MoveActor(GameObject actor, Vector3 newDirection, List<Coordinate> possibleMoves)
    {
        if (newDirection != null)
        {
            Vector2 futureStep = actor.transform.position + newDirection;
            Coordinate oneStepAhead = new Coordinate((int)futureStep.x, (int)futureStep.y);

            if (possibleMoves.Contains(oneStepAhead))
            {
                Debug.Log("Changing route");
                direction = newDirection;
                actor.transform.position += direction;
            }
        }
    }
}
