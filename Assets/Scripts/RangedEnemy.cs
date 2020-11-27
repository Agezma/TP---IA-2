using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject prefabBullet;
    public Transform pos;

    public void Attack()
    {
        GameObject bullet = Instantiate(prefabBullet);
        bullet.transform.position = pos.position;
        bullet.transform.forward = transform.forward;
    }

}
