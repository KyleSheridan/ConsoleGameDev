using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimManager : MonoBehaviour
{
    public Animator animator { get; private set; }

    public AgentNav agentNav;

    public AgentChase agentChase;

    public AIManager aiMan;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agentChase = GetComponent<AgentChase>();
        agentNav = GetComponent<AgentNav>();
        aiMan = GetComponent<AIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        SetParameters();

    }

    private void SetParameters()
    {

        animator.SetBool("isWandering", agentNav.wandering);

        animator.SetBool("isChasing", agentChase.chasing);

        animator.SetBool("isAttacking1", agentChase.attacking);

        animator.SetBool("isDead", aiMan.dead);

    }
}
