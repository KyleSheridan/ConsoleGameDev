using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public override State RunCurrState()
    {
        Debug.Log("Attacked");
        return this;
    }
}
