using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakProjectileLogic : MonoBehaviour
{
    public Rigidbody2D rb;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = (transform.up + transform.right*Random.Range(-0.2f, 0.2f)) * FlakTurret.Speed;
        //audioSource = GetComponent<AudioSource>();
        //audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, FlakTurret.ownShip.transform.position);
        if (distance > 150)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        IEnemy enemy = hitInfo.GetComponent<IEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(FlakTurret.Damage);
            Destroy(gameObject);
        }
    }
}
