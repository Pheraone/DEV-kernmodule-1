using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelGenerator
{
    List<Coordinate> Path { get; set; }
    ICell[,] Grid { get; }
    void GenerateLevel();
}
