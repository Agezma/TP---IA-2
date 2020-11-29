using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MissileBullet : TurretBullet
{
    public float radius;
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

        Debug.Log( Main.Instance.spatialGrid.EnemyQuery(transform.position, radius).Count());
        foreach (var item in Main.Instance.spatialGrid.EnemyQuery(transform.position, radius).ToList() )
        {
            item.TakeDamage(damage);
        }
    } 
}
