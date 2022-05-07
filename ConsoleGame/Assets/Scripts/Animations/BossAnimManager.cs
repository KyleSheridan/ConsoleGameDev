using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimManager : MonoBehaviour
{
    public Animator animator { get; private set; }
    public BossAI boss { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        boss = GetComponent<BossAI>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimParameters();

        //Debug.Log(animator.GetInteger("currentSequence"));
    }

    private void SetAnimParameters()
    {
        animator.SetFloat("speed", boss.navMesh.velocity.magnitude);

        animator.SetBool("attacking", boss.attacking);

        animator.SetInteger("currentAttack", boss.currentAttack);

        animator.SetBool("isAlive", boss.isAlive);
    }
}
