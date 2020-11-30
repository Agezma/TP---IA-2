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

    protected TurretState state;

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

    public virtual void Update()
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

    //IA2-P1
    //Estan aplicadas cada una en su torreta, pero las dejamos aca para que te sea mas rapido verlas

    public Enemy FileterByHitted()
    {
        return enemiesInRange.SkipWhile(x => x.life < x.startLife).FirstOrDefault();
    }

    public Enemy FilterByMaxLife()
    {
        return enemiesInRange.OrderBy(x => x.life).FirstOrDefault();
    }

    public Enemy FilterByMissileTurret()
    {
        return enemiesInRange.OfType<Dragon>().Concat(enemiesInRange.SkipWhile(x => x.life < x.startLife)).OrderBy(x => x.life).FirstOrDefault();
    }

    public Enemy FilterEnemysMaxMoney()
    {
        return enemiesInRange.OrderBy(x => x.moneyReward).FirstOrDefault();
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
        aggregate:			            (V)
        orderBy:			            (V)
        selectMany/Concat:		        ()
        zip:				            (V)
        take/TakeWhile/Skip/SkipWhile:	(V)
*/
}

