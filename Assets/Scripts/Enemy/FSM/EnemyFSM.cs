﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM
{
    public Enemy _owner { get; private set; }


    IState currentState;
    Dictionary<EnemyStateType, IState> states = new Dictionary<EnemyStateType, IState>();

    public void Initialize(Enemy owner)
    {
        _owner = owner;
    } 

    public void Update()
    {
        currentState?.Update();
    }

    public void SwitchState(EnemyStateType _type)
    {
        currentState?.Exit();
        currentState = states[_type];
        currentState?.Enter();
    }

    public void AddState(EnemyStateType _type, IState _state)
    {
        states.Add(_type, _state);
    }
}
