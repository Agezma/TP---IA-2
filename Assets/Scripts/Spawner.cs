using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour, IUpdate, IRestartable
{
    public List<Enemy> enemies = new List<Enemy>();

    public List<List<Enemy>> waves = new List<List<Enemy>>();
    public int wavesQuantity;
    public int[] enemiesPerWave;

    private int nextWave = 0;

    public float timeBetweenWaves = 10f;
    public float timeBetweenEnemies = 1f;

    IController controller;

    public Text startMessage;

    enum State { Start, SpawningWave, WaveInProgress, WaitingToSpawn, LevelCompleted }; 
    /// <EstadosDelJuego>
    /// Start: inicio del juego.
    /// Spawning: Durante la creacion de la wave.
    /// InProgress: Una vez que la wave ya fue completamente creada -> Cuando se mueren todos los enemigos de la wave, el estado pasa a Waiting.
    /// </summary>

    State gameState;

    void Start()
    {
        gameState = State.Start;
        UpdateManager.Instance.AddUpdate(this);
        RestartableManager.Instance.AddRestartable(this);

        GenerateWaves();
        StartCoroutine(checkEnemy());

        controller = new ComputerController();
        nextWave = 0;
    }

    void GenerateWaves()
    {
        for (int i = 0; i < wavesQuantity; i++)
        {
            List<Enemy> wave = new List<Enemy>();
            for (int j = 0; j < enemiesPerWave[i]; j++)
            {
                wave.Add(enemies[Random.Range(0, enemies.Count)]);
            }
            waves.Add(wave);
        }
    }

    public void OnUpdate()
    {
        if (controller.SpawnWave() && gameState == State.Start )         
            StartCoroutine(SpawnWave(waves[nextWave]));


        if ( controller.SpawnWave() && gameState == State.WaitingToSpawn)
        {
            StartCoroutine(SpawnWave(waves[nextWave]));
            startMessage.gameObject.SetActive(false);
            startMessage.text = "";
        }
    }

    public IEnumerator checkEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            
            if (gameState == State.WaveInProgress && Main.Instance.enemyManager.enemiesAlive.Count <= 0)
            {
                gameState = State.WaitingToSpawn;

                RestartableManager.Instance.WaveEnded();
                startMessage.gameObject.SetActive(true);
                startMessage.text = "Wave completed";
                yield return new WaitForSeconds(2f);
                if (nextWave < wavesQuantity)
                    startMessage.text = "Click here to start next round";
                else
                {
                    gameState = State.LevelCompleted;
                    Main.Instance.winPanel.SetActive(true);
                }
            }
        }
    }

    IEnumerator SpawnWave(List<Enemy> wave)
    {
        if(nextWave == wavesQuantity)
        {
            yield break;
        }
        
        gameState = State.SpawningWave;
        hasToReset = true;
        for (int i = 0; i < wave.Count; i++)
        {
            Enemy enemy = SpawnEnemy(wave[i]);
            Main.Instance.enemyManager.AddEnemy(enemy);
            Main.Instance.spatialGrid.CheckEnts();
            yield return new WaitForSeconds(timeBetweenEnemies);

        }
        gameState = State.WaveInProgress;
        nextWave += 1;
        yield break;
    }

    Enemy SpawnEnemy(Enemy enemyToSpawn)
    {
        Enemy enemy = Instantiate(enemyToSpawn, transform.position, transform.rotation, transform);
        enemy.currentWp = this.GetComponent<Waypoint>();
        
        return enemy;
    }

    public void SetOnWaveEnd()
    {

    }

    bool hasToReset = true;

    public void RestartFromLastWave()
    {
        if (hasToReset)
        {
            nextWave -= 1;
            hasToReset = false;
        }
    }
}
