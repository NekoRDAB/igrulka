using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{    
    [SerializeField] private Transform ship;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3; 
    [SerializeField] private GameObject enemy4; 
    [SerializeField] private GameObject miniBoss;
    public enum SpawnState { Spawning, Waiting, Counting };
    private WaveProcessing waveProcessing;
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
                            { enemy4, 2 },
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
                        2
                    ),
                new(
                        new Dictionary<GameObject, int>
                        {
                            { enemy4, 7 },
                            { enemy1, 15 },
                            { enemy2, 7 },
                        },
                        2
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
                        2
                    ),
            };
        waveProcessing = new WaveProcessing(waves);
    }

    private void Update()
    {
        if (waveCountDown <= 0)
        {
            if (state != SpawnState.Spawning)
                StartCoroutine(SpawnWave(waveProcessing.GetNextWave()));
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
            yield return new WaitForSeconds(wave.delay);
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
        var offset = Quaternion.AngleAxis(Random.Range(-180f, 180f), Vector3.back) * (new Vector3(80, 50, 0));
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

class WaveProcessing
{
    private List<Wave> waves = new();
    private int waveNumber;

    public WaveProcessing(Wave[] waves)
    {
        waveNumber = -1;
        foreach (var wave in waves)
            this.waves.Add(wave);
    }

    public Wave GetNextWave()
    {
        waveNumber = (waveNumber + 1) % waves.Count;
        return waves[waveNumber];
    }
}
