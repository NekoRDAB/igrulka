using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class NukeExplosion : MonoBehaviour
{
    private Stopwatch timeAlive;

    // Start is called before the first frame update
    void Start()
    {
        timeAlive = new Stopwatch();
        timeAlive.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAlive.ElapsedMilliseconds > 1000)
        {
            timeAlive.Stop();
            Destroy(gameObject);
        }

        transform.localScale *= 1.005f;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyBehaviour enemy = hitInfo.GetComponent<EnemyBehaviour>();
        if (enemy != null)
        {
            enemy.TakeDamage(100);
        }
    }
}
