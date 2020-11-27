using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBullet : TurretBullet
{
    public float range;
    public ParticleSystem explosion;

    public override void Start()
    {
        base.Start();
        damage = 2;
    }

    public override void DealDamage(Enemy enemy)
    {
        explosion.transform.SetParent(null);
        explosion.Play();
        explosion.GetComponent<ParticleDestroy>().DestroyThis();

        var inRange = Physics.OverlapSphere(transform.position, range);
        for (int i = 0; i < inRange.Length; i++)
        {
            if (inRange[i].GetComponentInParent<Enemy>())
            {
                inRange[i].GetComponentInParent<Enemy>().TakeDamage(damage);
            }
        }
    }

}
