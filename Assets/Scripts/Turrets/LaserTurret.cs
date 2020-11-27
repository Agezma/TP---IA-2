using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (enemiesInRange.Count > 0 && !prefabBullet.isActiveAndEnabled)
        {
            Shoot();
        }
        else if(enemiesInRange.Count <= 0 && prefabBullet.isActiveAndEnabled) prefabBullet.gameObject.SetActive(false);
    }

    public override void Shoot()
    {
        prefabBullet.gameObject.SetActive(true);
    }
}
