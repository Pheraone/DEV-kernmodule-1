using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    ILevelGenerator _levelGeneration;

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

        _levelGeneration = new LevelGeneration() as ILevelGenerator;
    }

    // Update is called once per frame
    void Update()
    {
        //ICommand commandTemp = _inputHandler.HandleInput();
        //
        //if (commandTemp != null)
        //{
        //    newDirection = commandTemp.Execute(playerObject);
        //}

        if (PlayerAlarm.TickingTimer())
        {
            _player.movePlayer(playerObject, newDirection);
        }
    }

    public void NextLevel()
    {
        _levelGeneration.GenerateLevel();
    }
}
