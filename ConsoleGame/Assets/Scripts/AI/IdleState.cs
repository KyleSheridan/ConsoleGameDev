using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public ChaseState chaseState;
    public bool canSeeThePlayer;

    public override State RunCurrState()
    {
        if (canSeeThePlayer)
        {
            return chaseState;
        }

        else
        {
            return this;
        }
    }
}
