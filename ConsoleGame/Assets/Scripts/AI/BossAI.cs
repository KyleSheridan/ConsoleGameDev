using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
    public NavMeshAgent navMesh { get; private set; }

    Character stats;

    public Transform player;

    public float aggressiveRange;

    public float attackRange;

    public int numAttacks;

    public float rotationSpeed;

    public GameObject[] spawnableObjects;

    public bool isAlive { get; private set; }

    float bossHealth;
    
    private bool spawnable;

    private bool playerInRange = false;

    private float distToPlayer;

    private bool canAttack;

    public bool attacking { get; private set; }

    public int currentAttack { get; private set; }

    public bool canRotate = false;

    [Header("UI")]
    public Slider healthbar;

    private void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();

        stats = GetComponent<Character>();

        bossHealth = stats.Health.Value;
        spawnable = true;

        isAlive = true;

        distToPlayer = float.MaxValue;

        attacking = false;
        canAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();

        if(!isAlive) { return; }

        if (playerInRange && !healthbar.gameObject.activeInHierarchy)
            healthbar.gameObject.SetActive(true);

        CheckPlayerDist();
        
        if(!playerInRange) { return; }

        MoveToPlayer();

        if (canAttack)
        {
            StartAttack();
        }

        if (canRotate)
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void StartAttack()
    {
        if(attacking) { return; }

        canAttack = false;

        currentAttack = UnityEngine.Random.Range(0, numAttacks);

        attacking = true;
    }

    public void EndAttack()
    {
        attacking = false;

    }

    private void MoveToPlayer()
    {
        if (distToPlayer >= attackRange && !attacking)
        {
            navMesh.SetDestination(player.position);
        }
        else if(!attacking)
            canAttack = true;
    }

    private void CheckPlayerDist()
    {
        distToPlayer = Vector3.Distance(transform.position, player.position);

        if (distToPlayer <= aggressiveRange)
        {
            playerInRange = true;
        }
    }

    public void CheckHealth()
    {
        if (healthbar.gameObject.activeInHierarchy)
        {
            healthbar.value = Mathf.Clamp((bossHealth / stats.Health.Value), 0, stats.Health.Value);
        }

        if (bossHealth <= 0)
        {
            healthbar.gameObject.SetActive(false);

            GetComponent<Collider>().enabled = false;

            isAlive = false;
            navMesh.isStopped = true;

            int randNum = UnityEngine.Random.Range(0, spawnableObjects.Length);
            if (spawnableObjects[randNum] != null && spawnable)
            {
                spawnable = false;
                Instantiate(spawnableObjects[randNum], transform.position, transform.rotation);
            }

            Destroy(gameObject, 6f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Melee")
        {
            float baseDamage = other.gameObject.GetComponentInParent<PlayerAnimEvents>().meleeDamage;

            float rawDamage = baseDamage - stats.PhysicalDefence.Value;

            float damage = Mathf.Clamp(rawDamage, 1, baseDamage);

            bossHealth -= damage;
        }

        if (other.gameObject.tag == "Ranged")
        {
            float baseDamage = other.gameObject.GetComponent<RangedAttack>().damage;

            float rawDamage = baseDamage - stats.MagicDefence.Value;

            float damage = Mathf.Clamp(rawDamage, 1, baseDamage);

            bossHealth -= damage;

            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Magic")
        {
            float baseDamage = other.gameObject.GetComponent<RangedAttack>().damage;

            float rawDamage = baseDamage - stats.MagicDefence.Value;

            float damage = Mathf.Clamp(rawDamage, 1, baseDamage);

            bossHealth -= damage;

            Destroy(other.gameObject);
        }
    }
}
