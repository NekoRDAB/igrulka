using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : MonoBehaviour
{
    public Rigidbody2D rb;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * RefrigeratorTurret.Speed;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        audioSource.volume = PlayerPrefs.GetFloat("soundVolume");
    }
    
    void Update()
    {
        var distance = Vector2.Distance(transform.position, RefrigeratorTurret.ownShip.transform.position);
        if (distance > 150)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        var enemy = hitInfo.GetComponent<IEnemy>();
        if (enemy != null)
        {
            enemy.Freeze(3);
            Destroy(gameObject);
        }
    }
}
