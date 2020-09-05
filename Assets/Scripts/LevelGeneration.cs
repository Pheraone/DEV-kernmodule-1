using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private int _minLength, _maxLength;
    private int _length;
    [SerializeField] private int _minHeight, _maxHeight;
    private int _height;

    private Coordinate _size;
    private ICell[,] _grid;

    [SerializeField] private Cell _cellPrefab;


    // Start is called before the first frame update
    void Start()
    {
        //save the length and height of the dungeon as one "Coordinate"
        _length = Random.Range(_minLength, _maxLength);
        _height = Random.Range(_minHeight, _maxHeight);
        _size = new Coordinate(_length, _height);

        _grid = new ICell[_size.x, _size.y];
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateGrid()
    {

    }

    private void GenerateLevel()
    {
        //ToDo generate level with difficulty scaled with score
    }

    /// <summary>
    /// generate a random coordinate within the size of the grid
    /// </summary>
    private Coordinate RandomStartCoordinate
    {
        get
        {
            return new Coordinate(Random.Range(0, _size.x), Random.Range(0, _size.y));
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

    /// <summary>
    /// instantiates a cell and places it at the given coordinates
    /// </summary>
    /// <param name="coordinate"></param>
    private ICell CreateCell(Coordinate coordinate)
    {
        //instantiate cell
        ICell newCell = Instantiate(_cellPrefab, transform) as ICell;
        _grid[coordinate.x, coordinate.y] = newCell;
        newCell._name = "Cell " + coordinate.x + ", " + coordinate.y;
        newCell._coordinate = coordinate;
        //newCell.transform.position = new Vector3(coordinate.x * 1.6f - _size.x * 0.7f + 0.2f, coordinate.y * 1.6f - _size.y * 0.7f + 0.2f, 0f);

        return newCell;
    }
}
