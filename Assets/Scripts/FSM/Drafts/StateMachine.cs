using System;
using System.Collections.Generic;
using FSM;
using UnityEngine;

public class StateMachine
{
    public State CurrentState;

    public void Initialize(State startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }
    
    public void ChangeState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}






