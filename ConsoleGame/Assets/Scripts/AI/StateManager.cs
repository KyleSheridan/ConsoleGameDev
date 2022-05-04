using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager
{
    public State currentState;

    //Function called to change the state if there is a state assigned to current state
    public void changeState(State newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        newState.Enter();
    }

    //Execute current state
    public void Update()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }
    }


}
