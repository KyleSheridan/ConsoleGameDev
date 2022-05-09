using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimEvents : EnemyAnimEvents
{
    public BossAI boss;

    public void EndAttack()
    {
        boss.EndAttack();
    }

    public void StartRotate()
    {
        boss.canRotate = true;
    }

    public void EndRotate()
    {
        boss.canRotate = false;
    }
}
