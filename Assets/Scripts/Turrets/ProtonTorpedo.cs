using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtonTorpedo : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject ownShip;
    private AudioSource audioSource;
    private GameObject turret;

    private ProtonTorpedoesTurret turretBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        turret = GameObject.Find("ProtonTorpedoesTurret");
        turretBehaviour = turret.GetComponent<ProtonTorpedoesTurret>();
        rb.velocity = transform.up * turretBehaviour.Speed;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        ownShip = GameObject.Find("OwnShip");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, ownShip.transform.position);
        if (distance > 150)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyBehaviour enemy = hitInfo.GetComponent<EnemyBehaviour>();
        if (enemy != null)
        {
            enemy.TakeDamage(turretBehaviour.Damage);
            Destroy(gameObject);
        }
    }
}
