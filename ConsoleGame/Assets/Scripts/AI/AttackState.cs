using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    Agent owner;
    public AttackState(Agent owner) : base(owner)
    {

    }

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void Execute()
    {
        //Play Attack anim
        //Give damage to player health
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}
