using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : EnemyBase
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        int damage = 1; //  calculate damage here

        DealDamageToPlayer(damage, collision);
    }
}
