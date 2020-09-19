using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourForTest : MonoBehaviour
{
    //For testing with monobehaviour
    private EnemyFSM enemyStateMachine;
   
    private void Awake()
    {
        enemyStateMachine = new EnemyFSM();   
    }

    void Start()
    {
        //add states to the dictionary
        enemyStateMachine.AddState(EnemyStateType.Idle, new IdleState());
        enemyStateMachine.AddState(EnemyStateType.Attack, new AttackState());
        
    }

   
    void Update()
    {
        //Temporary for FSM state switch test
        if (Input.GetKey(KeyCode.Space))
        {
            enemyStateMachine.SwitchState(EnemyStateType.Idle);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            enemyStateMachine.SwitchState(EnemyStateType.Attack);
        }
      
       enemyStateMachine.Update();
    }
}
