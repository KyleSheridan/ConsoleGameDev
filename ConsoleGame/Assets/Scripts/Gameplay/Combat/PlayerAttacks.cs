using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public Character stats;

    public Collider meleeCol;

    public float meleeDamage { get; private set; }

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
