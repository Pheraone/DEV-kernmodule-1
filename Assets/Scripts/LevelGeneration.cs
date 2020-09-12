using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration
{
    [SerializeField] private Coordinate _size;
    private ICell[,] _grid;

    [SerializeField] private GameObject _cellPrefab;


    // ToDo call from GameManager upon start
    void StartFunction()
    {
        if (_size == new Coordinate(0, 0))
        {
            _size = new Coordinate(31, 17);
        }

        _grid = new ICell[_size.x, _size.y];
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                _grid[x, y] = new Cell(new Coordinate(x, y), 1) as ICell;
                //CreateCell(thisCoordinate);
            }
        }
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

    /*
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
    }*/
}
