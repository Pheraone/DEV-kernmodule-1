using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ILevelGenerator _levelGeneration;
    private ObjectPool<TestPowerUp> _powerUpPool;

    InputHandler _inputHandler;
    Player _player;
    //ToDo: remove once obsolete
    TestPlayer _testPlayer;
    TestEnemy _testEnemy;

    public GameObject playerPrefab;
    public GameObject playerObject;
    Vector3 newDirection;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = Instantiate(playerPrefab);
        _inputHandler = new InputHandler();
        _inputHandler.InputInit();
        _player = new Player();
        _powerUpPool = new ObjectPool<TestPowerUp>();

        _levelGeneration = new LevelGeneration(new TestPlayer(), _powerUpPool) as ILevelGenerator;
    }

    // Update is called once per frame
    void Update()
    {
        ICommand commandTemp = _inputHandler.HandleInput();
        
        if (commandTemp != null)
        {
            newDirection = commandTemp.Execute(playerObject);
        }

        if (PlayerAlarm.TickingTimer())
        {
            _player.MoveActor(playerObject, newDirection, _levelGeneration.Path);
        }
    }

    public void NextLevel()
    {
        _levelGeneration.GenerateLevel();
    }
}
