using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourForTest : MonoBehaviour
{
    //For testing with monobehaviour
    private EnemyFSM _enemyStateMachine;
   
    private void Awake()
    {
        _enemyStateMachine = new EnemyFSM();   
    }

    void Start()
    {
        //add states to the dictionary
        _enemyStateMachine.AddState(EnemyStateType.Idle, new IdleState());
        _enemyStateMachine.AddState(EnemyStateType.Attack, new AttackState());
        
    }

   
    void Update()
    {
        //Temporary for FSM state switch test
        if (Input.GetKey(KeyCode.Space))
        {
            _enemyStateMachine.SwitchState(EnemyStateType.Idle);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            _enemyStateMachine.SwitchState(EnemyStateType.Attack);
        }
      
       _enemyStateMachine.Update();
    }
}
