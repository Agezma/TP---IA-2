using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : Bullet
{
    [HideInInspector] public Transform target;
    Vector3 pos;
    bool hasAlreadyDamaged = false;

    public override void Start()
    {
        lifeTime = 5f;
        StartCoroutine(DestroyMyBullet(lifeTime));
    }

    public void Update()
    {
        if (target)
        {
            float step = speed * Time.deltaTime; // calculate distance to move

            pos = new Vector3(target.position.x, transform.position.y, target.position.z);

            transform.position = Vector3.MoveTowards(transform.position, pos, step);
        }
        else Destroy(gameObject);
    }
    public virtual void DealDamage(Enemy enemy)
    {
        enemy.TakeDamage(damage);
    }
    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10 && !hasAlreadyDamaged)
        {
            hasAlreadyDamaged = true;
            DealDamage(collision.gameObject.GetComponent<Enemy>());
        }
        base.OnCollisionEnter(collision);
    }
}

