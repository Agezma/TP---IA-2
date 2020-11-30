using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MissileTurret : NormalTurret
{
    public override void Shoot(Enemy enemy)
    {
        base.Shoot(enemy);
    }

    public override void Update()
    {
        if (state.buildState != TurretState.state.built) return;


        //IA2-P2
        enemiesInRange = targetGrid.EnemyQuery(transform.position, radius).Where(x => !x.isDead).ToList();
        var enemyToShoot = FilterByMissileTurret();
        Aim(enemyToShoot);


        waitReloadTime += Time.deltaTime;
        DoShoot(enemyToShoot);
    }

}
