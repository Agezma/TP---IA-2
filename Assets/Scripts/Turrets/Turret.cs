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
    public IEnumerable<Enemy> enemiesInRange = new List<Enemy>();

    public abstract void Shoot(Enemy enemy);    

    public virtual void Start()
    {
        //IA2-P2
        targetGrid = Main.Instance.spatialGrid;

        enemyManager = Main.Instance.enemyManager;
        state = GetComponent<TurretState>();
    }
    public void Aim(Enemy enemy)
    {
        if (enemy != null )
        {
            partToRotate.transform.LookAt(new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z));
        }
    }
    private void Update()
    {
        if (state.buildState != TurretState.state.built) return;
        

        //IA2-P2
        enemiesInRange = targetGrid.EnemyQuery( transform.position, radius ).Where(x=>!x.isDead).ToList();
        Aim(enemiesInRange.FirstOrDefault());

        
        waitReloadTime += Time.deltaTime;
        DoShoot(enemiesInRange.FirstOrDefault());
    }

    public virtual void DoShoot(Enemy enemy)
    {
        if (waitReloadTime >= reloadTime && enemiesInRange.Any())
        {
            Shoot(enemy);
            waitReloadTime = 0;
        }
    }      

    public List<Enemy> FilterEnemysMaxLife()
    {
        return enemiesInRange.OrderBy(x => x.life).ToList();
    }

    public Enemy FilterByMaxLife()
    {
        return enemiesInRange.OrderBy(x => x.life).First();
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

