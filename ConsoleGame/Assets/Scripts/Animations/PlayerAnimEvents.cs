using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    public PlayerController controller;

    public Character stats;

    public Collider meleeCol;

    public float meleeDamage { get; private set; }

    public void EndAttack()
    {
        controller.combat.EndAttack();
    }

    public void SpawnMagicBall()
    {
        controller.combat.SpawnMagic(stats.MagicDamage.Value);
    }

    public void SpawnRanged()
    {
        controller.combat.SpawnRanged(stats.RangedDamage.Value);
    }

    public void StartMelee()
    {
        meleeCol.enabled = true;
        meleeDamage = stats.MeleeDamage.Value;
    }

    public void EndMelee()
    {
        meleeCol.enabled = false;
    }
}
