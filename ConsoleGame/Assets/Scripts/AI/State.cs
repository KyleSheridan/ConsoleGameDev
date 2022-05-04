using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Agent agent;

    protected State(Agent _agent)
    {
        this.agent = _agent;
    }
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
