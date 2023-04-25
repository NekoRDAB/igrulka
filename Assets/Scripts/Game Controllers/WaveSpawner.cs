using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { Spawning, Waiting, Counting };

    [SerializeField] private Transform ship;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    private WaveProcessing waveProcessing;

    public float timeBetweenWaves = 1f;
    public float waveCountDown;
    public SpawnState state = SpawnState.Counting;
    private void Start()
    {
        waveCountDown = timeBetweenWaves;
        var waves = new Wave[] {
                new Wave(
                        new Dictionary<GameObject, int>
                        {
                            { enemy1, 10 }
                        },
                        2
                    ),
                new Wave(
                        new Dictionary<GameObject, int>
                        {
                            { enemy2, 15 }
                        },
                        1
                    )
            };
        waveProcessing = new WaveProcessing(waves);
    }

    private void Update()
    {

        if (waveCountDown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waveProcessing.GetNextWave()));
            }
        }
        else
            waveCountDown -= Time.deltaTime;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.Spawning;

        foreach (var enemy in wave.GetEnemy())
        {
            SpawnEnemy(enemy);
            yield return new WaitForSeconds(wave.delay);
        }

        state = SpawnState.Waiting;

        yield break;
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
        yield break;
    }
}

class WaveProcessing
{
<<<<<<< HEAD
    private List<(Wave, int)> waves = new List<(Wave, int)>();
=======
    private List<Wave> waves = new List<Wave>();
>>>>>>> WabeSpawner
    private int waveNumber;

    public WaveProcessing(Wave[] waves)
    {
        waveNumber = -1;
        foreach (var wave in waves)
            this.waves.Add((wave));
    }

    public Wave GetNextWave()
    {
        waveNumber = (waveNumber + 1) % waves.Count;
        return waves[waveNumber];
    }
}
