using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : Bullet
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 14)//Base
            other.gameObject.GetComponent<Base>().TakeDamage(damage);
    }
}
