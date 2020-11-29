    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
    public float damage = 25;
    public ParticleSystem firePS;

    public GameObject flyColiders;
       
    public override void TakeDamage(float damage)
    {
        if (isDead) return;

        life -= damage;
        myLifeBar.UpdateLife();

        if (life <= 0)
        {
            StartCoroutine(Die());
        }
    }


    public void FireAttack()
    {
        StartCoroutine(SpitFire());
    }

    public IEnumerator SpitFire()
    {
        firePS.gameObject.SetActive(true);
        for (int i = 0; i < damage; i++)
        {
            baseToAttack.TakeDamage(1f);
            yield return new WaitForSeconds(0.1f);

        }
        firePS.gameObject.SetActive(false);

    }

    public void StartFly()
    {
        foreach (var item in coliders)
        {
            item.enabled = false;
        }
        flyColiders.SetActive(true);        
    }
    public void EndFly()
    {
        flyColiders.SetActive(false);
        foreach (var item in coliders)
        {
            item.enabled = true;
        }
    }
}
