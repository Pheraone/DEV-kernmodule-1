using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICell
{
    void SetInteraction();
}

public class GameObject : ICell
{
    public Coordinate _cellCoordinate;

    public GameObject(Coordinate coordinate)
    {
        _cellCoordinate = coordinate;
    }

    public void SetInteraction()
    {

    }
}