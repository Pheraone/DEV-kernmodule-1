using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas _winScreen;
    private ILevelGenerator _levelGeneration;
    private ObjectPool<PowerUp> _powerUpPool;

    private PointCounter _score;
    public int _currentLevel = 1;

    InputHandler _inputHandler;
    Player _player;

    PowerUpManager _powerUpManager;

    public GameObject _playerPrefab;
    public GameObject _playerObject;

    Vector3 newDirection;

    private EnemyFSM _enemyStateMachine;
    public Pathfinder2 _pathfinder;

    public GameObject enemyPrefab;
    public GameObject enemyObject;

    public RandomCoordinate _randomCoordinate;

    // Start is called before the first frame update
    void Start()
    {

        _playerObject = Instantiate(_playerPrefab);

        _inputHandler = new InputHandler();
        _inputHandler.InputInit();
        _player = new Player(_playerObject);
        _powerUpPool = new ObjectPool<PowerUp>();

        _levelGeneration = new LevelGeneration(_player, _powerUpPool) as ILevelGenerator;
        _score = new PointCounter();

        _powerUpManager = new PowerUpManager();

        _enemyStateMachine = new EnemyFSM();
        _enemyStateMachine.AddState(EnemyStateType.Idle, new IdleState());
        _enemyStateMachine.AddState(EnemyStateType.Attack, new AttackState());

        _randomCoordinate = new RandomCoordinate(_levelGeneration);
        _pathfinder = new Pathfinder2(_levelGeneration);
    }

    // Update is called once per frame
    void Update()
    {
        ICommand commandTemp = _inputHandler.HandleInput();
        
        if (commandTemp != null)
        {
            newDirection = commandTemp.Execute(_playerObject);
        }

        if (PlayerAlarm.TickingTimer())
        {
            _player.MoveActor(_playerObject, newDirection, _levelGeneration.Path);
            foreach (TestEnemy enemy in _levelGeneration.TestEnemies)
            {
                if (enemy.CheckCollision(_playerObject.transform.position))
                {
                    Die();
                    break;
                }
            }
            int points = _powerUpManager.checkPickUp(_playerObject.transform.position, _powerUpPool);
            _score.AddPoints(points, 300*_currentLevel);
        }

        if (_score._levelUp)
        {
            LevelCleared();
        }
    }

    public void Die()
    {
        _winScreen.gameObject.SetActive(true);
        TextMesh endText = _winScreen.gameObject.GetComponentInChildren<TextMesh>();
        endText.text = "Too bad";
        Time.timeScale = 0;
    }

    public void LevelCleared()
    {
        _score._levelUp = false;
        _score.ResetPoints();
        if (_currentLevel <= 2) _currentLevel = _levelGeneration.GenerateLevel();
        else
        {
            _winScreen.gameObject.SetActive(true);
            TextMesh endText = _winScreen.gameObject.GetComponentInChildren<TextMesh>();
            endText.text = "You did it!";
            Time.timeScale = 0;
        }

        //Temporary for FSM state switch test
        if (Vector2.Distance(enemyObject.transform.position, playerObject.transform.position) > 0.5 )
        {
            _enemyStateMachine.SwitchState(EnemyStateType.Idle);

            Debug.Log(playerObject.transform.position);
        }

        if (Vector2.Distance(enemyObject.transform.position, playerObject.transform.position) <= 0.5)
        {
            _enemyStateMachine.SwitchState(EnemyStateType.Attack);
        }
        _enemyStateMachine.Update();
    }

    //buttons

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

