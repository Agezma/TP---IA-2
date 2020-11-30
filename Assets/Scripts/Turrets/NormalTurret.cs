using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NormalTurret : Turret
{
    public override void Shoot(Enemy enemy)
    { 
        TurretBullet bullet = (TurretBullet)Instantiate(prefabBullet);
        //UpdateManager.Instance.AddFixedUpdate(bullet.GetComponent<IFixedUpdate>());
        bullet.transform.position = bulletSpawnPos.position;
        bullet.transform.forward = bulletSpawnPos.transform.forward;

        //IA2-P1
        if(enemy != null)
            bullet.target = enemy.transform;

        bullet.damage = damage;                
    }

    public override void Update()
    {
        if (state.buildState != TurretState.state.built) return;


        //IA2-P2
        enemiesInRange = targetGrid.EnemyQuery(transform.position, radius).Where(x => !x.isDead).ToList();
        var enemyToShoot = enemiesInRange.FirstOrDefault();
        Aim(enemyToShoot);


        waitReloadTime += Time.deltaTime;
        DoShoot(enemyToShoot);
    }
}
