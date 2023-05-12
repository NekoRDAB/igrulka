using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class NukeExplosion : MonoBehaviour
{
    private Stopwatch timeAlive;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        timeAlive = new Stopwatch();
        timeAlive.Start();
        audio = GetComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetFloat("soundVolume");
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAlive.ElapsedMilliseconds > MiniNukeTurret.Duration)
        {
            timeAlive.Stop();
            Destroy(gameObject);
        }

        transform.localScale *= 1.006f;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        IEnemy enemy = hitInfo.GetComponent<IEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(MiniNukeTurret.Damage);
        }
    }
}
