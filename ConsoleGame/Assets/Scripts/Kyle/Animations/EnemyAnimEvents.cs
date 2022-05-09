using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvents : MonoBehaviour
{
    public Character stats;

    public Collider meleeColLeft;
    public Collider meleeColRight;

    public float meleeDamage { get; private set; }

    public void StartMelee()
    {
        meleeColLeft.enabled = true;
        meleeColRight.enabled = true;
        meleeDamage = stats.MeleeDamage.Value;
    }

    public void EndMelee()
    {
        meleeColLeft.enabled = false;
        meleeColRight.enabled = false;
    }
}
