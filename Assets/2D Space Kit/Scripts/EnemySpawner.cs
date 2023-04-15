using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform ship;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float spawnInterval = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy(spawnInterval, enemy));
    }

    void Update()
    {
        transform.transform.position = ship.position;
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        var newEnemy = Instantiate(
            enemy, 
            GetSpawnPosition(ship.position), 
            Quaternion.identity
            );
        StartCoroutine(SpawnEnemy(interval, enemy));
    }

    private Vector3 GetSpawnPosition(Vector3 playerPosition)
    {
        var offset = Quaternion.AngleAxis(Random.Range(-180f, 180f), Vector3.back) * (new Vector3(80, 50, 0));
        return playerPosition + offset;
    }
}
