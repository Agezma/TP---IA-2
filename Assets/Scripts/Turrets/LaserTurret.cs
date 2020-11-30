using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LaserTurret : Turret
{
    public override void Start()
    {
        base.Start();
        prefabBullet.damage = damage;
        prefabBullet.speed = reloadTime;
    }

    public override void Update()
    {
        if (state.buildState != TurretState.state.built) return;


        //IA2-P2
        enemiesInRange = targetGrid.EnemyQuery(transform.position, radius).Where(x => !x.isDead).ToList();
        var enemyToShoot = FilterByMaxLife();
        Aim(enemyToShoot);


        waitReloadTime += Time.deltaTime;
        DoShoot(enemyToShoot);
    }

    public override void DoShoot(Enemy enemy)
    {
        //IA2-P1 linq? aunque el any no era necesario
        if (enemiesInRange.Any() && !prefabBullet.isActiveAndEnabled)
        {
            Shoot(null);
        }
        else if(!enemiesInRange.Any() && prefabBullet.isActiveAndEnabled) prefabBullet.gameObject.SetActive(false);
    }

    public override void Shoot(Enemy enemy)
    {
        prefabBullet.gameObject.SetActive(true);
    }
}
