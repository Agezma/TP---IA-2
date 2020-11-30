using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IA2;

public class Enemy : GridEntity,IUpdate, IFixedUpdate, IRestartable
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
    private ChangeScene scene;
    [HideInInspector] public bool isDead = false;

        
    // IA2-P3
    public EventFSM<EnemyStates> fsm;

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

        UpdateManager.Instance.AddUpdate(this);
        UpdateManager.Instance.AddFixedUpdate(this);
        RestartableManager.Instance.AddRestartable(this);

        UpdateNext(currentWp);

        foreach (var item in coliderContainter.GetComponents<Collider>())
        {
            coliders.Add(item);
        }

        StartCoroutine(UpdateGridCoRoutine());

        FSMConfiguration();
    }
    //IA2-P3
    public void FSMConfiguration()
    {
        var move = new State<EnemyStates>("Move");
        var seachWP = new State<EnemyStates>("SeachWP");
        var attack = new State<EnemyStates>("FirstAttack");
        var secondAttack = new State<EnemyStates>("SecondAttack");
        var die = new State<EnemyStates>("Dead");

        StateConfigurer.Create(seachWP)
            .SetTransition(EnemyStates.move, move)
            .SetTransition(EnemyStates.die, die)
            .Done();

        StateConfigurer.Create(move)
            .SetTransition(EnemyStates.searchNextWp, seachWP)
            .SetTransition(EnemyStates.firstAttack, attack)
            .SetTransition(EnemyStates.die, die)
            .Done();

        StateConfigurer.Create(attack)
            .SetTransition(EnemyStates.secondAttack, secondAttack)
            .SetTransition(EnemyStates.die, die)
            .Done();

        StateConfigurer.Create(die)
            .Done();
                       

        move.OnUpdate += () =>
        {
            Move();

            if (Vector3.Distance(this.transform.position, baseToAttack.transform.position) <= attackRange)
                fsm.SendInput(EnemyStates.firstAttack);
        };     

        move.OnFixedUpdate += () =>
        {
            Move();
        };

        seachWP.OnEnter += (x) =>
        {
            currentWp = nextWp;
            UpdateNext(currentWp);
        };
        seachWP.OnUpdate += () => fsm.SendInput(EnemyStates.move);

        attack.OnEnter += (x) => DoAttack();

        die.OnEnter += (x) =>
        {
            _rb.velocity = Vector3.zero;

            StartCoroutine(Die());
        };
    

        fsm = new EventFSM<EnemyStates>(move);
    }

    void MoveToNextWaypoint(Waypoint current)
    {
        var distance = Vector3.Distance(transform.position, nextWp.transform.position);
        if (distance <= 2 && !current.isBase)
        {
            fsm.SendInput(EnemyStates.searchNextWp);
        }
    }

    void UpdateNext(Waypoint current)
    {
        if (current.isBase) return;

        nextWp = current.nextPoint[UnityEngine.Random.Range(0, current.nextPoint.Length)];
        var dir = nextWp.transform.position - transform.position;

        transform.forward = dir;
    }

    public void OnUpdate()
    {
        fsm.Update();
    }

    public void OnFixedUpdate()
    {
        fsm.FixedUpdate();
    }

    //IA2-P2
    IEnumerator UpdateGridCoRoutine()
    {
        while (true)
        {
            UpdateGrid();
            yield return new WaitForSeconds(1f);
        }
    }

    public void Attack(float damage)
    {
        baseToAttack.TakeDamage(damage);
    }

    public void Move()
    {
        MoveToNextWaypoint(currentWp);        

        _rb.velocity = transform.forward * speed;
        anim.SetFloat("Speed", _rb.velocity.magnitude / startSpeed);
    }

    public virtual void TakeDamage(float damage)
    {
        if (isDead) return;

        life -= damage;
        myLifeBar.UpdateLife();

        speed -= startSpeed / startLife;        

        if (life <= 0)
        {
            fsm.SendInput(EnemyStates.die);
        }
    }

    public void DoAttack()
    {
        transform.LookAt(baseToAttack.transform);
        anim.SetBool("Attack", true);
        speed = 0;
        anim.SetFloat("Speed", speed);
    }

    public IEnumerator Die()
    {
        isDead = true;
        anim.Play("Die");
        UpdateManager.Instance.RemoveUpdate(this);
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

        OnDestroyed.Invoke();

        yield return new WaitForSeconds(deathAnimTime);

        myMoney.money += moneyReward;
        Destroy(this.gameObject);
    }

    public void SetOnWaveEnd()
    {

    }

    public void RestartFromLastWave()
    {
        UpdateManager.Instance.RemoveUpdate(this);
        UpdateManager.Instance.RemoveFixedUpdate(this);
        RestartableManager.Instance.RemoveRestarable(this);
        Main.Instance.enemyManager.RemoveEnemy(this);
        
        Destroy(myLifeBar.lifeBar.gameObject);       
        Destroy(this.gameObject);
    } 
}

public enum EnemyStates
{
    move,
    searchNextWp,
    firstAttack,
    secondAttack,
    die
}