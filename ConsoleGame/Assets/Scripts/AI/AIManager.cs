using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIManager : MonoBehaviour
{
    public float aiHealth;
    public bool dead = false;

    AgentChase agentChase;
    AgentNav agentNav;
    NavMeshAgent navMesh;

    Character stats;

    private void Start()
    {
        agentChase = GetComponent<AgentChase>();
        agentNav = GetComponent<AgentNav>();
        navMesh = GetComponent<NavMeshAgent>();

        stats = GetComponent<Character>();

        aiHealth = stats.Health.Value;
    }
    // Update is called once per frame
    void Update()
    {
        Die();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Melee")
        {
            float baseDamage = other.gameObject.GetComponentInParent<PlayerAnimEvents>().meleeDamage;

            float rawDamage = baseDamage - stats.PhysicalDefence.Value;

            float damage = Mathf.Clamp(rawDamage, 1, baseDamage);

            aiHealth -= damage;
        }
        
        if(other.gameObject.tag == "Ranged")
        {
            float baseDamage = other.gameObject.GetComponent<RangedAttack>().damage;

            float rawDamage = baseDamage - stats.MagicDefence.Value;

            float damage = Mathf.Clamp(rawDamage, 1, baseDamage);

            aiHealth -= damage;

            Destroy(other.gameObject);
        }
        
        if(other.gameObject.tag == "Magic")
        {
            float baseDamage = other.gameObject.GetComponent<RangedAttack>().damage;

            float rawDamage = baseDamage - stats.MagicDefence.Value;

            float damage = Mathf.Clamp(rawDamage, 1, baseDamage);

            aiHealth -= damage;

            Destroy(other.gameObject);
        }
    }

    public void Die()
    {
        if(aiHealth <= 0)
        {
            dead = true;
            agentChase.enabled = false;
            agentNav.enabled = false;
            navMesh.isStopped = true;
            StartCoroutine(DestroyWait(6f));

            
        }
    }

    IEnumerator DestroyWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }

}
