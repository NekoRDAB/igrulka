using UnityEngine;

public class DebriSpawner : MonoBehaviour
{
    [SerializeField] private Transform ship;
    [SerializeField] private GameObject debri;
    public float timeBetweenSpawning = 15f;
    public float debriCountDown { get; set; }
    private void Start()
    {
        debriCountDown = timeBetweenSpawning;
    }

    private void Update()
    {

        if (debriCountDown <= 0)
        {
            SpawnDebri(debri);
            debriCountDown = timeBetweenSpawning;
        }
        else
            debriCountDown -= Time.deltaTime;
    }

    void SpawnDebri(GameObject debri)
    {
        Debug.Log(debri);
        Instantiate(
            debri,
            GetSpawnPosition(),
            Quaternion.identity);
    }
    private Vector3 GetSpawnPosition()
    {
        var offset = Quaternion.AngleAxis(Random.Range(-180f, 180f), Vector3.back) * (new Vector3(80, 50, 0));
        return ship.position + offset;
    }
}
