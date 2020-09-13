using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : ILevelGenerator
{
    public Coordinate _size { get; set; }
    public ICell[,] _grid { get; private set; }
    public List<Coordinate> _path { get; private set; }

    public LevelGeneration()
    {
        if (_size == new Coordinate(0, 0))
        {
            _size = new Coordinate(31, 17);
        }

        _grid = new ICell[_size.x, _size.y];
        GenerateGrid();
        NewLevel();
    }

    private void GenerateGrid()
    {
        //ToDo use object pool fopr cells
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                _grid[x, y] = new Cell(new Coordinate(x, y), -1) as ICell;
            }
        }
    }

    public void NewLevelSimple()
    {
        _path = new List<Coordinate>();
        // start from the middle
        Coordinate thisCoordinate = new Coordinate(0,0);
        Coordinate startCoordinate = new Coordinate(_size.x / 2, _size.y / 2);
        Coordinate loopStartDirection = new Coordinate(0, 0);
        Coordinate nextDirection = new Coordinate(0, 0);
        Coordinate previousDirection = new Coordinate(0, 0);

        int numberOfLoops = 5;
        bool finishedLoop = false;

        int loopTimer = 0;

        //make little room in the middle
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                thisCoordinate = startCoordinate + new Coordinate(x, y);
                _grid[thisCoordinate.x, thisCoordinate.y].Cost = 0;
                _path.Add(thisCoordinate);
            }
        }

        //how many loops?
        for (int n = 0; n < numberOfLoops; n++)
        {
            // Max 4 sides to a rectangle
            for (int x = 0; x <= 3; x++)
            {
                if (previousDirection == new Coordinate(0, 0))
                {
                    do
                    {
                        loopTimer++;
                        nextDirection = Direction.NotSoRandomDirection(previousDirection);
                        if (loopTimer > 10) break;
                    }
                    while (_grid[thisCoordinate.x + nextDirection.x, thisCoordinate.y + nextDirection.y].Cost == 0);
                    loopTimer = 0;
                    thisCoordinate += nextDirection;

                    loopStartDirection = nextDirection;
                }

                finishedLoop = false;
                //how long is this side?
                for (int y = 0; y < Random.Range(4, 10); y++)
                {
                    if (_grid[thisCoordinate.x, thisCoordinate.y].Cost < 0)
                    {
                        _grid[thisCoordinate.x, thisCoordinate.y].Cost = 0;
                        _path.Add(thisCoordinate);

                        thisCoordinate += nextDirection;
                        if (!ContainsCoordinates(thisCoordinate)) break;
                    }
                    else
                    {
                        finishedLoop = true;
                        break;
                    }
                }
                if (finishedLoop) break;

                if (x==2)
                {
                    nextDirection = loopStartDirection * -1;
                    previousDirection = nextDirection;
                }
                nextDirection = Direction.NotSoRandomDirection(previousDirection);
                previousDirection = nextDirection;
            }

            thisCoordinate = _path[(int)Random.Range(0, _path.Count - 1)];
            previousDirection = new Coordinate(0, 0);

            do
            {
                loopTimer++;
                nextDirection = Direction.NotSoRandomDirection(previousDirection);
                if (loopTimer > 10) break;
            }
            while (_grid[thisCoordinate.x + nextDirection.x, thisCoordinate.y + nextDirection.y].Cost == 0);
            loopTimer = 0;

            thisCoordinate += nextDirection;
            previousDirection = nextDirection;
        }
    }


    public void NewLevel()
    {
        _path = new List<Coordinate>();
        List<Coordinate> thisLoop = new List<Coordinate>();
        
        // start from the middle
        Coordinate thisCoordinate = new Coordinate(_size.x/2, _size.y/2);
        int numberOfLoops = 0;
        Coordinate nextDirection = new Coordinate(0, 0);
        Coordinate previousDirection = new Coordinate(0,0);
        bool tryAgain = false;
        int loopOrigin = 0;
        int looptimer = 0;

        //while there are less than x loops
        do
        {
            //while the coordinates are within the array and there is no cost assigned yet
            do
            {
                //ToDo check for adjecent cells and avoid them
                //Todo check for loops going back on themselves

                //change the cost of the cell
                if (_grid[thisCoordinate.x, thisCoordinate.y].Cost < 0)
                {
                    _grid[thisCoordinate.x, thisCoordinate.y].Cost = 0;
                    _path.Add(thisCoordinate);
                    thisLoop.Add(thisCoordinate);

                    thisCoordinate += nextDirection;
                    do
                    {
                        tryAgain = false;
                        while (!ContainsCoordinates(thisCoordinate))
                        {
                            tryAgain = true;
                            thisCoordinate += previousDirection;
                            nextDirection = Direction.RandomDirection(previousDirection);
                            thisCoordinate += nextDirection;
                        }
                        previousDirection = nextDirection * -1;
                        if (_grid[thisCoordinate.x, thisCoordinate.y].Cost < 0)
                        {
                            _grid[thisCoordinate.x, thisCoordinate.y].Cost = 0;
                            _path.Add(thisCoordinate);
                            thisLoop.Add(thisCoordinate);
                        }
                    }
                    while (tryAgain);
                }
                //take a random tile on the path and try from there
                else 
                {
                    if (_path[loopOrigin] != thisCoordinate)
                    {
                        numberOfLoops++;
                        loopOrigin = (int)Random.Range(0, _path.Count - 1);
                        thisCoordinate = _path[loopOrigin];
                        previousDirection = new Coordinate(0, 0);
                    }
                    else
                    {
                        thisCoordinate += previousDirection;

                    }
                }
                if (numberOfLoops > 4)
                {
                    break;
                }
                nextDirection = Direction.RandomDirection(previousDirection);
                thisCoordinate += nextDirection;
                previousDirection = nextDirection * -1;
            }
            while (ContainsCoordinates(thisCoordinate));

            while (!ContainsCoordinates(thisCoordinate))
            {
                looptimer++;
                thisCoordinate += previousDirection;
                nextDirection = Direction.RandomDirection(previousDirection);
                thisCoordinate += nextDirection;
                previousDirection = nextDirection * -1;

                if (looptimer>10)
                {
                    looptimer = 0;
                    break;
                }
            }
        }
        while (numberOfLoops <= 4);
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
        return coordinate.x >= 1 && coordinate.x < _size.x -1 && coordinate.y >= 1 && coordinate.y < _size.y-1;
    }
}

public interface ILevelGenerator
{
    Coordinate _size { get; set; }
    ICell[,] _grid { get; }
    void NewLevel();
}