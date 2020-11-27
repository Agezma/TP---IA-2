using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> enemiesAlive;

    public void AddEnemy(Enemy myEnemy)
    {
        enemiesAlive.Add(myEnemy);
    }

    public void RemoveEnemy(Enemy myEnemy)
    {
        enemiesAlive.Remove(myEnemy);
    }
}
