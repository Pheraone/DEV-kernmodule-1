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

        _grid = new ICell[_size._x, _size._y];
        GenerateGrid();
        NewLevel();
    }

    private void GenerateGrid()
    {
        //ToDo use object pool fopr cells
        for (int x = 0; x < _size._x; x++)
        {
            for (int y = 0; y < _size._y; y++)
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
        Coordinate startCoordinate = new Coordinate(_size._x / 2, _size._y / 2);
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
                _grid[thisCoordinate._x, thisCoordinate._y].Cost = 0;
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
                    while (_grid[thisCoordinate._x + nextDirection._x, thisCoordinate._y + nextDirection._y].Cost == 0);

                    loopTimer = 0;
                    thisCoordinate += nextDirection;

                    loopStartDirection = nextDirection;
                }

                finishedLoop = false;
                //how long is this side?
                for (int y = 0; y < Random.Range(2, 6); y++)
                {
                    if (_grid[thisCoordinate._x, thisCoordinate._y].Cost < 0)
                    {
                        _grid[thisCoordinate._x, thisCoordinate._y].Cost = 0;
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
                    previousDirection = loopStartDirection * -1;
                    //previousDirection = nextDirection;
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
            while (_grid[thisCoordinate._x + nextDirection._x, thisCoordinate._y + nextDirection._y].Cost == 0);

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
        Coordinate thisCoordinate = new Coordinate(0,0);
        Coordinate startCoordinate = new Coordinate(_size._x/2, _size._y/2);
        int numberOfLoops = 0;
        Coordinate nextDirection = new Coordinate(0, 0);
        Coordinate previousDirection = new Coordinate(0,0);
        bool tryAgain = false;
        int loopOrigin = 0;
        int loopTimer = 0;

        //make little room in the middle
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                thisCoordinate = startCoordinate + new Coordinate(x, y);
                _grid[thisCoordinate._x, thisCoordinate._y].Cost = 0;
                _path.Add(thisCoordinate);
            }
        }

        //while there are less than x loops
        do
        {
            if (previousDirection == new Coordinate(0, 0))
            {
                do
                {
                    loopTimer++;
                    nextDirection = Direction.NotSoRandomDirection(previousDirection);
                    if (loopTimer > 10) break;
                }
                while (_grid[thisCoordinate._x + nextDirection._x, thisCoordinate._y + nextDirection._y].Cost == 0);

                loopTimer = 0;
                thisCoordinate += nextDirection;

                //loopStartDirection = nextDirection;
            }
            //while the coordinates are within the array and there is no cost assigned yet
            do
            {
                //Todo check for loops going back on themselves too soon

                //change the cost of the cell
                if (_grid[thisCoordinate._x, thisCoordinate._y].Cost < 0)
                {
                    _grid[thisCoordinate._x, thisCoordinate._y].Cost = 0;
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

                        if (_grid[thisCoordinate._x, thisCoordinate._y].Cost < 0)
                        {
                            _grid[thisCoordinate._x, thisCoordinate._y].Cost = 0;
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

                if (numberOfLoops > 8) break;
                
                nextDirection = Direction.RandomDirection(previousDirection);
                thisCoordinate += nextDirection;
                previousDirection = nextDirection * -1;
            }
            while (ContainsCoordinates(thisCoordinate));

            while (!ContainsCoordinates(thisCoordinate))
            {
                loopTimer++;
                thisCoordinate += previousDirection;
                nextDirection = Direction.RandomDirection(previousDirection);
                thisCoordinate += nextDirection;
                previousDirection = nextDirection * -1;

                if (loopTimer>10)
                {
                    loopTimer = 0;
                    break;
                }
            }
        }
        while (numberOfLoops <= 8);
    }

    /// <summary>
    /// see if given coordinates are within the set size for the grid
    /// </summary>
    /// <param name="coordinate"></param>
    /// <returns></returns>
    public bool ContainsCoordinates(Coordinate coordinate)
    {
        return coordinate._x >= 1 && coordinate._x < _size._x -1 && coordinate._y >= 1 && coordinate._y < _size._y-1;
    }
}

public interface ILevelGenerator
{
    Coordinate _size { get; set; }
    ICell[,] _grid { get; }
    void NewLevel();
}