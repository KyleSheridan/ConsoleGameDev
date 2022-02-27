using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator { get; private set; }
    public PlayerController controller { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimParameters();

        Debug.Log(animator.GetInteger("currentSequence"));
    }

    private void SetAnimParameters()
    {
        Vector2 playerVelocity = new Vector2(controller.rigidbody.velocity.x, controller.rigidbody.velocity.z);

        animator.SetFloat("speed", playerVelocity.magnitude);

        animator.SetBool("isFalling", !controller.grounded);

        animator.SetInteger("currentSequence", controller.combat.currentAttack);

        animator.SetInteger("attackType", (int)controller.combat.sequenceType);

        animator.SetBool("attacking", controller.combat.attacking);
    }
}
