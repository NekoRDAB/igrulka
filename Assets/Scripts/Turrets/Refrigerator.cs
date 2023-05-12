using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject ownShip;
    private AudioSource audioSource;
    private GameObject turret;

    private RefrigeratorTurret turretBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * RefrigeratorTurret.Speed;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        audioSource.volume = PlayerPrefs.GetFloat("soundVolume");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, RefrigeratorTurret.ownShip.transform.position);
        if (distance > 150)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        IEnemy enemy = hitInfo.GetComponent<IEnemy>();
        if (enemy != null)
        {
            enemy.Freeze(3);
            Destroy(gameObject);
        }
    }
}
