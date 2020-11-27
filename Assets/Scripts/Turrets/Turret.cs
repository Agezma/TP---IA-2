using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    public float reloadTime;
    protected float waitReloadTime = 0f;
    public float damage;
    public float range;
    public Transform bulletSpawnPos;
    public GameObject partToRotate;
    public List<Enemy> enemiesInRange = new List<Enemy>();
    [HideInInspector] public List<Enemy> enemies;
    [HideInInspector] public EnemyManager enemyManager;

    TurretState state;

    public string TurretName;
    public string Description;

    public Bullet prefabBullet;


    public abstract void Shoot();
    

    public virtual void Start()
    {
        enemyManager = Main.Instance.enemyManager;
        state = GetComponent<TurretState>();
    }
    public void Aim()
    {
        if (enemiesInRange.Count <= 0 )
            return;

        if(enemiesInRange[0] != null)
        {
            partToRotate.transform.LookAt(new Vector3(enemiesInRange[0].transform.position.x, this.transform.position.y, enemiesInRange[0].transform.position.z));
        }
    }
    private void Update()
    {
        if (state.buildState != TurretState.state.built) return;

        Aim();

        enemies = enemyManager.enemiesAlive;

        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemiesInRange.Contains(enemies[i]))
            {
                AddEnemyInRange(enemies[i]);
            }
        }
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            RemoveEnemyFromRange(enemiesInRange[i]);
        }

        waitReloadTime += Time.deltaTime;

        DoShoot();
    }

    public virtual void DoShoot()
    {
        if (waitReloadTime >= reloadTime && enemiesInRange.Count > 0)
        {
            Shoot();
            waitReloadTime = 0;
        }
    }
        

    public void GetEnemies(List<Enemy> allEnemies)
    {
        enemies = allEnemies;
    }

    public void AddEnemyInRange(Enemy thisEnemy)
    {
        if (Vector3.Distance(thisEnemy.transform.position, transform.position) <= range && thisEnemy != null)
        {
            enemiesInRange.Add(thisEnemy); 
        }
    }

    public void RemoveEnemyFromRange(Enemy thisEnemy)
    {
        if (!Main.Instance.enemyManager.enemiesAlive.Contains(thisEnemy) || (enemiesInRange.Contains(thisEnemy) && Vector3.Distance(thisEnemy.transform.position, transform.position) > range))
        {
            enemiesInRange.Remove(thisEnemy);
            //print("Remove");
        }
    }
}

