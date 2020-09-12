using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration
{
    [SerializeField] private Coordinate _size;
    private ICell[,] _grid;

    public LevelGeneration()
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
        //ToDo use object pool fopr cells
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                _grid[x, y] = new Cell(new Coordinate(x, y), (int)Random.Range(0, 1.999f)) as ICell;
            }
        }
    }

    /// <summary>
    /// generate a random coordinate within the size of the dungeon, leaving the borders free
    /// </summary>
    public Coordinate RandomStartCoordinate
    {
        get
        {
            return new Coordinate(Random.Range(1, _size.x - 1), Random.Range(1, _size.y - 1));
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
}
