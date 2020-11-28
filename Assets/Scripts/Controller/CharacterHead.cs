using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IA2;

public class CharacterHead : MonoBehaviour, IUpdate
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

    public EventFSM<PlayerInputs> fsm;

    void Awake()
    {
        _rb = GetComponentInChildren<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        charAttack.anim = anim;
    }
    
    void Start()
    {           
        controller = new ComputerController();

        initDamage = charAttack.damage;
        initialSpeed = speed;
        animSpeed = 1;
        UpdateManager.Instance.AddUpdate(this);

        FSMConfiguration();
    }


    public void FSMConfiguration()
    {
        var idle = new State<PlayerInputs>("Idle");
        var move = new State<PlayerInputs>("Move");
        var chargeAttack = new State<PlayerInputs>("ChargeAttack");
        var releaseAttack = new State<PlayerInputs>("ReleaseAttack");

        StateConfigurer.Create(idle)
            .SetTransition(PlayerInputs.move, move)
            .SetTransition(PlayerInputs.chargeAttack, chargeAttack)
            .Done();

        StateConfigurer.Create(move)
            .SetTransition(PlayerInputs.idle, idle)
            .SetTransition(PlayerInputs.chargeAttack, chargeAttack)
            .Done();

        StateConfigurer.Create(chargeAttack)
            .SetTransition(PlayerInputs.releaseAttack, releaseAttack)
            .Done();

        StateConfigurer.Create(releaseAttack)
            .SetTransition(PlayerInputs.idle, idle)
            .SetTransition(PlayerInputs.move, move)
            .Done();



        idle.OnUpdate += () =>
        {
            if (controller.HorizontalSpeed() != 0 || controller.VerticalSpeed() != 0)
                fsm.SendInput(PlayerInputs.move);
            else if (controller.AttackStart())
                fsm.SendInput(PlayerInputs.chargeAttack);
        };     

        move.OnUpdate += () =>
        {
            if (controller.HorizontalSpeed() == 0 && controller.VerticalSpeed() == 0)
                fsm.SendInput(PlayerInputs.idle);
            else if (controller.AttackStart())
                fsm.SendInput(PlayerInputs.chargeAttack);
        };

        move.OnFixedUpdate += () =>
        {
            MoveChar();
        };

        chargeAttack.OnEnter += (x) => OnAttackStart();

        chargeAttack.OnUpdate += () =>
        {
            if (controller.AttackRelease())
                fsm.SendInput(PlayerInputs.releaseAttack);
        };
        chargeAttack.OnFixedUpdate += () =>
        {
            MoveChar();
        };

        releaseAttack.OnEnter += (x) => OnAttackReleased();

        releaseAttack.OnUpdate += () =>
        {
            if (controller.HorizontalSpeed() == 0 && controller.VerticalSpeed() == 0)
                fsm.SendInput(PlayerInputs.idle);
            else 
                fsm.SendInput(PlayerInputs.move);
        };
        
        fsm = new EventFSM<PlayerInputs>(idle);
    }

    public void MoveChar()
    {
        _rb.velocity = (transform.forward * controller.VerticalSpeed() + transform.right * controller.HorizontalSpeed()) * speed;
        anim.SetFloat("VerticalSpeed", (controller.VerticalSpeed() * animSpeed));
        anim.SetFloat("HorizontalSpeed", (controller.HorizontalSpeed() * animSpeed));
        anim.SetFloat("AimWalk", Mathf.Clamp(Mathf.Abs(controller.VerticalSpeed()) + Mathf.Abs(controller.HorizontalSpeed()),0,1));
    }

    public void OnAttackStart()
    {
        charAttack.damage = initDamage;
        charAttack.chargedTime = 0f;

        if (charAttack.isAttacking) return;

        animFinished = false;
        attackReleased = false;
        charAttack.StartAttack();
        speed = initialSpeed / 2.5f;
        animSpeed = 0.5f;
    }
    public void OnAttackReleased()
    {
        if (animFinished && charAttack.chargedTime > 1f)
            charAttack.damage = initDamage * 5f;
        else
            attackReleased = true;

        anim.SetBool("AttackRelease", true);
        StartCoroutine(SpeedReturn());
    }
    public void OnUpdate()
    {
        fsm.Update();
        Debug.Log(fsm.Current.Name);
    }

    public void FixedUpdate()
    {
        fsm.FixedUpdate();
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
    
public enum PlayerInputs
{
    idle,
    move,
    chargeAttack,
    releaseAttack
}