using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class enemyProjectileBehaviour : MonoBehaviour, IEnemy
{
    public Rigidbody2D rb;
    private AudioSource audioSource;
    private float timeToLive;
    public int damage { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * 10f;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        audioSource.volume = PlayerPrefs.GetFloat("soundVolume");
        damage = 10;
        timeToLive = 5f;
    }

    void Update()
    {
        if (timeToLive <= 0)
            Destroy(gameObject);
        else
            timeToLive -= Time.deltaTime;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), gameObject.GetComponent<CircleCollider2D>());
        var player = collision.gameObject.GetComponent<PlayerShipBehaviour>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
    }


    public void TakeDamage(int damage)
    {
        Destroy(gameObject);
    }

    public void Freeze(float time)
    {
        Destroy(gameObject);
    }
}
