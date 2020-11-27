using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IUpdate
{
    private Camera fpsCam;

    public float damage;

    public Animator anim;
    public bool isAttacking;
    bool isPressing;
    public Bullet prefabArrow;
    [HideInInspector]
    public float chargedTime = 0;

    public AudioClip audioHitConfirm;
    AudioSource source;
    public GameObject hitConfirm;
    
    private void Start()
    {
        source = GetComponent<AudioSource>();
        fpsCam = Main.Instance.myCam;
        UpdateManager.Instance.AddUpdate(this);
    }

    public void StartAttack()
    {
        if (!isAttacking)
        {
            anim.SetBool("IsAttacking", true);
            anim.SetBool("AttackRelease", false);
            isAttacking = true;
        } 
    }

    public void ReleaseAttack()
    {
        anim.SetBool("IsAttacking", false);
        anim.SetBool("AttackRelease", true);

        RaycastHit hit;
        Vector3 ray = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 100));

        //Debug.DrawRay(transform.position, ray - transform.position, Color.red, 2f);

        Vector3 dir = ray - transform.position;

        if(Physics.Raycast(fpsCam.transform.position, ray - fpsCam.transform.position, out hit))
        {            
            dir = hit.point - transform.position;

            //Debug.DrawRay(transform.position, dir, Color.blue, 2f);     

            Enemy enemyHit = hit.transform.GetComponent<Enemy>();
            if (enemyHit != null)
            {
                enemyHit.TakeDamage(damage);
                source.clip = audioHitConfirm;
                source.Play();
                StartCoroutine(hitConfirmed());
            }
        }

        Bullet arrow = Instantiate(prefabArrow);
        arrow.transform.position = transform.position;
        arrow.transform.forward = dir;

        isAttacking = false;
    }

    public void OnUpdate()
    {
        chargedTime += Time.deltaTime;

        if (chargedTime > 10f) chargedTime = 2f;
    }
    IEnumerator hitConfirmed()
    {
        hitConfirm.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        hitConfirm.SetActive(false);
    }
}
