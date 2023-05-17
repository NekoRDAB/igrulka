using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class NukeExplosion : MonoBehaviour
{
    private AudioSource audio;
    private float timeAlive;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetFloat("soundVolume");
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAlive > MiniNukeTurret.Duration)
        {
            timeAlive = 0;
            Destroy(gameObject);
        }
        else
        {
            timeAlive += Time.deltaTime;
            transform.localScale *= 1.035f;
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        IEnemy enemy = hitInfo.GetComponent<IEnemy>();
        if (enemy != null)
            enemy.TakeDamage(MiniNukeTurret.Damage);
    }
}
