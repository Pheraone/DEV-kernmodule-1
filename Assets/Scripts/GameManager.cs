using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    ILevelGenerator _levelGeneration;

    InputHandler _inputHandler;
    Player _player;

    public GameObject playerPrefab;
    public GameObject playerObject;
    Vector3 newDirection;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = Instantiate(playerPrefab);
        _inputHandler = new InputHandler();
        _inputHandler.InputInit();
        _levelGeneration = new LevelGeneration() as ILevelGenerator;
        _player = new Player();
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
            _player.movePlayer(playerObject, newDirection);
        }
    }
}
