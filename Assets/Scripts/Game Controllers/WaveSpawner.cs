using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveSpawner : MonoBehaviour
{    
    [SerializeField] private Transform ship;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3; 
    [SerializeField] private GameObject enemy4; 
    [SerializeField] private GameObject miniBoss;
    [SerializeField] private int maxEnemies;
    public enum SpawnState { Spawning, Waiting, Counting };
    private WaveProcessor waveProcessor;
    public float timeBetweenWaves = 1f;
    public float waveCountDown;
    public SpawnState state = SpawnState.Counting;
    
    private void Start()
    {
        waveCountDown = timeBetweenWaves;
        var waves = new Wave[] {
                new(
                        new Dictionary<GameObject, int>
                        {
                            { enemy1, 10 },
                        },
                        2
                    ),
                new(
                        new Dictionary<GameObject, int>
                        {
                            { enemy1, 8 },
                            { enemy2, 5 },
                        },
                        1
                    ),
                new(
                        new Dictionary<GameObject, int>
                        {
                            { enemy1, 5 },
                            { enemy2, 3 },
                            { enemy4, 3 },
                            { enemy3, 2 },
                        },
                        1
                    ),
                new(
                        new Dictionary<GameObject, int>
                        {
                            { enemy4, 7 },
                            { enemy1, 15 },
                            { enemy2, 7 },
                        },
                        1
                    ),
                new(
                        new Dictionary<GameObject, int>
                        {
                            { enemy4, 6 },
                            { enemy1, 5 },
                            { enemy2, 5 },
                            { enemy3, 3 },
                            { miniBoss, 1 },
                        },
                        1
                    ),
            };
        waveProcessor = new WaveProcessor(waves);
    }

    private void Update()
    {
        if (waveCountDown <= 0)
        {
            var enemyCount = GameObject.FindGameObjectsWithTag("enemy").Length;
            if (state != SpawnState.Spawning && enemyCount < maxEnemies)
                StartCoroutine(SpawnWave(waveProcessor.GetNextWave()));
        }
        
        else
            waveCountDown -= Time.deltaTime;
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.Spawning;

        foreach (var enemy in wave.GetEnemy())
        {
            SpawnEnemy(enemy);
            yield return new WaitForSeconds(1.7f * wave.delay / (float)waveProcessor.cycle);
        }

        state = SpawnState.Waiting;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Debug.Log(enemy);
        Instantiate(
            enemy,
            GetSpawnPosition(),
            Quaternion.identity);
    }
    private Vector3 GetSpawnPosition()
    {
        var offset = Quaternion.AngleAxis(UnityEngine.Random.Range(-180f, 180f), Vector3.back) * (new Vector3(150, 120, 0));
        return ship.position + offset;
    }
}

public class Wave
{
    private Dictionary<GameObject, int> enemies;
    public int delay;

    public Wave(Dictionary<GameObject, int> enemies, int delay)
    {
        this.enemies = enemies;
        this.delay = delay;
    }

    public IEnumerable<GameObject> GetEnemy()
    {
        foreach (var pair in enemies)
            for (var i = 0; i < pair.Value; i++)
                yield return pair.Key;
    }
}

class WaveProcessor
{
    private List<Wave> waves = new();
    private int waveNumber;
    public int cycle;

    public WaveProcessor(Wave[] waves)
    {
        waveNumber = -1;
        foreach (var wave in waves)
            this.waves.Add(wave);
    }

    public Wave GetNextWave()
    {
        waveNumber = (waveNumber + 1) % waves.Count;
        cycle += waveNumber == 0 ? 1 : 0;
        return waves[waveNumber];
    }
}
