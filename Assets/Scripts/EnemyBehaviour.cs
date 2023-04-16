using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] public int health = 100;
    [SerializeField] public int damage = 40;
    private SpriteRenderer spriteRenderer;
    private Stopwatch timeDamaged;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject exp;
    [SerializeField] private GameObject damageNumbers;

    public EnemyInterface EnemyInterface { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        EnemyInterface = new EnemyInterface(damage, 0.08f, GetComponent<Rigidbody2D>());
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeDamaged = new Stopwatch();
        player = GameObject.Find("OwnShip");
    }

    // Update is called once per frame
    void Update()
    {
        var movement = (Vector2)player.transform.position - EnemyInterface.Ship.position;
        movement = Vector2.ClampMagnitude(movement, EnemyInterface.Speed);
        EnemyInterface.Ship.MovePosition(EnemyInterface.Ship.position + movement);
        transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(movement.x, movement.y) * 180 / Mathf.PI);
        if (spriteRenderer.color == Color.red && timeDamaged.ElapsedMilliseconds > 400)
        {
            spriteRenderer.color = Color.white;
            timeDamaged.Stop();
            timeDamaged.Reset();
        }
            
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        var damageNumber = Instantiate(damageNumbers, EnemyInterface.Ship.position, Quaternion.identity);
        damageNumber.GetComponent<NumbersBehaviour>().SetNumber(damage);
        spriteRenderer.color = Color.red;
        if (health <= 0)
        {
            Die();
            return;
        }
        timeDamaged.Start();
    }

    void Die()
    {
        Instantiate(explosion, EnemyInterface.Ship.position, Quaternion.identity);
        Instantiate(exp, EnemyInterface.Ship.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
