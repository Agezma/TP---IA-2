using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTurret : Turret
{
    public override void Shoot()
    { 
        TurretBullet bullet = (TurretBullet)Instantiate(prefabBullet);
        //UpdateManager.Instance.AddFixedUpdate(bullet.GetComponent<IFixedUpdate>());
        bullet.transform.position = bulletSpawnPos.position;
        bullet.transform.forward = bulletSpawnPos.transform.forward;

        if(enemiesInRange.Count > 0)
            bullet.target = enemiesInRange[0].transform;

        bullet.damage = damage;                
    }    
}
