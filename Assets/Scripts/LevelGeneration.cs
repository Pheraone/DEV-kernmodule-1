using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration
{
    [SerializeField] private Coordinate _size;
    private GameObject[,] _grid;

    [SerializeField] private GameObject _cellPrefab;


    // ToDo call from GameManager upon start
    void StartFunction()
    {
        if (_size == new Coordinate(0, 0))
        {
            _size = new Coordinate(5, 5);
        }

        _grid = new GameObject[_size.x, _size.y];
        GenerateGrid();
        GenerateLevel();
    }
    
    private void GenerateGrid()
    {
        //ToDo fill grid with cells
    }

    private void GenerateLevel()
    {
        //ToDo generate level with difficulty scaled with score
    }

    /// <summary>
    /// see if given coordinates are within the set size for the grid
    /// </summary>
    /// <param name="coordinate"></param>
    /// <returns></returns>
    public bool ContainsCoordinates(Coordinate coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < _size.x && coordinate.y >= 0 && coordinate.y < _size.y;
    }

    /// <summary>
    /// instantiates a cell and places it at the given coordinates
    /// </summary>
    /// <param name="coordinate"></param>
    private GameObject CreateCell(Coordinate coordinate)
    {
        //instantiate cell
        GameObject newCell = new GameObject(coordinate);
        _grid[coordinate.x, coordinate.y] = newCell;
        //newCell.transform.position = new Vector3(coordinate.x * 1.6f - _size.x * 0.7f + 0.2f, coordinate.y * 1.6f - _size.y * 0.7f + 0.2f, 0f);
        return newCell;
    }
}
