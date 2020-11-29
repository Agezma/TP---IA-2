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
    public override void DoShoot()
    {
        //IA2-P1 linq? aunque el any no era necesario
        if (enemiesInRange.Any() && !prefabBullet.isActiveAndEnabled)
        {
            Shoot();
        }
        else if(!enemiesInRange.Any() && prefabBullet.isActiveAndEnabled) prefabBullet.gameObject.SetActive(false);
    }

    public override void Shoot()
    {
        prefabBullet.gameObject.SetActive(true);
    }
}
