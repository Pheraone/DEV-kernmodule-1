using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : ILevelGenerator
{
    public GameObject _player;
    public Coordinate _size { get; set; }
    public ICell[,] _grid { get; private set; }
    Coordinate _thisCoordinate;
    public List<Coordinate> _path { get; private set; }
    List<Coordinate> _thisLoop;

    public LevelGeneration(GameObject player)
    {
        _player = player;
        if (_size == new Coordinate(0, 0))
        {
            _size = new Coordinate(31, 17);
        }

        _grid = new ICell[_size._x, _size._y];
        GenerateGrid();
        GenerateLevel();
    }

    private void GenerateGrid()
    {
        //ToDo use object pool fopr cells
        for (int x = 0; x < _size._x; x++)
        {
            for (int y = 0; y < _size._y; y++)
            {
                _grid[x, y] = new Cell(new Coordinate(x, y), 1) as ICell;
            }
        }
    }

    public void GenerateLevel()
    {
        NewLevelSimple();
        PopulateLevel();
    }

    private void NewLevelSimple()
    {
        _path = new List<Coordinate>();
        _thisLoop = new List<Coordinate>();
        // start from the middle
        _thisCoordinate = new Coordinate(0,0);
        Coordinate startCoordinate = new Coordinate(_size._x / 2, _size._y / 2);
        Coordinate loopStartDirection = new Coordinate(0, 0);
        Coordinate nextDirection = new Coordinate(0, 0);
        Coordinate previousDirection = new Coordinate(0, 0);

        int maxNumberOfLoops = 10;
        bool finishedLoop = false;
        int lengthSide1;
        int lengthSide2;
        int lengthThisSide;

        int loopTimer = 0;

        //make little room in the middle
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                _thisCoordinate = startCoordinate + new Coordinate(x, y);
                _grid[_thisCoordinate._x, _thisCoordinate._y].Cost = 0;
                _path.Add(_thisCoordinate);
            }
        }

        //how many loops?
        for (int n = 0; n < maxNumberOfLoops; n++)
        {
            lengthSide1 = Random.Range(3, 6);
            lengthSide2 = Random.Range(3, 6);
            // Max 4 sides to a rectangle
            for (int x = 0; x <= 3; x++)
            {
                finishedLoop = false;
                if (previousDirection == new Coordinate(0, 0))
                {
                    do
                    {
                        loopTimer++;
                        nextDirection = Direction.RandomDirection(previousDirection);
                        if (loopTimer > 4)
                        {
                            _thisCoordinate = _path[(int)Random.Range(0, _path.Count - 1)];
                            loopTimer = 0;
                        }
                    }
                    while (_grid[_thisCoordinate._x + nextDirection._x, _thisCoordinate._y + nextDirection._y].Cost == 0);

                    loopTimer = 0;
                    _thisCoordinate += nextDirection;
                    previousDirection = nextDirection * -1;

                    loopStartDirection = nextDirection;
                }

                //how long is this side?
                if (x == 0 || x == 2) lengthThisSide = lengthSide1;
                else lengthThisSide = lengthSide2;

                for (int y = 0; y < lengthThisSide; y++)
                {
                    if (_grid[_thisCoordinate._x, _thisCoordinate._y].Cost >= 1)
                    {
                        //_grid[_thisCoordinate._x, _thisCoordinate._y].Cost = 0;
                        //_path.Add(_thisCoordinate);
                        _thisLoop.Add(_thisCoordinate);

                        _thisCoordinate += nextDirection;
                        if (!ContainsCoordinates(_thisCoordinate))
                        {
                            _thisCoordinate += previousDirection;
                            break;
                        }
                    }
                    else
                    {
                        finishedLoop = true;
                        break;
                    }
                }

                if (finishedLoop) break;

                nextDirection = Coordinate.TurnLeft(nextDirection);
                //nextDirection = Direction.NotSoRandomDirection(previousDirection);
                previousDirection = nextDirection * -1;
            }

            if (_thisLoop.Count >= 10)
            {
                foreach (Coordinate loopCoordinate in _thisLoop)
                {
                    _grid[loopCoordinate._x, loopCoordinate._y].Cost = 0;
                    _path.Add(loopCoordinate);
                }
            }
            else
            {
                n--;
            }

            _thisLoop.Clear();
            _thisCoordinate = _path[(int)Random.Range(0, _path.Count - 1)];
            previousDirection = new Coordinate(0, 0);

        }
        _thisCoordinate = _path[(int)Random.Range(0, _path.Count - 1)];
    }


    private void NewLevel()
    {
        _path = new List<Coordinate>();
        _thisLoop = new List<Coordinate>();
        
        // start from the middle
        _thisCoordinate = new Coordinate(0,0);
        Coordinate startCoordinate = new Coordinate(_size._x/2, _size._y/2);
        int maxNumberOfLoops = 10;
        int numberOfLoops = 0;
        Coordinate nextDirection = new Coordinate(0, 0);
        Coordinate previousDirection = new Coordinate(0,0);
        bool tryAgain = false;
        bool loopingBack = false;
        int loopOrigin = 0;
        int loopTimer = 0;

        //make little room in the middle
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                _thisCoordinate = startCoordinate + new Coordinate(x, y);
                _grid[_thisCoordinate._x, _thisCoordinate._y].Cost = 0;
                _path.Add(_thisCoordinate);
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
                while (_grid[_thisCoordinate._x + nextDirection._x, _thisCoordinate._y + nextDirection._y].Cost == 0);

                loopTimer = 0;
                _thisCoordinate += nextDirection;
                previousDirection = nextDirection * -1;
            }
            //while the coordinates are within the array 
            do
            {
                //change the cost of the cell if there is no cost assigned yet
                if (_grid[_thisCoordinate._x, _thisCoordinate._y].Cost >= 1)
                {
                    AddCost();

                    _thisCoordinate += nextDirection;

                    do
                    {
                        tryAgain = false;
                        while (!ContainsCoordinates(_thisCoordinate))
                        {
                            tryAgain = true;
                            _thisCoordinate += previousDirection;
                            nextDirection = Direction.RandomDirection(previousDirection);
                            _thisCoordinate += nextDirection;
                        }

                        previousDirection = nextDirection * -1;

                        if (_grid[_thisCoordinate._x, _thisCoordinate._y].Cost >= 1)
                        {
                            AddCost();
                        }
                    }
                    while (tryAgain);
                }
                //take a random tile on the path and try from there if the current loop is done/long enough
                else 
                {
                    if (!_thisLoop.Contains(_thisCoordinate) || _thisLoop.Count >= 16)
                    {
                        numberOfLoops++;
                        loopOrigin = (int)Random.Range(0, _path.Count - 1);
                        _thisCoordinate = _path[loopOrigin];
                        _thisLoop.Clear();
                        previousDirection = new Coordinate(0, 0);
                    }
                    else
                    {
                        loopingBack = true;
                        _thisCoordinate += previousDirection;
                    }
                }

                if (numberOfLoops > maxNumberOfLoops) break;

                if (loopingBack)
                {
                    nextDirection = Direction.NotSoRandomDirection(previousDirection);
                    loopingBack = false;
                }
                else
                {
                    nextDirection = Direction.RandomDirection(previousDirection);
                }

                _thisCoordinate += nextDirection;
                previousDirection = nextDirection * -1;
            }
            while (ContainsCoordinates(_thisCoordinate));

            while (!ContainsCoordinates(_thisCoordinate))
            {
                loopTimer++;
                _thisCoordinate += previousDirection;
                nextDirection = Direction.NotSoRandomDirection(previousDirection);
                _thisCoordinate += nextDirection;
                previousDirection = nextDirection * -1;

                if (loopTimer > 10)
                {
                    loopTimer = 0;
                    break;
                }
            }
        }
        while (numberOfLoops <= maxNumberOfLoops);

        //ToDo make sure it doesn't freeze
        //do
        //{
        //    AddCost();
        //    do
        //    {
        //        loopTimer++;
        //        nextDirection = Direction.RandomDirection(previousDirection);
        //        if (loopTimer > 10)
        //        {
        //            loopTimer = 0;
        //            break;
        //        }
        //    }
        //    while (!ContainsCoordinates(_thisCoordinate + nextDirection));
        //
        //    _thisCoordinate += nextDirection;
        //    previousDirection = nextDirection * -1;
        //}
        //while (_grid[_thisCoordinate._x + nextDirection._x, _thisCoordinate._y + nextDirection._y].Cost >= 1);
    }

    private void PopulateLevel()
    {
        _player.transform.position = new Vector3(_thisCoordinate._x, _thisCoordinate._y, -1);
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

    public void AddCost()
    {
        _grid[_thisCoordinate._x, _thisCoordinate._y].Cost = 0;
        _path.Add(_thisCoordinate);
        _thisLoop.Add(_thisCoordinate);
    }

}

public interface ILevelGenerator
{
    Coordinate _size { get; set; }
    ICell[,] _grid { get; }
    void GenerateLevel();
}