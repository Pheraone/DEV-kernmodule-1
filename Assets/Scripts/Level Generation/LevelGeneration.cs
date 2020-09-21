using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : ILevelGenerator
{
    public int _currentLevel = 0;
    public ISpawnable _player;
    private List<ISpawnable> _enemyList;
    private List<ISpawnable> _powerUpList;
    private ObjectPool<TestPowerUp> _powerUpPool;

    public Coordinate Size { get; set; }
    public ICell[,] Grid { get; private set; }

    Coordinate _thisCoordinate;
    public List<Coordinate> Path { get; set; }
    List<Coordinate> _thisLoop;

    public LevelGeneration(ISpawnable player, ObjectPool<TestPowerUp> powerUpPool)
    {
        _player = player;
        _enemyList = new List<ISpawnable>();
        _powerUpList = new List<ISpawnable>();
        _powerUpPool = powerUpPool;

        Path = new List<Coordinate>();

        if (Size == new Coordinate(0, 0))
        {
            Size = new Coordinate(32, 18);
        }

        Grid = new ICell[Size._x, Size._y];

        GenerateGrid();
        GenerateLevel();
    }

    private void GenerateGrid()
    {
        //ToDo use object pool fopr cells
        for (int x = 0; x < Size._x; x++)
        {
            for (int y = 0; y < Size._y; y++)
            {
                Grid[x, y] = new Cell(new Coordinate(x, y), 1) as ICell;
            }
        }
    }

    public void GenerateLevel()
    {
        ClearPreviousLevel();
        _currentLevel++;
        NewLevelSimple();
        GenerateEnemies(_currentLevel);
        GeneratePowerUps(_currentLevel * 2);
        PopulateLevel();
    }

    void ClearPreviousLevel()
    {
        foreach (Coordinate pathCoordinate in Path)
        {
            if (ContainsCoordinates(pathCoordinate))
            {
                Grid[pathCoordinate._x, pathCoordinate._y].Cost = 1;
            }
        }
    }

    void GenerateEnemies(int enemyAmount)
    {
        int currentEnemies = _enemyList.Count;
        for (int i = 0; i < enemyAmount; i++)
        {
            if (i > currentEnemies - 1)
            {
                _enemyList.Add(new TestEnemy() as ISpawnable);
            }
        }
    }

    void GeneratePowerUps(int powerUpAmount)
    {
        _powerUpList.Clear();
        for (int i = 0; i < powerUpAmount; i++)
        {
            _powerUpList.Add(_powerUpPool.RequestObject() as ISpawnable);
        }
    }

    private void NewLevelSimple()
    {
        Path = new List<Coordinate>();
        _thisLoop = new List<Coordinate>();
        // start from the middle
        _thisCoordinate = new Coordinate(0,0);
        Coordinate startCoordinate = new Coordinate(Size._x / 2, Size._y / 2);
        Coordinate loopStartDirection = new Coordinate(0, 0);
        Coordinate nextDirection = new Coordinate(0, 0);
        Coordinate previousDirection = new Coordinate(0, 0);

        int maxNumberOfLoops = 15;
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
                Grid[_thisCoordinate._x, _thisCoordinate._y].Cost = 0;
                Path.Add(_thisCoordinate);
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
                            _thisCoordinate = Path[(int)Random.Range(0, Path.Count - 1)];
                            loopTimer = 0;
                        }
                    }
                    while (Grid[_thisCoordinate._x + nextDirection._x, _thisCoordinate._y + nextDirection._y].Cost == 0);

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
                    if (Grid[_thisCoordinate._x, _thisCoordinate._y].Cost >= 1)
                    {
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
                previousDirection = nextDirection * -1;
            }

            if (_thisLoop.Count >= 10)
            {
                foreach (Coordinate loopCoordinate in _thisLoop)
                {
                    Grid[loopCoordinate._x, loopCoordinate._y].Cost = 0;
                    Path.Add(loopCoordinate);
                }
            }
            else
            {
                n--;
            }

            _thisLoop.Clear();
            _thisCoordinate = Path[(int)Random.Range(0, Path.Count - 1)];
            previousDirection = new Coordinate(0, 0);

        }
        _thisCoordinate = Path[(int)Random.Range(0, Path.Count - 1)];
    }


    //ToDo: remove
    private void NewLevel()
    {
        Path = new List<Coordinate>();
        _thisLoop = new List<Coordinate>();
        
        // start from the middle
        _thisCoordinate = new Coordinate(0,0);
        Coordinate startCoordinate = new Coordinate(Size._x/2, Size._y/2);
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
                Grid[_thisCoordinate._x, _thisCoordinate._y].Cost = 0;
                Path.Add(_thisCoordinate);
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
                while (Grid[_thisCoordinate._x + nextDirection._x, _thisCoordinate._y + nextDirection._y].Cost == 0);

                loopTimer = 0;
                _thisCoordinate += nextDirection;
                previousDirection = nextDirection * -1;
            }
            //while the coordinates are within the array 
            do
            {
                //change the cost of the cell if there is no cost assigned yet
                if (Grid[_thisCoordinate._x, _thisCoordinate._y].Cost >= 1)
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

                        if (Grid[_thisCoordinate._x, _thisCoordinate._y].Cost >= 1)
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
                        loopOrigin = (int)Random.Range(0, Path.Count - 1);
                        _thisCoordinate = Path[loopOrigin];
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
        Coordinate spawnPoint;

        _player.SpawnTo(_thisCoordinate);

        foreach (ISpawnable enemy in _enemyList)
        {
            do
            {
                spawnPoint = Path[(int)Random.Range(0, Path.Count - 1)];
            }
            while (spawnPoint == _thisCoordinate);
            enemy.SpawnTo(spawnPoint);
        }

        foreach (ISpawnable powerUp in _powerUpList)
        {
            do
            {
                spawnPoint = Path[(int)Random.Range(0, Path.Count - 1)];
            }
            while (spawnPoint == _thisCoordinate);
            powerUp.SpawnTo(spawnPoint);
        }
    }

    /// <summary>
    /// see if given coordinates are within the set size for the grid
    /// </summary>
    /// <param name="coordinate"></param>
    /// <returns></returns>
    public bool ContainsCoordinates(Coordinate coordinate)
    {
        return coordinate._x >= 1 && coordinate._x < Size._x -1 && coordinate._y >= 1 && coordinate._y < Size._y-1;
    }

    public void AddCost()
    {
        Grid[_thisCoordinate._x, _thisCoordinate._y].Cost = 0;
        Path.Add(_thisCoordinate);
        _thisLoop.Add(_thisCoordinate);
    }

}