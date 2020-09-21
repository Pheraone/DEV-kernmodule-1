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

    private EnemyFSM _enemyStateMachine;
    //public Pathfinder _pathfinder;

    public GameObject enemyPrefab;
    public GameObject enemyObject;


    // Start is called before the first frame update
    void Start()
    {
        playerObject = Instantiate(playerPrefab);
        enemyObject = Instantiate(enemyPrefab);
        _inputHandler = new InputHandler();
        _inputHandler.InputInit();
        _player = new Player();

        
        _enemyStateMachine = new EnemyFSM();
        _enemyStateMachine.AddState(EnemyStateType.Idle, new IdleState());
        _enemyStateMachine.AddState(EnemyStateType.Attack, new AttackState());

       // _pathfinder = new Pathfinder(_levelGeneration._path);


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

        //Temporary for FSM state switch test
        if (Input.GetKey(KeyCode.Space) || Vector2.Distance(enemyObject.transform.position, playerObject.transform.position) > 2 )
        {
            _enemyStateMachine.SwitchState(EnemyStateType.Idle);
            //FIXME: game object to Vector2
          //  _pathfinder.FindPath(Vector2.zero, playerObject.transform.position);
            Debug.Log(playerObject.transform.position);
        }

        if (Input.GetKeyDown(KeyCode.F) || Vector2.Distance(enemyObject.transform.position, playerObject.transform.position) <= 2)
        {
            _enemyStateMachine.SwitchState(EnemyStateType.Attack);
        }
        _enemyStateMachine.Update();
    }

    public void NextLevel()
    {
        _levelGeneration.GenerateLevel();
    }
}

