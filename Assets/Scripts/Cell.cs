using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICell
{
    void SetInteraction();
}

public class Cell : ICell
{
    public Coordinate _cellCoordinate;

    public Cell(Coordinate coordinate)
    {
        _cellCoordinate = coordinate;
    }

    public void SetInteraction()
    {

    }
}