using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NormalTurret : Turret
{
    public override void Shoot()
    { 
        TurretBullet bullet = (TurretBullet)Instantiate(prefabBullet);
        //UpdateManager.Instance.AddFixedUpdate(bullet.GetComponent<IFixedUpdate>());
        bullet.transform.position = bulletSpawnPos.position;
        bullet.transform.forward = bulletSpawnPos.transform.forward;

        //IA2-P1
        if(enemiesInRange.Any())
            bullet.target = enemiesInRange.First().transform;

        bullet.damage = damage;                
    }    
}
