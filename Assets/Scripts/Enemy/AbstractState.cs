using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

//DISCUSS: the use of the enum
public enum EnemyStateType { Idle, Attack }


public abstract class AbstractState : IState
{
    protected EnemyFSM owner;
    public void Initialize(EnemyFSM _owner)
    {
        this.owner = _owner;
    }
   
    //QUESTION (for me) Virtual or Abstract
    public virtual void Enter()
    {
       
    }

    public virtual void Exit()
    {
       
    }

    public virtual void Update()
    {
       
    }
}


//TODO: give it's own script
public class IdleState : AbstractState
{
    public override void Enter()
    {
        Debug.Log("Idle Enter");
    }

    public override void Exit()
    {
        Debug.Log("Idle Exit");
    }

    public override void Update()
    {
        Debug.Log("Idle Update");
    }

  
}

//TODO: give it's own script
public class AttackState : AbstractState
{
    

    public override void Enter()
    {
        Debug.Log("Attack Enter");
    }

    public override void Exit()
    {
        Debug.Log("Attack Exit");
    }

    public override void Update()
    {
        Debug.Log("Attack Update");
    }
}
