using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : Bullet
{
    List<Enemy> enemiesTouched = new List<Enemy>();


    public override void Start()
    {
        enemiesTouched = new List<Enemy>();
        StartCoroutine(DealDamage(damage, speed));
    }

    IEnumerator DealDamage(float damage, float time)
    {
        while (true)
        {
            Debug.Log(enemiesTouched.Count);
            if (enemiesTouched.Count > 0)
            {
                for (int i = 0; i < enemiesTouched.Count; i++)
                {
                    Debug.Log("Deal damage to " + enemiesTouched[0]);
                    enemiesTouched[0].TakeDamage(damage);
                }
            }
            yield return new WaitForSeconds(time);
        }
    }

    private void Update()
    {
        for (int i = 0; i < enemiesTouched.Count; i++)
        {
            if (!Main.Instance.enemyManager.enemiesAlive.Contains(enemiesTouched[i]))
            {
                enemiesTouched.RemoveAt(i);
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        Enemy current = other.gameObject.GetComponentInParent<Enemy>();
        if (other.gameObject.layer == 10 && !enemiesTouched.Contains(current))
        {
            enemiesTouched.Add(current);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Enemy current = other.gameObject.GetComponentInParent<Enemy>();
        if (other.gameObject.layer == 10 && !enemiesTouched.Contains(current))
        {
            enemiesTouched.Add(current);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Enemy current = other.gameObject.GetComponentInParent<Enemy>();
        if (other.gameObject.layer == 10 && enemiesTouched.Contains(current))
        {
            enemiesTouched.Remove(current);
        }
    }



}
