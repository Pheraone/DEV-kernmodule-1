using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : ICell
{
    public Coordinate Position { get; set; }
    public int Cost { get; set; }

    public Cell(Coordinate coordinate, int cost)
    {
        Position = coordinate;
        Cost = cost;
    }
}