using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtonTorpedo : MonoBehaviour
{
    public Rigidbody2D rb;
    private AudioSource audioSource;
    [SerializeField] private GameObject hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * ProtonTorpedoesTurret.Speed;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        audioSource.volume = PlayerPrefs.GetFloat("soundVolume");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, ProtonTorpedoesTurret.ownShip.transform.position);
        if (distance > 150)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        IEnemy enemy = hitInfo.GetComponent<IEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(ProtonTorpedoesTurret.Damage);
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
