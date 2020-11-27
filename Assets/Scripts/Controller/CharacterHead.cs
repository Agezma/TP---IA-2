using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHead : MonoBehaviour, IUpdate, IFixedUpdate
{
    Rigidbody _rb;
    public float force;

    [SerializeField]private float speed;
    private float initialSpeed;
    private float animSpeed;
    public float gold;

    Vector3 gravity;
    public Animator anim;

    IController controller;
    public Weapon charAttack;
    private bool attackReleased;
    private bool animFinished;

    private float initDamage;

    public AudioSource audioS;

    void Awake()
    {
        _rb = GetComponentInChildren<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        charAttack.anim = anim;

    }

    void Start()
    {
        /*if (Application.platform == RuntimePlatform.Android)
        {
            mobilePad.gameObject.SetActive(true);
            controller =  Main.Instance.mobilePad;

        }
        else
        {
            mobilePad.gameObject.SetActive(false);
            controller = new ComputerController();
        }*/
        controller = Main.Instance.mobilePad;

        initDamage = charAttack.damage;
        initialSpeed = speed;
        animSpeed = 1;
        UpdateManager.Instance.AddFixedUpdate(this);
        UpdateManager.Instance.AddUpdate(this);
    }

    public void OnFixedUpdate()
    {
        _rb.velocity = (transform.forward * controller.VerticalSpeed() + transform.right * controller.HorizontalSpeed()) * speed;
        anim.SetFloat("VerticalSpeed", (controller.VerticalSpeed() * animSpeed));
        anim.SetFloat("HorizontalSpeed", (controller.HorizontalSpeed() * animSpeed));

        anim.SetFloat("AimWalk", Mathf.Clamp(Mathf.Abs(controller.VerticalSpeed()) + Mathf.Abs(controller.HorizontalSpeed()),0,1));
    }

    public void OnUpdate()
    {
        if (controller.AttackStart())
        {
            charAttack.damage = initDamage;
            charAttack.chargedTime = 0f;

            if (charAttack.isAttacking) return;

            animFinished = false;
            attackReleased = false;
            charAttack.StartAttack();
            speed = initialSpeed / 5f;
            animSpeed = 0.5f;
        }
        if (controller.AttackRelease() && charAttack.isAttacking && !animFinished)
        {            
            attackReleased = true;
            anim.SetBool("AttackRelease", true);
            StartCoroutine(SpeedReturn());
        }
        else if(controller.AttackRelease() && charAttack.isAttacking && animFinished)
        {
            if(charAttack.chargedTime > 1f)
            {
                charAttack.damage = initDamage * 5f;
            }
            anim.SetBool("AttackRelease", true);
            StartCoroutine(SpeedReturn());
        }
    }

    IEnumerator SpeedReturn()
    {
        yield return new WaitForSeconds(1f);
        if (!charAttack.isAttacking)
        {
            speed = initialSpeed;
            animSpeed = 1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 12) // Terrain layer
        {
            var collection = Physics.OverlapSphere(transform.position, 5);
            foreach (var x in collection)
            {
                if (x.GetComponent<Rigidbody>() && !x.gameObject.isStatic)
                    x.GetComponent<Rigidbody>().AddForce((x.transform.position - transform.position) * force, ForceMode.Force);
            }
        }
    }
    public void FinishAnim()
    {
        animFinished = true;
    }

    public void ReleaseAttack()
    {
        charAttack.ReleaseAttack();

        StartCoroutine(SpeedReturn());

        audioS.Play();

    }
}
    