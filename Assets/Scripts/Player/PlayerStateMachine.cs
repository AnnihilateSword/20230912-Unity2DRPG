using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currState { get; private set; }

    public void Initialize(PlayerState state)
    {
        currState = state;
        currState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        currState.Exit();
        currState = newState;
        currState.Enter();
    }
}
