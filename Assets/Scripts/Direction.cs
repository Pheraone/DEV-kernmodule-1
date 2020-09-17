using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Direction
{
    public static Coordinate[] DirectionVectors { get; private set; } =
    {
        new Coordinate(0, 1),
        new Coordinate(1, 0),
        new Coordinate(0, -1),
        new Coordinate(-1, 0)
    };

    /// <summary>
    /// gives a random direction that is not the given direction
    /// </summary>
    /// <param name="back"></param>
    /// <returns></returns>
    public static Coordinate RandomDirection(Coordinate back)
    {
        Coordinate randomDirection;

        do
        {
            randomDirection = DirectionVectors[Random.Range(0, DirectionVectors.Length)];
        }
        while (randomDirection == back);

        {
            return randomDirection;
        }
    }

    public static Coordinate NotSoRandomDirection(Coordinate lastDirection)
    {
        Coordinate lessRandomDirection;

        if (lastDirection._x == 0)
        {
            do
            {
                lessRandomDirection = DirectionVectors[Random.Range(0, DirectionVectors.Length)];
            }
            while (lessRandomDirection._x == 0);
        }
        else if (lastDirection._y == 0)
        {
            do
            {
                lessRandomDirection = DirectionVectors[Random.Range(0, DirectionVectors.Length)];
            }
            while (lessRandomDirection._y == 0);
        }
        else
        {
            do
            {
                lessRandomDirection = DirectionVectors[Random.Range(0, DirectionVectors.Length)];
            }
            while (lessRandomDirection == lastDirection);
        }
        return lessRandomDirection;
    }
}
