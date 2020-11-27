
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody _rb;
    public float speed;
    [HideInInspector] public float damage;
    public float lifeTime = 3f;
    
    public virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();        
    }

    public virtual void Start()
    {
        _rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        StartCoroutine(DestroyMyBullet(lifeTime));
    }
    
    protected IEnumerator DestroyMyBullet(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
