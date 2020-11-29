using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

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

    public string filterSelected;
    public string filterInfo;

    public Bullet prefabBullet;

    //IA2-P2
    public float radius = 20f;
    public SpatialGrid targetGrid;
    public IEnumerable<GridEntity> selected = new List<GridEntity>();
    IEnumerable<GridEntity> myEnemiesInRange;

    public abstract void Shoot();    

    public virtual void Start()
    {
        //IA2-P2
        targetGrid = Main.Instance.spatialGrid;

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

        //IA2-P2
        enemiesInRange = targetGrid.EnemyQuery( transform.position, radius ).Where(x=>!x.isDead).ToList();

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

    public List<Enemy> FilterEnemysMaxLife()
    {
        return enemiesInRange.Where(x => !x.isDragon).OrderBy(x => x.life).ToList();
    }

    public List<Enemy> FilterEnemysMaxMoney()
    {
        return enemiesInRange.OrderBy(x => x.moneyReward).ToList();
    }

    public List<Enemy> FilterEnemysShootOnlyOnce()
    {
        return enemiesInRange.Skip(1).ToList();
    }

    /*public List<Enemy> EnemysInRangeFilter()
    {
        return enemiesInRange.Zip(enemies,(enemiesInRange, enemies) => enemiesInRange).ToList();
    }*/


    /*public List<Enemy> FilterEnemysShootOnlyOnce()
    {
        return enemiesInRange..ToList();
    }*/



    /*
     * select:			            	(V)
        where:				            (V)
        aggregate:			            (?)
        orderBy:			            (V)
        selectMany/Concat:		        ()
        zip:				            (?)
        take/TakeWhile/Skip/SkipWhile:	(V)
*/
}

