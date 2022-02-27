using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    public PlayerController controller;

    public void EndAttack()
    {
        controller.combat.EndAttack();
    }
}
