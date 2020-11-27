using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IFixedUpdate, IRestartable
{
    protected Rigidbody _rb;
    public float speed;
    float startSpeed;
    public float attackRange;


    protected Base baseToAttack;
    protected Canvas worldCanvas;
    protected MoneyManager myMoney;

    public Animator anim;
    public float deathAnimTime;

    public float startLife;
    public float moneyReward;

    [HideInInspector]
    public float life;
    [HideInInspector]
    public EnemyLifeBar myLifeBar;

    private List<Waypoint> waypoints = new List<Waypoint>();
    public Waypoint currentWp;
    [HideInInspector]
    public Waypoint nextWp;
    public GameObject coliderContainter;
    protected List<Collider> coliders = new List<Collider>();
    public bool isDragon = false;
    private ChangeScene scene;
    bool isDead = false;

    void Awake()
    {
        startSpeed = speed;
        anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        myLifeBar = GetComponent<EnemyLifeBar>();
        life = startLife;
        isDead = false;
    }

    protected void Start()
    {
        scene = Main.Instance.sceneManager;
        worldCanvas = Main.Instance.worldCanvas;
        baseToAttack = Main.Instance.baseToAttack;
        myMoney = Main.Instance.myMoneyManager;

        UpdateManager.Instance.AddFixedUpdate(this);
        RestartableManager.Instance.AddRestartable(this);

        UpdateNext(currentWp);

        foreach (var item in coliderContainter.GetComponents<Collider>())
        {
            coliders.Add(item);
        }
    }

    void MoveToNextWaypoint(Waypoint current)
    {
        var distance = Vector3.Distance(transform.position, nextWp.transform.position);
        if (distance <= 2 && !current.isBase)
        {
            current = nextWp;
            UpdateNext(current);
        }
    }
    void UpdateNext(Waypoint current)
    {
        if (current.isBase) return;

        nextWp = current.nextPoint[UnityEngine.Random.Range(0, current.nextPoint.Length)];
        var dir = nextWp.transform.position - transform.position;

        transform.forward = dir;
    }

    public void OnFixedUpdate()
    {
        Move();
    }

    public void Attack(float damage)
    {
        baseToAttack.TakeDamage(damage);
    }

    public void Move()
    {

        if (Vector3.Distance(this.transform.position, baseToAttack.transform.position) <= attackRange)
        {
            StartCoroutine(DoAttack());
        }
        else
        {
            MoveToNextWaypoint(currentWp);
        }        

        if (life <= 0)
            _rb.velocity = Vector3.zero;

        _rb.velocity = transform.forward * speed;

        anim.SetFloat("Speed", _rb.velocity.magnitude / startSpeed);
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        life -= damage;
        myLifeBar.UpdateLife();
        if (!isDragon)
        {
            speed -= startSpeed / startLife;
        }

        if (life <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public IEnumerator DoAttack()
    {
        transform.LookAt(baseToAttack.transform);
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(0.5f);
        speed = 0;

    }

    public IEnumerator Die()
    {
        isDead = true;
        anim.Play("Die");
        UpdateManager.Instance.RemoveFixedUpdate(this);
        Main.Instance.enemyManager.RemoveEnemy(this);
        RestartableManager.Instance.RemoveRestarable(this);
        if (_rb != null)
        {
            Destroy(_rb);
        }
        Destroy(myLifeBar.lifeBar.gameObject);
        foreach (var item in coliders)
        {
            Destroy(item);
        }
        //Main.Instance.mouseLock.UnlockMouse();

        yield return new WaitForSeconds(deathAnimTime);

        myMoney.money += moneyReward;
        Destroy(this.gameObject);

    }

    public void SetOnWaveEnd()
    {

    }

    public void RestartFromLastWave()
    {
        UpdateManager.Instance.RemoveFixedUpdate(this);
        RestartableManager.Instance.RemoveRestarable(this);
        Main.Instance.enemyManager.RemoveEnemy(this);
        
        Destroy(myLifeBar.lifeBar.gameObject);       
        Destroy(this.gameObject);
    }
}
